using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateScanning : MonoBehaviour
{
    private int _scannCounts = 3;
    private bool _once = true;

    [SerializeField] private ScanMine _scanner;
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(Scann);
    }


    public void Scann()
    {
        if(_once)
        {
            StartCoroutine(ScanningInProcess());
        }
        else
        {
            StopCoroutine(ScanningInProcess());
        }

        _once = !_once;    
    }


    private IEnumerator ScanningInProcess()
    {
        while(_scannCounts > 0)
        {
            if(Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Vector3 position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    Vector3Int intPosition = new Vector3Int((int)position.x, (int)position.y, 0);

                    _scanner.Discover(intPosition);
                    _scannCounts--;
                }
            }
            yield return null;
        }

        if(_scannCounts < 1)
        {
            _button.onClick.RemoveAllListeners();
            _button.enabled = false;
        }      
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}