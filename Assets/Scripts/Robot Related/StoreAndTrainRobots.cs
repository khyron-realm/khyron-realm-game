using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoreAndTrainRobots : MonoBehaviour
{
    //
    [SerializeField] private int _robotsLimit;
    [SerializeField] private RobotsManagerUI _managerUI;

    // 
    [SerializeField] private GameObject _canvasForRobotsInTrain;

    // Gameobjects that represent the robots in training from the right side
    private List<GameObject> _robotsInBuildingIcons;

    // Robots in training
    public static List<Robot> robotsInTraining;

    // Robots trained
    public static List<Robot> robotsTrained;
    public static int totalTime;

    private bool _once = false;
   
    private void Awake()
    {
        totalTime = 0;

        robotsInTraining = new List<Robot>();
        _robotsInBuildingIcons = new List<GameObject>();

        robotsTrained = new List<Robot>();

        _managerUI.OnButtonPressed += AddRobotsToBuild;
    }

    private void AddRobotsToBuild(Robot robot)
    {
        if(robotsTrained.Count + robotsInTraining.Count < _robotsLimit)
        {
            robotsInTraining.Add(robot);
            totalTime += robot.buildTime;

            GameObject newRobotToCreate = ObjectPoolingTrainingRobots.SharedInstance.GetPooledObjects();

            if (newRobotToCreate != null)
            {
                newRobotToCreate.SetActive(true);
                _robotsInBuildingIcons.Add(newRobotToCreate);
                newRobotToCreate.GetComponent<Image>().sprite = robot.icon;
            }

            if (robotsInTraining.Count > 0)
            {
                TrainRobots();
            }
        } 
    }


    private void RemoveRobotsToBuild(Robot robot)
    {
        robotsInTraining.Remove(robot);
        totalTime -= robot.buildTime;
    }


    private void TrainRobots()
    {
        if(_once == false)
        {
            StartCoroutine("BuildRobots");
            _once = true;
        }
    }
    

    private IEnumerator BuildRobots()
    {
        for(int i = 0; i < robotsInTraining.Count; i++)
        {
            float temp = 0;
            while(temp < robotsInTraining[i].buildTime)
            {
                temp += 1;
                totalTime -= 1;
                yield return new WaitForSeconds(1f);
            }

            robotsTrained.Add(robotsInTraining[i]);
            robotsInTraining.RemoveAt(i);

            _robotsInBuildingIcons[i].SetActive(false);
            _robotsInBuildingIcons.RemoveAt(i);
            i--;
        }

        _once = false;
    }
}