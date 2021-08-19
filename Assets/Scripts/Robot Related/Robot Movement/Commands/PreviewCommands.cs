using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Responsible for previewing the path the robot will take/mine
/// 
/// -Detects the colliders on one direction and save them in a list.
/// -Colliders are detected using OverlapPoint
/// 
/// </summary>
public class PreviewCommands : MonoBehaviour, IPreviewCommand
{
    [SerializeField]
    private LayerMask _mask;

    [SerializeField]
    private float _tileDistance;

    private Robot _robot;
    private GameObject _commandBlock;

    private List<Collider2D> _hitsPreview;
    private Direction _direction;
    private Direction _directionLast;
  
    #region "Setters and Getters"
    public Direction Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value;
        }
    }

    public GameObject CommandBlock
    {
        set
        {
            _commandBlock = value;
        }
    }
    public List<Collider2D> HitsPreview
    {
        get
        {
            return _hitsPreview;
        }
        set
        {
            _hitsPreview = value;
        }
    }
    #endregion

    private void Awake()
    {
        _hitsPreview = new List<Collider2D>();
        _robot = GetComponent<RobotManager>().robot;
    }

    #region "Methods"

    public void PreviewCommand(Direction dir)
    {
        _directionLast = dir;

        Vector2 diff = UserTouch.TouchPosition(0) - _commandBlock.transform.position;

        float diffXaxis = Mathf.Abs(diff.x);
        float diffYaxis = Mathf.Abs(diff.y);

        HandleCommand(diff, diffXaxis, diffYaxis);
    }

    /// <summary>
    /// 
    /// -Handle direction of touch.position on one axis and then get list of colliders
    /// 
    /// -Check for the direction adn save it
    /// 
    /// -Restrict the previous direction the last command so 2 commands can not overlap
    /// 
    /// </summary>
    private void HandleCommand(Vector2 diff, float diffXaxis, float diffYaxis)
    {
        if (diffXaxis / diffYaxis > 1)
        {
            int temp = (int)Mathf.Sign(diff.x);

            if(temp == 1)
            {
                _direction = Direction.right;
            }
            else if(temp == -1)
            {
                _direction = Direction.left;
            }

            if (_direction != _directionLast)
            {
               ClearPreview();
               ManageCollidersPreview(diffXaxis, temp, 0);
            }
            
        }
        else
        {
            int temp = (int)Mathf.Sign(diff.y);

            if (temp == 1)
            {
                _direction = Direction.up;
            }
            else if (temp == -1)
            {
                _direction = Direction.down;
            }

            if (_direction != _directionLast)
            {
                ClearPreview();
                ManageCollidersPreview(diffYaxis, 0, temp);
            }
        }
    }

    /// <summary>
    /// 
    /// Get the list of colliders on the desired direction
    /// 
    /// </summary>
    /// <param name="temp"> difference between transform.position and touch position on one axis </param>
    private void ManageCollidersPreview(float temp, int coef_x, int coef_y)
    {
        int crt = (int)(temp / _tileDistance);

        if (crt > _robot.actionLength)
        {
            crt = _robot.actionLength;
        }

        for (int i = 0; i <= crt; i++)
        {
            float x = _commandBlock.transform.position.x + coef_x * ((i + 1) * _tileDistance);
            float y = _commandBlock.transform.position.y + coef_y * ((i + 1) * _tileDistance);

            Collider2D hit = Physics2D.OverlapPoint(new Vector2(x, y), _mask);

            if (hit != null && !_hitsPreview.Contains(hit))
            {
                _hitsPreview.Add(hit);
            }

            if (_hitsPreview.Count > crt)
            {
                _hitsPreview.RemoveAt(_hitsPreview.Count - 1);
            }
        }
    }

    private void ClearPreview()
    {
        _hitsPreview.Clear();
    }

    #endregion
}