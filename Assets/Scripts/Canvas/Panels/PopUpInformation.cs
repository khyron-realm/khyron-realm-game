using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Store;


public class PopUpInformation : MonoBehaviour
{
    [SerializeField] private Text _text;

    private bool _once = false;
    private float _time;

    private void Awake()
    {
        _text.enabled = false;
        ManageResourcesOperations.OnNotEnoughResources += StartShowingPopUp;
    }


    private void StartShowingPopUp()
    {
        if(_once == false)
        {
            _once = true;
            StartCoroutine(ShowPopUp());            
        }
        else
        {
            _time = 0;
            StartCoroutine(FontChange());
        }
    }


    private IEnumerator ShowPopUp()
    {
        _text.enabled = true;

        _time = 0;
        while(_time < 3)
        {
            _time += Time.deltaTime;
            yield return null;
        }

        _once = false;
        _text.enabled = false;
    }
    private IEnumerator FontChange()
    {
        float temp = 0;
        while (temp < 1)
        {
            temp += Time.deltaTime * 12;
            _text.fontSize = (int)Mathf.Lerp(64, 84, temp);

            yield return null;
        }

        temp = 0;
        while (temp < 1)
        {
            temp += Time.deltaTime * 12;
            _text.fontSize = (int)Mathf.Lerp(84, 64, temp);

            yield return null;
        }
    }
}