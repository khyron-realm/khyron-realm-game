using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManageStatesOfScreen
{
    public static bool ScreenToDeployUser = false;

    public static void ChangeState()
    {
        ScreenToDeployUser = true;
    }
}