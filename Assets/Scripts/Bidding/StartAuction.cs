using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Save;
using CountDown;


namespace Bidding
{
    public class StartAuction : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Timer _time;
        [SerializeField] private int _totalTime;
        #endregion

        #region "Private members"
        private Coroutine _coroute;
        #endregion

        private void Start()
        {
            if (0 == 0)
            {
                _coroute = StartCoroutine(AuctionInProgress(_totalTime));
            }
            else
            {
                _coroute = StartCoroutine(AuctionInProgress(1000));
            }
        }

        /// <summary>
        /// Restart the coroutine that tracks the time of the auction till it is ending
        /// </summary>
        public void Restart()
        {
            if(_coroute != null)
            {
                StopCoroutine(_coroute);
            }
            _coroute = StartCoroutine(AuctionInProgress(_totalTime));
        }


        /// <summary>
        /// Coroutine that keeps the time of auction and restart itself at the end of bidding
        /// </summary>
        /// <param name="time"> The initial time till auction ends </param>
        /// <returns></returns>
        private IEnumerator AuctionInProgress(int time)
        {
            _time.CurrentTime = time;

            while (_time.CurrentTime > 1)
            {
                //_valuesTime.TimeTillFinished = _time.CurrentTime;

                yield return _time.ActivateTimer();
            }

            _coroute = StartCoroutine(AuctionInProgress(_totalTime));
        }
    }
}