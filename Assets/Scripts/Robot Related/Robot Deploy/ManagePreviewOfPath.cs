using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// If button is selected the path of the robot with the waypoint is visible
/// 
/// </summary>
public class ManagePreviewOfPath : MonoBehaviour
{
    private GameObject _robot;

    private Button _button;

    public GameObject Robot
    {
        set
        {
            _robot = value;
        }
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        GetComponent<DeployRobot>().OnDeployed += AddListener;
    }

    private void AddListener()
    {
        _button.onClick.AddListener(EnablePath);
        EnablePath();
    }

    private void EnablePath()
    {
        _robot.transform.GetChild(2).GetComponent<LineRenderer>().enabled = true;
        _robot.GetComponent<RobotManager>().commandBlock.SetActive(true);
        _robot.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void DisablePath()
    {
        _robot.transform.GetChild(2).GetComponent<LineRenderer>().enabled = false;
        _robot.GetComponent<RobotManager>().commandBlock.SetActive(false);
        _robot.GetComponent<SpriteRenderer>().color = Color.white;
    }
}