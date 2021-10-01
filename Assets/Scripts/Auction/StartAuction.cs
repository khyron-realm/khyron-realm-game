using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Save;
using CountDown;

public class StartAuction : MonoBehaviour
{
    [SerializeField] private Timer _time;
    [SerializeField] private int _totalTime;

    private MineValues _valuesMine;
    private TimeValues _valuesTime;

    private Coroutine _coroute;

    private void Start()
    {
        _valuesMine = GetComponent<MineValues>();
        _valuesTime = GetComponent<TimeValues>();

        if (_valuesTime.TimeTillFinished == 0)
        {
            _coroute = StartCoroutine(AuctionInProgress(_totalTime));
        }
        else
        {
            _coroute = StartCoroutine(AuctionInProgress(_valuesTime.TimeTillFinished));
        }
    }

    public void Restart()
    {
        StopCoroutine(_coroute);
        _coroute = StartCoroutine(AuctionInProgress(_totalTime));
    }


    private IEnumerator AuctionInProgress(int time)
    {
        _time.TotalTime = time;
        
        while (_time.TotalTime > 1)
        {
            _valuesTime.TimeTillFinished = _time.TotalTime;
            _valuesTime.SaveData();

            yield return _time.ActivateTimer();
        }

        _valuesMine.Refresh();
        _coroute = StartCoroutine(AuctionInProgress(_totalTime));
    }
}