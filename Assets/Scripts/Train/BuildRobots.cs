using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panels;
using CountDown;
using Levels;
using Manager.Robots;
using Networking.Headquarters;
using PlayerDataUpdate;
using TMPro;


namespace Manager.Train
{
    public class BuildRobots : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private List<TextMeshProUGUI> _numberOfRobots;
        [SerializeField] private Timer _time;

        [Space(20f)]

        [SerializeField] private TextMeshProUGUI _timeRemained;
        [SerializeField] private ProgressBar _tempLoadingBar;
        #endregion


        #region "Private Members"
        private bool _once = false;
        private static int s_tempTime = 0;

        public static Timer Time;
        private Coroutine _coroutine;

        private RobotSO _robot;
        private ushort _queueNumber;
        #endregion

        public static event Action OnBuildingProcessFinished;
      
        private void Awake()
        {
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


        #region "Start & Stop Building process"
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
            Time.CurrentTime = 0;

            s_tempTime = 0;
        }
        #endregion


        #region "Show number of robots"
        /// <summary>
        /// Displays the number of robots
        /// </summary>
        private void DisplayNumberOfRobots()
        {
            var count = 0;
            
            for(int i = 0; i < HeadquartersManager.Player.Robots.Length; i++)
            {
                count += HeadquartersManager.Player.Robots[i].Count * RobotsManager.robots[i].HousingSpace;
            }

            foreach (TextMeshProUGUI item in _numberOfRobots)
            {
                item.text = string.Format("{0}/{1}", count, LevelMethods.HousingSpace(HeadquartersManager.Player.Level));
            }          
        }
        private void ShowRobotsNumber(byte receivedTag)
        {
            DisplayNumberOfRobots();
        }
        #endregion


        #region "Show Time"
        /// <summary>
        /// Recalculate total time for building robots
        /// </summary>
        public static void RecalculateTime()
        {
            Time.CurrentTime = 0;

            foreach (KeyValuePair<ushort, RobotSO> item in BuildRobotsOperations.RobotsInTraining)
            {
                Time.AddTime(RobotsManager.robots[item.Value.RobotId].BuildTime);
            }

            Time.DecreaseTime((int)s_tempTime);
        }
        private void FirstRobotTimeRemained(int timePassed, int timeRemained)
        {
            s_tempTime = timePassed;
            Time.AddTime(timeRemained);
        }
        #endregion


        #region "Building process"
        /// <summary>
        /// Coroutine that keeps the timer and the process of building robots
        /// </summary>
        private IEnumerator BuildingRobots()
        {
            _timeRemained.enabled = true;

            for (var i = 0; i < BuildRobotsOperations.RobotsInTraining.Count; i++)
            {
                _robot = BuildRobotsOperations.RobotsInTraining.ElementAt(i).Value;
                _queueNumber = BuildRobotsOperations.RobotsInTraining.ElementAt(i).Key;

                if (RobotsInBuilding.robotsInBuildingIcons.Count > 0)
                {                  
                    _tempLoadingBar.MaxValue = RobotsManager.robots[_robot.RobotId].BuildTime;
                }
                while (_robot != null && s_tempTime < RobotsManager.robots[_robot.RobotId].BuildTime)
                {
                    s_tempTime += 1;

                    _tempLoadingBar.CurrentValue = s_tempTime;
                    Time.DisplayTime(_timeRemained, (RobotsManager.robots[_robot.RobotId].BuildTime - s_tempTime));
                    
                    yield return Time.ActivateTimer();
                }

                s_tempTime = 0;

                PlayerDataOperations.AddRobot(_robot.RobotId, OperationsTags.BUILDING_ROBOTS);

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

            OnBuildingProcessFinished?.Invoke();
        }
        private void RobotFinishedBuilding(byte receivedTag)
        {
            if (OperationsTags.BUILDING_ROBOTS != receivedTag) return;

            BuildRobotsOperations.TotalHousingSpaceDuringBuilding -= RobotsManager.robots[_robot.RobotId].HousingSpace;
            HeadquartersManager.FinishBuildingRequest(_queueNumber, _robot.RobotId, DateTime.UtcNow, HeadquartersManager.Player.Robots[_robot.RobotId]);
            
        }
        #endregion


        private void OnDestroy()
        {
            BuildRobotsOperations.OnStartOperation -= StartBuildingRobots;
            BuildRobotsOperations.OnStopOperation -= StopBuildingRobots;

            BuildRobotsOperations.OnRobotAdded -= Time.AddTime;
            BuildRobotsOperations.OnFirstRobotAdded -= FirstRobotTimeRemained;

            PlayerDataOperations.OnRobotAdded -= RobotFinishedBuilding;
            PlayerDataOperations.OnRobotAdded -= ShowRobotsNumber;

            HeadquartersManager.OnPlayerDataReceived -= DisplayNumberOfRobots;
        }
    }
}