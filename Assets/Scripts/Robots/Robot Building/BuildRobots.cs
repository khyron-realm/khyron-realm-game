using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manager.Train
{
    public class BuildRobots : MonoBehaviour
    {
        [SerializeField] private Text _numberOfRobots;
        [SerializeField] private Timer _time;

        [Space(20f)]

        [SerializeField] private Text _timeRemained;
        [SerializeField] private ProgressBar _tempLoadingBar;

        private bool _once = false;
        private static float _tempTime = 0;

        public static Timer time;

        private void Awake()
        {
            GetRobotsTrained.RobotsBuilt = new List<Robot>();

            _timeRemained.text = "";
            _tempLoadingBar.MaxValue = 1;
            _tempLoadingBar.CurrentValue = 1;

            time = _time;

            StoreTrainRobotsOperations.OnStartOperation += StartBuildingRobots;
            StoreTrainRobotsOperations.OnStopOperation += StopBuildingRobots;
            StoreTrainRobotsOperations.OnRobotAdded += _time.AddTime;
            
        }

        private void Start()
        {
            DisplayNumberOfRobots();
        }


        private void StopBuildingRobots()
        {
            StopCoroutine("BuildingRobots");
            _once = false;
            time.TimeTextState(false);

            // Reset Time
            time.TotalTime = 0;
        }
        private void StartBuildingRobots()
        {
            if (_once == false)
            {
                _time.TimeTextState(true);
                StartCoroutine("BuildingRobots");
                _once = true;
            }
        }


        private void DisplayNumberOfRobots()
        {
            _numberOfRobots.text = string.Format("{0}/30", StoreTrainRobots.RobotsTrained.Count);
        }


        public static void RecalculateTime()
        {
            time.TotalTime = 0;

            foreach (Robot item in StoreTrainRobots.RobotsInTraining)
            {
                time.AddTime(item.buildTime);
            }

            time.DecreaseTime((int)_tempTime);
        }
        private IEnumerator BuildingRobots()
        {
            _timeRemained.enabled = true;

            for (int i = 0; i < StoreTrainRobots.RobotsInTraining.Count; i++)
            {
                if (ManageIcons.robotsInBuildingIcons.Count > 0)
                {
                    _tempLoadingBar.MaxValue = StoreTrainRobots.RobotsInTraining[i].buildTime;
                }

                _tempTime = 0;

                while (StoreTrainRobots.RobotsInTraining[i] != null && _tempTime < StoreTrainRobots.RobotsInTraining[i].buildTime)
                {
                    _tempTime += 1;

                    _tempLoadingBar.CurrentValue = (int)_tempTime;
                    time.DisplayTime(_timeRemained, (int)(StoreTrainRobots.RobotsInTraining[i].buildTime - _tempTime));
                    
                    yield return _time.ActivateTimer();
                }

                StoreTrainRobots.RobotsTrained.Add(StoreTrainRobots.RobotsInTraining[i]);

                GetRobotsTrained.RobotsBuilt = new List<Robot>(StoreTrainRobots.RobotsTrained);

                DisplayNumberOfRobots();
                StoreTrainRobots.RobotsInTraining.Remove(StoreTrainRobots.RobotsInTraining[i]);

                ManageIconsDuringTraining.DezactivateIcon(ManageIcons.robotsInBuildingIcons[i]);

                ManageIcons.robotsInBuildingIcons.RemoveAt(i);
                i--;
            }

            _once = false;
            time.TimeTextState(false);

            // last robot loading bar
            _timeRemained.enabled = false;
            _tempLoadingBar.MaxValue = 1;
        }


        private void OnDestroy()
        {
            StoreTrainRobotsOperations.OnStartOperation -= StartBuildingRobots;
            StoreTrainRobotsOperations.OnStopOperation -= StopBuildingRobots;
            StoreTrainRobotsOperations.OnRobotAdded -= _time.AddTime;
        }
    }
}