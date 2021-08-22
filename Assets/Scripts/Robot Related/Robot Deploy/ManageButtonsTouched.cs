using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage all buttons of all robots
public class ManageButtonsTouched : MonoBehaviour
{
    public static void DisableOtherButtone(GameObject selecteButton)
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
}
