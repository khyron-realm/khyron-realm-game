using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage all buttons of all robots
public class ManageButtonsTouched : MonoBehaviour
{
    public static void DisableOtherButtons(GameObject selecteButton)
    {
        foreach (GameObject item in CreateButtonForRobot.buttons)
        {
            if(selecteButton != item)
            {
                item.GetComponent<DeployRobot>().DeselectRobot();
                item.GetComponent<SelectedButton>().DeselectRobot();
                item.GetComponent<ManagePreviewOfPath>().DisablePath();
            }
        }
    }

    public static void DisableAllButtons()
    {
        foreach (GameObject item in CreateButtonForRobot.buttons)
        {
            item.GetComponent<DeployRobot>().DeselectRobot();
            item.GetComponent<SelectedButton>().DeselectRobot();
            item.GetComponent<ManagePreviewOfPath>().DisablePath();
        }
    }
}