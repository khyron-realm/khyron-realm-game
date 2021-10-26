using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AuxiliaryClasses;


namespace Manager.Train
{
    public class RobotsInBuildingOperations : MonoBehaviour
    {
        [SerializeField] private ObjectPooling _object;

        private static ObjectPooling s_objectPooling;

        private void Awake()
        {
            s_objectPooling = _object;
        }

        #region "Icons Operations"
        public static GameObject CreateIconInTheRightForRobotInBuilding(RobotSO robot)
        {
            GameObject newRobotToCreate = s_objectPooling.GetPooledObjects();

            if (newRobotToCreate != null)
            {
                RobotsInBuilding.robotsInBuildingIcons.Add(newRobotToCreate);

                newRobotToCreate.SetActive(true);
                newRobotToCreate.transform.SetSiblingIndex(RobotsInBuilding.robotsInBuildingIcons.Count - 1);
                newRobotToCreate.GetComponent<Image>().sprite = robot.Icon;

                newRobotToCreate.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(
                    delegate
                    {
                        BuildRobotsOperations.CancelBuildRobot(robot, newRobotToCreate);
                    });
            }

            return newRobotToCreate;
        }
        public static void DezactivateIcon(GameObject robotIcon)
        {
            robotIcon.SetActive(false);
            robotIcon.transform.SetSiblingIndex(RobotsInBuilding.robotsInBuildingIcons.Count);
            robotIcon.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        }
        #endregion
    }
}