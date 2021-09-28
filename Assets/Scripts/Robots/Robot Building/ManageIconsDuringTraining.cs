using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Manager.Train
{
    public class ManageIconsDuringTraining : MonoBehaviour
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
                ManageIcons.robotsInBuildingIcons.Add(newRobotToCreate);

                newRobotToCreate.SetActive(true);
                newRobotToCreate.transform.SetSiblingIndex(ManageIcons.robotsInBuildingIcons.Count - 1);
                newRobotToCreate.GetComponent<Image>().sprite = robot.icon;

                newRobotToCreate.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(
                    delegate
                    {
                        StoreTrainRobotsOperations.RemoveRobotsToBuild(robot, newRobotToCreate);
                    });
            }
        }

        public static void DezactivateIcon(GameObject robotIcon)
        {
            robotIcon.SetActive(false);
            robotIcon.transform.SetSiblingIndex(ManageIcons.robotsInBuildingIcons.Count);
            robotIcon.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
}