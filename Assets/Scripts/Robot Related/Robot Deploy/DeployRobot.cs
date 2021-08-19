using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployRobot : MonoBehaviour
{
    // Robot to deploy
    private GameObject _robot;

    // Button asociated with the robot
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
        _button.onClick.AddListener(SelectRobot);
        _button.onClick.AddListener(StartDeployOperation);
    }

    private void SelectRobot()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1);
        ManageButtonsTouched.DisableOtherButtone(gameObject);
    }

    public void DeselectRobot()
    {
        transform.localScale = new Vector3(1f, 1f, 1);
        StopCoroutine("Deploy");
    }

    private void StartDeployOperation()
    {
        StartCoroutine("Deploy");
    }


    private IEnumerator Deploy()
    {
        bool check = true;
        LayerMask layerMask = LayerMask.GetMask("Blocks");

        while (check)
        {
            Collider2D temp = UserTouch.DetectColliderTouched(layerMask);

            if (temp != null && UserTouch.TouchPhaseEnded(0))
            {
                temp.GetComponent<MeshFilter>().mesh = null;
                temp.GetComponent<HealthManager>().DoDamage(999999);

                _robot.SetActive(true);

                _robot.GetComponent<RobotManager>().commandBlock.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, -6f);
                _robot.transform.position = temp.transform.position;

                _button.onClick.RemoveListener(StartDeployOperation);
                
                check = false;
            }
            yield return null;
        }
    }
}