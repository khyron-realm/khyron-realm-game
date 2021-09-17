using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoreAndTrainRobots : MonoBehaviour
{
    [SerializeField] private int _robotsLimit;
    [SerializeField] private RobotsManagerUI _managerUI;

    [SerializeField] private GameObject _canvasForRobotsInTrain;
    [SerializeField] private Text _timeText;

    // Gameobjects that represent the robots in training from the right side
    private List<GameObject> _robotsInBuildingIcons;

    // Robots in training
    public static List<Robot> robotsInTraining;
    public static List<Robot> robotsTrained;

    // Total time of building
    public static int totalTime;

    // Check if coroutine started once
    private bool _once = false;
    private ProgressBar _timeBar;
    private Text _timeBarText;

    private void Awake()
    {
        totalTime = 0;

        robotsInTraining = new List<Robot>();
        _robotsInBuildingIcons = new List<GameObject>();

        robotsTrained = new List<Robot>();

        _managerUI.OnButtonPressed += AddRobotsToBuild;
    }


    // Add and remove methods for Robots to building process
    private void AddRobotsToBuild(Robot robot)
    {
        if (robotsTrained.Count + robotsInTraining.Count < _robotsLimit)
        {
            robotsInTraining.Add(robot);

            TimeIncrement(robot.buildTime);

            CreateIconInTheRightForRobotInBuilding(robot);

            if (robotsInTraining.Count > 0)
            {
                StartBuildingRobots();
            }
        }
    }
    private void RemoveRobotsToBuild(Robot robot, GameObject robotIcon)
    {
        if (robotIcon == _robotsInBuildingIcons[0])
        {
            StopCoroutine("BuildRobots");

            robotsInTraining.Remove(robot);
            _robotsInBuildingIcons.Remove(robotIcon);

            StartCoroutine("BuildRobots");
        }
        else
        {
            robotsInTraining.Remove(robot);
            _robotsInBuildingIcons.Remove(robotIcon);
        }
     
        RecalculateTime(_timeText);
        DezactivateIcon(robotIcon);

        if (_robotsInBuildingIcons.Count > 0)
        {
            ActivateDezactivateIconLoadingBar(_robotsInBuildingIcons[0], true);
        }

        if (robotsInTraining.Count < 1)
        {
            StopCoroutine("BuildRobots");
            _once = false;
            totalTime = 0;
        }
    }


    // Create icon for the robot in the right
    private void CreateIconInTheRightForRobotInBuilding(Robot robot)
    {
        GameObject newRobotToCreate = ObjectPoolingTrainingRobots.SharedInstance.GetPooledObjects();

        if (newRobotToCreate != null)
        {
            _robotsInBuildingIcons.Add(newRobotToCreate);

            newRobotToCreate.SetActive(true);
            newRobotToCreate.transform.SetSiblingIndex(_robotsInBuildingIcons.Count - 1);
            newRobotToCreate.GetComponent<Image>().sprite = robot.icon;

            newRobotToCreate.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(
                delegate
                {
                    RemoveRobotsToBuild(robot, newRobotToCreate);
                });

            ActivateDezactivateIconLoadingBar(newRobotToCreate, false);
        }
    }
    private static void ActivateDezactivateIconLoadingBar(GameObject newRobotToCreate, bool allocation)
    {
        newRobotToCreate.transform.GetChild(1).gameObject.SetActive(allocation);
        newRobotToCreate.transform.GetChild(2).gameObject.SetActive(allocation);
    }
    private void DezactivateIcon(GameObject robotIcon)
    {
        robotIcon.SetActive(false);
        robotIcon.transform.SetSiblingIndex(_robotsInBuildingIcons.Count);
        robotIcon.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
    }


    // Time remained for building robots
    private static void RecalculateTime(Text _timeText)
    {
        totalTime = 0;

        foreach (Robot item in robotsInTraining)
        {
            totalTime += item.buildTime;
        }

        _timeText.text = totalTime.ToString();
    }
    private void TimeIncrement(int time)
    {
        totalTime += time;
        _timeText.text = totalTime.ToString();
    }
    private void TimeDecrement(int time)
    {
        totalTime -= time;
        _timeText.text = totalTime.ToString();
    }


    // Building process for robots
    private void StartBuildingRobots()
    {
        if (_once == false)
        {
            StartCoroutine("BuildRobots");
            _once = true;
        }
    }
    private IEnumerator BuildRobots()
    {
        for (int i = 0; i < robotsInTraining.Count; i++)
        {
            if(_robotsInBuildingIcons.Count > 0)
            {
                ActivateDezactivateIconLoadingBar(_robotsInBuildingIcons[0], true);

                _timeBarText = _robotsInBuildingIcons[0].transform.GetChild(1).GetComponent<Text>();
                _timeBar = _robotsInBuildingIcons[0].transform.GetChild(2).GetComponent<ProgressBar>();
               
                _timeBar.MaxValue = robotsInTraining[i].buildTime;
            }

            float temp = 0;

            while (robotsInTraining[i] != null && temp < robotsInTraining[i].buildTime)
            {
                temp += 1;
                _timeBar.CurrentValue = (int)temp;
                _timeBarText.text = (robotsInTraining[i].buildTime - temp).ToString();

                TimeDecrement(1);

                yield return new WaitForSeconds(1f);
            }

            robotsTrained.Add(robotsInTraining[i]);
            robotsInTraining.Remove(robotsInTraining[i]);

            DezactivateIcon(_robotsInBuildingIcons[i]);

            _robotsInBuildingIcons.RemoveAt(i);
            i--;
        }

        _once = false;
    }
}