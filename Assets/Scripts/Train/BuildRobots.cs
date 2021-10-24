using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panels;
using CountDown;
using Manager.Robots;
using Save;
using Networking.Headquarters;
using PlayerDataUpdate;


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
        private static int s_tempTime = 0;

        public static Timer Time;
        private Coroutine _coroutine;

        private RobotSO _robot;
        private ushort _queueNumber;

        private static byte Tag = 2;
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
            BuildRobotsOperations.OnFirstRobotAdded += FirstRobotTimeRemained;

            PlayerDataOperations.OnRobotAdded += RobotFinishedBuilding;
            PlayerDataOperations.OnRobotAdded += ShowRobotsNumber;

            HeadquartersManager.OnPlayerDataReceived += DisplayNumberOfRobots;       
        }
        #endregion


        /// <summary>
        /// Building process operations
        /// </summary>
        private void StartBuildingRobots()
        {
            if (_once == false)
            {
                Time.TimeTextState(true);
                _coroutine = StartCoroutine(BuildingRobots());
                _once = true;
            }
        }
        private void StopBuildingRobots()
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
          
            _once = false;

            _timeRemained.enabled = false;
            _tempLoadingBar.MaxValue = 1;

            Time.TimeTextState(false);
            Time.TotalTime = 0;

            s_tempTime = 0;
        }


        /// <summary>
        /// Displays the number of robots
        /// </summary>
        private void DisplayNumberOfRobots()
        {
            int count = 0;

            for(int i = 0; i < GameDataValues.Robots.Count; i++)
            {
                count += HeadquartersManager.Player.Robots[i].Count * GameDataValues.Robots[i].HousingSpace;
            }

            _numberOfRobots.text = string.Format("{0}/{1}", count, GameDataValues.MaxHousingSpace);                      
        }
        private void ShowRobotsNumber(byte tag)
        {
            DisplayNumberOfRobots();
        }


        /// <summary>
        /// Recalculate total time for building robots
        /// </summary>
        public static void RecalculateTime()
        {
            Time.TotalTime = 0;

            foreach (KeyValuePair<ushort, RobotSO> item in BuildRobotsOperations.RobotsInTraining)
            {
                Time.AddTime(GameDataValues.Robots[item.Value._robotId].BuildTime);
            }

            Time.DecreaseTime((int)s_tempTime);
        }
        private void FirstRobotTimeRemained(int timePassed, int timeRemained)
        {
            s_tempTime = timePassed;
            Time.AddTime(timeRemained);
        }


        /// <summary>
        /// Coroutine that keeps the timer and the process of building robots
        /// </summary>
        private IEnumerator BuildingRobots()
        {
            _timeRemained.enabled = true;

            for (int i = 0; i < BuildRobotsOperations.RobotsInTraining.Count; i++)
            {
                _robot = BuildRobotsOperations.RobotsInTraining.ElementAt(i).Value;
                _queueNumber = BuildRobotsOperations.RobotsInTraining.ElementAt(i).Key;

                if (RobotsInBuilding.robotsInBuildingIcons.Count > 0)
                {
                    _tempLoadingBar.MaxValue = GameDataValues.Robots[_robot._robotId].BuildTime;
                }

                while (_robot != null && s_tempTime < GameDataValues.Robots[_robot._robotId].BuildTime)
                {
                    s_tempTime += 1;

                    _tempLoadingBar.CurrentValue = (int)s_tempTime;
                    Time.DisplayTime(_timeRemained, (int)(GameDataValues.Robots[_robot._robotId].BuildTime - s_tempTime));
                    
                    yield return Time.ActivateTimer();
                }

                s_tempTime = 0;

                PlayerDataOperations.AddRobot(_robot._robotId, Tag);

                BuildRobotsOperations.RobotsInTraining.Remove(_queueNumber);
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
        private void RobotFinishedBuilding(byte tag)
        {
            if(Tag == tag)
            {
                BuildRobotsOperations.TotalHousingSpaceDuringBuilding -= GameDataValues.Robots[_robot._robotId].HousingSpace;
                HeadquartersManager.FinishBuildingRequest(_queueNumber, _robot._robotId, DateTime.UtcNow, HeadquartersManager.Player.Robots[_robot._robotId]);
            }
        }


        private void OnDestroy()
        {
            BuildRobotsOperations.OnStartOperation -= StartBuildingRobots;
            BuildRobotsOperations.OnStopOperation -= StopBuildingRobots;

            BuildRobotsOperations.OnRobotAdded -= Time.AddTime;
            BuildRobotsOperations.OnFirstRobotAdded += FirstRobotTimeRemained;

            PlayerDataOperations.OnRobotAdded -= RobotFinishedBuilding;
            PlayerDataOperations.OnRobotAdded -= ShowRobotsNumber;

            HeadquartersManager.OnPlayerDataReceived -= DisplayNumberOfRobots;
        }
    }
}