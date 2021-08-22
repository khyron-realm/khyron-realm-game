using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// -Enum for the directions the robot can take in the mine [2d space]
/// -Used so user can not give a command twice on the same path
/// 
/// </summary>
public enum Direction { left, right, up, down, none };


/// <summary>
/// 
/// Manager of the robot 
/// 
/// Logic:
/// 
/// User drag from the command block to a desired position [PreviewCommand script is responsible]
/// User lift the finger and command is executed if there is a difference between command block position and place where finger was lifted [ExecuteCommand script is reponsible]
/// The List of commands in PreviewCommand [hitsPreview] is added to the end of nested list _allHits in this script
/// The ExecuteCommand calls a method from the mining script that starts the process of mining and moving
/// As blocks are mined, they are removed from the list and in the end, the whole command is removed from the nested list
/// If user taps on the commandBlock, the action is considered a delete action, and the last command given[Last list of colliders in the nested list] is deleted;
/// 
/// Commands can not overlap so there is a restricion based on the Direction enum
/// All commands have a Direction asociated
/// 
/// </summary>

[RequireComponent(typeof(PreviewCommands))]
[RequireComponent(typeof(ExecuteCommands))]
[RequireComponent(typeof(DeleteCommands))]

public class RobotManager : MonoBehaviour
{
    [Header("Robot SO with all stats about the robot")]
    public Robot robot;

    [Header("Collider Size for the Command block so user can touch it")]
    [Space(30f)]
    [SerializeField]
    private Vector2 _collidersize;

    [Header("Position on Z axis for the command block")]
    [Space(30f)]
    [SerializeField]
    private int _layerZ;

    [Header("Selecting mining type [Experimental]")]
    [Space(30f)]
    [SerializeField]
    private bool _miningType;

    // List with all commands given to robot
    private List<List<Vector3>> _allTiles;

    // List with all directions given to the robot
    private List<Direction> _directions;
    private Direction _directionSaved = Direction.none;

    // Gameobject responsible for drawing  the path user choose to take
    private DrawPathToFollowForRobot _path;

    // Command block -- The gameobject used for giving commands which is separate from the robot GameObject
    private CreateCommandBlock _block;
    private GameObject _commandBlock;
    
    private SpriteRenderer _commandBlockSprite;

    // Components that implemnents the interfaces attached to the gameObject
    private PreviewCommands _handleTouch;
    private IExecuteCommand<List<Vector3>> _executeCommand;
    private IDeleteCommand<List<Vector3>> _deleteCommand;

    private IMining<Vector3> _mine;
    private IMove _move;

    private void Awake()
    {
        _allTiles = new List<List<Vector3>>();
        _directions = new List<Direction>();
       
        CreateCommandBlock();
        GetCommandsScripts();
        SetCommandBlock();

        SubscribeMethods();
    }

    #region "Commands"
    private void PreviewCommands()
    {
        if(_allTiles.Count == 0)
        {
            _directionSaved = Direction.none;
        }

        if(MaximumNumberOfCommands())
        {
            _handleTouch.PreviewCommand(_directionSaved);

            _path.CreatePath(_allTiles, true, _handleTouch.TilesPositions);
        }
    }

    private void ExecuteCommand()
    {
        if (MaximumNumberOfCommands())
        {
            _directions.Add(_handleTouch.Direction);
            _directionSaved = ConvertDirection(_handleTouch.Direction);

            _allTiles.Add(new List<Vector3>(_handleTouch.TilesPositions));
            _handleTouch.TilesPositions.Clear();

            _path.SetWayPoint(_commandBlockSprite, _allTiles.Count);
            _executeCommand.ExecuteCommand(_allTiles);
        }
    }

    private void DeleteCommand()
    {
        if (_allTiles.Count > 0)
        {
            _directions.RemoveAt(_directions.Count - 1);

            if(_directions.Count > 0)
            {
                _directionSaved = ConvertDirection(_directions[_directions.Count - 1]);
            }

            _deleteCommand.DeleteCommand(_allTiles);

            SetPath();
        }

        if (_allTiles.Count == 0)
        {
            _path.DeleteLine();
            _directionSaved = Direction.none;
        }

        _path.SetWayPoint(_commandBlockSprite, _allTiles.Count);
    }
    #endregion

    #region "Set Variables in awake"
    private void CreateCommandBlock()
    {
        _block = new CreateCommandBlock();
        _block.Create(new Vector3(transform.position.x, transform.position.y, _layerZ), _collidersize);
        _commandBlockSprite = _block.GetBlock().GetComponent<SpriteRenderer>();
        _commandBlock = _block.GetBlock();
    }

    private void GetCommandsScripts()
    {
        _handleTouch = GetComponent<PreviewCommands>();
        _executeCommand = GetComponent<IExecuteCommand<List<Vector3>>>();
        _deleteCommand = GetComponent<IDeleteCommand<List<Vector3>>>();

        _path = GetComponent<DrawPathToFollowForRobot>();

        _mine = GetComponent<IMining<Vector3>>();
        _move = GetComponent<IMove>();
    }

    private void SetCommandBlock()
    {
        _handleTouch.CommandBlock = _block.GetBlock();
        _executeCommand.CommandBlock = _block.GetBlock();
        _deleteCommand.CommandBlock = _block.GetBlock();
    }

    private void SubscribeMethods()
    {
        _block.GetBlock().GetComponent<CommandBlockHandler>().OnPreviewCommand += PreviewCommands;
        _block.GetBlock().GetComponent<CommandBlockHandler>().OnExecuteCommand += ExecuteCommand;
        _block.GetBlock().GetComponent<CommandBlockHandler>().OnDeleteCommand += DeleteCommand;

        _mine.OnFinishedMining += SetPath;
        _move.OnStartingMoving += SetPath;
    }
    #endregion

    #region "Auxilary methods" 
    private Direction ConvertDirection(Direction temp)
    {
        if(temp == Direction.left)
        {
            return Direction.right;
        }
        else if(temp == Direction.right)
        {
            return Direction.left;
        }
        else if (temp == Direction.up)
        {
            return Direction.down;
        }
        else if (temp == Direction.down)
        {
            return Direction.up;
        }
        else
        {
            return Direction.none;
        }
    }

    private bool MaximumNumberOfCommands()
    {
        return _allTiles.Count < robot.actionNumber;
    }

    private void SetPath()
    {
        if (_allTiles.Count > 0)
        {
            _path.CreatePath(_allTiles);
        }

        _path.SetWayPoint(_commandBlockSprite, _allTiles.Count);
    }
    #endregion

    public GameObject commandBlock
    {
        get
        {
            return _commandBlock;
        }
    }
}