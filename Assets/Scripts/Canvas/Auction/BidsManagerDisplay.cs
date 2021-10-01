using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using ObjectPool;
using Panels;

public class BidsManagerDisplay : MonoBehaviour
{
    [SerializeField] private ObjectPooling _objectsToPoll;
    [SerializeField] private GameObject _content;

    private void Awake()
    {
        Confirm.OnBidAccepted += AddBidToList;
    }


    private void AddBidToList(int amount)
    {
        GameObject newPriceAddedToBid = _objectsToPoll.GetPooledObjects();

        if (newPriceAddedToBid != null)
        {          
            newPriceAddedToBid.SetActive(true);
            newPriceAddedToBid.transform.SetSiblingIndex(0);
            newPriceAddedToBid.transform.GetChild(0).GetComponent<Text>().text = amount.ToString();
        }

        _content.transform.GetChild(5).transform.gameObject.SetActive(false);
    }


    private void OnDestroy()
    {
        Confirm.OnBidAccepted -= AddBidToList;
    }
}