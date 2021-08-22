using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Responsible for executing the command given
/// 
/// </summary>

[RequireComponent(typeof(LiniarMining))]
public class ExecuteCommands : MonoBehaviour, IExecuteCommand<List<Vector3>>
{
    private Robot _robot;
    private GameObject _commandBlock;

    private bool _coroutineStartedOnce = true;

    private IMining<Vector3> _mine;

    #region "Setters and Getters" 
    public GameObject CommandBlock
    {
        set
        {
            _commandBlock = value;
        }
    }
    #endregion

    private void Awake()
    {
        _robot = GetComponent<RobotManager>().robot;
        _mine = GetComponent<IMining<Vector3>>();

        _mine.OnFinishedMining += SetCoroutineTrue;
    }

    #region "Methods"
    public void ExecuteCommand(List<List<Vector3>> _allHits)
    {
        List<Vector3> _lastList = new List<Vector3>(_allHits[_allHits.Count - 1]);

        if (_lastList.Count > 0)
        {
            _commandBlock.transform.position = new Vector3(_lastList[_lastList.Count - 1].x, _lastList[_lastList.Count - 1].y, -6f);
        }

        if (_allHits.Count > 0 && _coroutineStartedOnce == true)
        {
            SetCoroutineFalse();
            _mine.Mine(_allHits, _robot.damagePerSecond); // coroutine starts only once and stop if there are no more commands left to execute
        }
    }

    public void SetCoroutineTrue()
    {
        _coroutineStartedOnce = true;
    }

    public void SetCoroutineFalse()
    {
        _coroutineStartedOnce = false;
    }
    #endregion
}