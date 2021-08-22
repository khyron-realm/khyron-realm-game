using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsHandler : MonoBehaviour
{
    // Scriptable object with data about the robots
    [SerializeField]
    private List<Robot> _robots;

    // Robot prefab [template]
    [SerializeField]
    private GameObject _robotPrefab;

    [SerializeField]
    private Vector2 _position;

    [SerializeField]
    private List<Sprite> _wayPoints;

    public static List<Sprite> WayPoints;

    private void Start()
    {
        WayPoints = _wayPoints;

        if (_robotPrefab == null || _wayPoints == null)
        {
            Debug.LogWarning("Robot prefab or SO, not added to the editor!! -- RobotsHandler/30");
        }
        else
        {
            // Pre Instatiate all available robots
            foreach (Robot item in _robots)
            {
                // Instatiate prefab
                GameObject tempRobot = Instantiate(_robotPrefab, _position, Quaternion.identity);

                // Set prefab to inactive until user deploy the robot
                tempRobot.SetActive(false);

                // Create button for robots
                CreateButtonForRobot.CreateButton(tempRobot);

                // Add custom property to every robot
                tempRobot.GetComponent<RobotManager>().robot = item;
            }
        }
    }
}