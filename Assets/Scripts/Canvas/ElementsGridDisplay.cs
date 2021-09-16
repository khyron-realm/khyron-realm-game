using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsGridDisplay : MonoBehaviour
{
    [Header("Panel/Canvas where buttons will be displayed")]
    [SerializeField] private GameObject _canvas;
    
    [Header("Button Prefab that will be instantiated")]
    [SerializeField]  private GameObject _buttonPrefab;

    //[Header("List with robots/resources that will be displayed on the grid")]
    //[SerializeField]  private RobotsForGrid _allRobotsList;

    //[Header("List with resources that will be displayed on the grid")]
    //[SerializeField] private RobotsForGrid _allResources;


    private void Awake()
    {
        //if(!_resources)
        //{
        //    foreach (Robot item in _allRobotsList.robotsForTraining)
        //    {
        //        GameObject newButton = Instantiate(_buttonPrefab);
        //        newButton.transform.SetParent(_canvas.transform, false);
        //    }
        //}
        //else
        //{
        //    //foreach (Robot item in _allRobotsList.robotsForTraining)
        //    //{
        //    //    GameObject newButton = Instantiate(_buttonPrefab);
        //    //    newButton.transform.SetParent(_canvas.transform, false);
        //    //}
        //}
    }
}
