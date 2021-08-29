using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobotButtonInteractions;

public class RobotsManager : MonoBehaviour
{
    // Scriptable object with data about the robots
    [SerializeField]
    [Header("Robots that will be deployed in the mine")]
    private List<Robot> _robots;

    // Robot prefab [template]
    [SerializeField]
    [Header("Prefab of the robot")]
    private GameObject _robotPrefab;

    [SerializeField]
    [Header("Initial Deploy position of robots after they are instanced")]
    private Vector2 _position;

    [SerializeField]
    [Header("Waypoints Sprites")]
    private List<Sprite> _wayPoints;

    public static List<Sprite> WayPoints;

    public static List<GameObject> robots;

    private void Awake()
    {
        WayPoints = _wayPoints;
        robots = new List<GameObject>();
    }

    private void Start()
    {
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

                robots.Add(tempRobot);

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