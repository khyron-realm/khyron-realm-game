using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Manager.Train
{
    public class ManageIcons : MonoBehaviour
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