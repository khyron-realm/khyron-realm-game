using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageButtonsTouched : MonoBehaviour
{
    public static void DisableOtherButtone(GameObject selecteButton)
    {
        foreach (GameObject item in CreateButtonForRobot.buttons)
        {
            if(selecteButton != item)
            {
                item.GetComponent<DeployRobot>().DeselectRobot();
            }
        }
    }
}
