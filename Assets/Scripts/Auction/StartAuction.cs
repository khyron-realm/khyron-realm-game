using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAuction : MonoBehaviour
{
    [SerializeField] public int _totalTimeOfAuction;

    public Text _timeText;

    private void Start()
    {     
        //StartCoroutine(AuctionInProgress());
    }


    private IEnumerator AuctionInProgress()
    {
        int temp = _totalTimeOfAuction;
       
        while (temp > 1)
        {
            temp -= 1;

            if(_timeText != null)
                _timeText.text = _totalTimeOfAuction.ToString();

            yield return new WaitForSeconds(1f);
        }       
    }
}