using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panels;
using CountDown;
using Manager.Robots;


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

        private bool _once = false;
        private static float _tempTime = 0;

        public static Timer Time;

        private void Awake()
        {
            GetRobotsTrained.RobotsBuilt = new List<Robot>();

            _timeRemained.text = "";
            _tempLoadingBar.MaxValue = 1;
            _tempLoadingBar.CurrentValue = 1;

            Time = _time;

            BuildRobotsOperations.OnStartOperation += StartBuildingRobots;
            BuildRobotsOperations.OnStopOperation += StopBuildingRobots;
            BuildRobotsOperations.OnRobotAdded += Time.AddTime;
            
        }

        private void Start()
        {
            DisplayNumberOfRobots();
        }


        private void StopBuildingRobots()
        {
            StopCoroutine("BuildingRobots");
            _once = false;

            Time.TimeTextState(false);
            Time.TotalTime = 0;
        }
        private void StartBuildingRobots()
        {
            if (_once == false)
            {
                Time.TimeTextState(true);
                StartCoroutine("BuildingRobots");
                _once = true;
            }
        }


        private void DisplayNumberOfRobots()
        {
            _numberOfRobots.text = string.Format("{0}/30", StoreRobots.RobotsTrained.Count);
        }


        public static void RecalculateTime()
        {
            Time.TotalTime = 0;

            foreach (Robot item in StoreRobots.RobotsInTraining)
            {
                Time.AddTime(item.buildTime);
            }

            Time.DecreaseTime((int)_tempTime);
        }
        private IEnumerator BuildingRobots()
        {
            _timeRemained.enabled = true;

            for (int i = 0; i < StoreRobots.RobotsInTraining.Count; i++)
            {
                if (RobotsInBuilding.robotsInBuildingIcons.Count > 0)
                {
                    _tempLoadingBar.MaxValue = StoreRobots.RobotsInTraining[i].buildTime;
                }

                _tempTime = 0;

                while (StoreRobots.RobotsInTraining[i] != null && _tempTime < StoreRobots.RobotsInTraining[i].buildTime)
                {
                    _tempTime += 1;

                    _tempLoadingBar.CurrentValue = (int)_tempTime;
                    Time.DisplayTime(_timeRemained, (int)(StoreRobots.RobotsInTraining[i].buildTime - _tempTime));
                    
                    yield return _time.ActivateTimer();
                }

                StoreRobots.RobotsTrained.Add(StoreRobots.RobotsInTraining[i]);

                GetRobotsTrained.RobotsBuilt = new List<Robot>(StoreRobots.RobotsTrained);

                DisplayNumberOfRobots();
                StoreRobots.RobotsInTraining.Remove(StoreRobots.RobotsInTraining[i]);

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
            BuildRobotsOperations.OnRobotAdded -= _time.AddTime;
        }
    }
}