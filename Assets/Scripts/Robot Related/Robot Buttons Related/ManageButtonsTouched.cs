using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Manage all buttons of all robots
namespace RobotButtonInteractions
{
    public class ManageButtonsTouched : MonoBehaviour
    {
        public static void DeselectOtherButtons(GameObject selecteButton)
        {
            foreach (GameObject item in CreateButtonForRobot.buttons)
            {
                if (selecteButton != item)
                {
                    item.GetComponent<DeployRobot>().DeselectRobot();
                    item.GetComponent<SelectedButton>().DeselectRobot();
                    item.GetComponent<ManagePreviewOfPath>().DisablePath();
                }
            }
        }


        public static void DeselectAllButtons()
        {
            foreach (GameObject item in CreateButtonForRobot.buttons)
            {
                item.GetComponent<DeployRobot>().DeselectRobot();
                item.GetComponent<SelectedButton>().DeselectRobot();
                item.GetComponent<ManagePreviewOfPath>().DisablePath();
            }
        }


        public static void DisableAllFocusAnimations(GameObject selecteButton)
        {
            foreach (GameObject item in CreateButtonForRobot.buttons)
            {
                if (selecteButton != item)
                {
                    item.GetComponent<MoveCameraToRobot>().StopMoveCamera();
                }
            }
        }


        public static void DisableButtons(bool active)
        {
            foreach (GameObject item in CreateButtonForRobot.buttons)
            {
                if(active)
                {
                    item.GetComponent<Button>().enabled = true;
                }
                else
                {
                    item.GetComponent<Button>().enabled = false;
                }
            }
        }
    }
}