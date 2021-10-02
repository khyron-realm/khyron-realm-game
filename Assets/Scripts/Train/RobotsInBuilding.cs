using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panels;


namespace Manager.Train
{
    public class RobotsInBuilding : MonoBehaviour
    {
        // List with icons
        public static List<GameObject> robotsInBuildingIcons;

        // elements of the icon
        public static ProgressBar timeBar;
        public static Text timeBarText;

        private void Awake()
        {
            robotsInBuildingIcons = new List<GameObject>();
        }
    }
}