using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panels;
using CountDown;
using Manager.Robots;
using Networking.Game;
using Save;


namespace Manager.Train
{
    public class BuildRobots : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Text _numberOfRobots;
        [SerializeField] private Timer _time;

        [Space(20f)]

        [SerializeField] private Text _timeRemained;
        [SerializeField] private ProgressBar _tempLoadingBar;
        #endregion

        #region "Private Members"
        private bool _once = false;
        private static float s_tempTime = 0;

        public static Timer Time;
        #endregion

        #region "Awake"
        private void Awake()
        {
            GetRobotsTrained.RobotsBuilt = new List<RobotSO>();

            _timeRemained.text = "";
            _tempLoadingBar.MaxValue = 1;
            _tempLoadingBar.CurrentValue = 1;

            Time = _time;

            BuildRobotsOperations.OnStartOperation += StartBuildingRobots;
            BuildRobotsOperations.OnStopOperation += StopBuildingRobots;

            BuildRobotsOperations.OnRobotAdded += Time.AddTime;

            UnlimitedPlayerManager.OnRobotsUpdate += DisplayNumberOfRobots;
            UnlimitedPlayerManager.OnPlayerDataReceived += DisplayNumberOfRobots;
        }
        #endregion


        private void StartBuildingRobots()
        {
            if (_once == false)
            {
                Time.TimeTextState(true);
                StartCoroutine("BuildingRobots");
                _once = true;
            }
        }
        private void StopBuildingRobots()
        {
            StopCoroutine("BuildingRobots");
            _once = false;

            Time.TimeTextState(false);
            Time.TotalTime = 0;
        }


        /// <summary>
        /// Displays the number of robots
        /// </summary>
        private void DisplayNumberOfRobots()
        {
            int count = 0;

            foreach (Networking.GameElements.Robot item in UnlimitedPlayerManager.player.Robots)
            {
                count += item.Count;
            }

            _numberOfRobots.text = string.Format("{0}/30", count);                      
        }


        public static void RecalculateTime()
        {
            Time.TotalTime = 0;

            foreach (KeyValuePair<ushort, RobotSO> item in BuildRobotsOperations.RobotsInTraining)
            {
                Time.AddTime(GameDataValues.Robots[item.Value._robotId].BuildTime);
            }

            Time.DecreaseTime((int)s_tempTime);
        }


        private IEnumerator BuildingRobots()
        {
            _timeRemained.enabled = true;

            for (int i = 0; i < BuildRobotsOperations.RobotsInTraining.Count; i++)
            {
                RobotSO robot = BuildRobotsOperations.RobotsInTraining.ElementAt(i).Value;
                ushort queueNumber = BuildRobotsOperations.RobotsInTraining.ElementAt(i).Key;

                if (RobotsInBuilding.robotsInBuildingIcons.Count > 0)
                {
                    _tempLoadingBar.MaxValue = GameDataValues.Robots[robot._robotId].BuildTime;
                }

                s_tempTime = 0;

                while (robot != null && s_tempTime < GameDataValues.Robots[robot._robotId].BuildTime)
                {
                    s_tempTime += 1;

                    _tempLoadingBar.CurrentValue = (int)s_tempTime;
                    Time.DisplayTime(_timeRemained, (int)(GameDataValues.Robots[robot._robotId].BuildTime - s_tempTime));
                    
                    yield return _time.ActivateTimer();
                }

                UnlimitedPlayerManager.FinishBuildingRequest(queueNumber, robot._robotId, DateTime.UtcNow, true);

                BuildRobotsOperations.RobotsInTraining.Remove(queueNumber);
                RobotsInBuildingOperations.DezactivateIcon(RobotsInBuilding.robotsInBuildingIcons[i]);
                RobotsInBuilding.robotsInBuildingIcons.RemoveAt(i);
                i--;
            }

            _once = false;
            Time.TimeTextState(false);

            // last robot loading bar
            _timeRemained.enabled = false;
            _tempLoadingBar.MaxValue = 1;
        }


        private void OnDestroy()
        {
            BuildRobotsOperations.OnStartOperation -= StartBuildingRobots;
            BuildRobotsOperations.OnStopOperation -= StopBuildingRobots;

            BuildRobotsOperations.OnRobotAdded -= Time.AddTime;

            UnlimitedPlayerManager.OnRobotsUpdate -= DisplayNumberOfRobots;
            UnlimitedPlayerManager.OnPlayerDataReceived -= DisplayNumberOfRobots;
        }
    }
}