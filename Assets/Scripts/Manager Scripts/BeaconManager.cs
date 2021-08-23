using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _beacon;

    private void Awake()
    {
        GameObject temp = Instantiate(_beacon);
        temp.SetActive(false);

        DeployBeacon.beacon = temp;
    }
}