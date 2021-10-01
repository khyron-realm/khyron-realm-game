using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObjectPool;


namespace Manager.Train
{
    public class RobotsInBuildingOperations : MonoBehaviour
    {
        [SerializeField] private ObjectPooling _object;

        private static ObjectPooling ObjectPooling;

        private void Awake()
        {
            ObjectPooling = _object;
        }

        public static void CreateIconInTheRightForRobotInBuilding(Robot robot)
        {
            GameObject newRobotToCreate = ObjectPooling.GetPooledObjects();

            if (newRobotToCreate != null)
            {
                RobotsInBuilding.robotsInBuildingIcons.Add(newRobotToCreate);

                newRobotToCreate.SetActive(true);
                newRobotToCreate.transform.SetSiblingIndex(RobotsInBuilding.robotsInBuildingIcons.Count - 1);
                newRobotToCreate.GetComponent<Image>().sprite = robot.icon;

                newRobotToCreate.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(
                    delegate
                    {
                        BuildRobotsOperations.RemoveRobotsToBuild(robot, newRobotToCreate);
                    });
            }
        }
        public static void DezactivateIcon(GameObject robotIcon)
        {
            robotIcon.SetActive(false);
            robotIcon.transform.SetSiblingIndex(RobotsInBuilding.robotsInBuildingIcons.Count);
            robotIcon.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
}