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
    public List<Sprite> _wayPoints;

    public static List<Sprite> WayPoints;

    void Awake()
    {
        WayPoints = _wayPoints;

        if (_robotPrefab == null || _wayPoints == null)
        {
            Debug.LogWarning("Robot prefab or SO, not added to the editor!! -- RobotsHandler/18");
        }
        else
        {
            // Pre Instatiate all available robots
            foreach (Robot item in _robots)
            {
                // Instatiate prefab
                Instantiate(_robotPrefab, _position, Quaternion.identity);

                // Set prefab to inactive until user deploy the robot
                _robotPrefab.SetActive(true);

                // Add custom property to every robot
                _robotPrefab.GetComponent<RobotManager>().robot = item;
            }
        }
    }
}