using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Train;
using DG.Tweening;

public class ActivateScanning : MonoBehaviour
{
    private int _scannCounts = 3;
    private bool _once = true;

    [SerializeField] private ScanMine _scanner;
    [SerializeField] private Button _button;

    [SerializeField] private ObjectPooling _poolOfObjects;

    private Coroutine _coroute;

    private void Awake()
    {
        _button.onClick.AddListener(Scann);
    }


    public void Scann()
    {
        if(_once)
        {
            _button.transform.DOScale(1.2f, 0.2f);
            _coroute = StartCoroutine(ScanningInProcess());
        }
        else
        {
            _button.transform.DOScale(1, 0.2f);
            StopCoroutine(_coroute);
        }

        _once = !_once;    
    }


    private IEnumerator ScanningInProcess()
    {
        while(_scannCounts > 0)
        {
            if(Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    Vector3 position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    Vector3Int intPosition = new Vector3Int((int)position.x, (int)position.y, 0);

                    if (_scanner.Discover(intPosition) == true)
                    {
                        _scannCounts--;

                        GameObject temp = _poolOfObjects.GetPooledObjects();

                        temp.SetActive(true);

                        Sequence mySequenceOne = DOTween.Sequence();
                        Sequence mySequenceTwo = DOTween.Sequence();

                        temp.transform.position = new Vector3(intPosition.x + 0.5f, intPosition.y + 0.5f, intPosition.z);
                      
                        mySequenceOne.Append(temp.transform.DOLocalRotate(new Vector3(0, 0, 360 * 3), 2f, RotateMode.FastBeyond360));
                        mySequenceOne.OnComplete(() => { temp.SetActive(false); });
                        
                        mySequenceTwo.Append(_button.image.DOColor(Color.green, 0.2f));                       
                        mySequenceTwo.Append(_button.image.DOColor(Color.white, 0.2f));
                    }
                }
            }
            yield return null;
        }

        if(_scannCounts < 1)
        {
            _button.onClick.RemoveAllListeners();
            _button.enabled = false;
        }

        _button.transform.DOScale(1, 0.2f);
    }


    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}