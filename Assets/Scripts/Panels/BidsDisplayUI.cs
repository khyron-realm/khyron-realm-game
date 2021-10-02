using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using ObjectPool;

namespace Panels
{
    public class BidsDisplayUI : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private ObjectPooling _objectsToPoll;
        [SerializeField] private GameObject _content;
        #endregion

        private void Awake()
        {
            Confirm.OnAccepted += AddBidToList;
        }


        /// <summary>
        /// Add user bid to the list as the last one
        /// </summary>
        /// <param name="amount"> Amount bidded </param>
        private void AddBidToList(int amount)
        {
            GameObject newPriceAddedToBid = _objectsToPoll.GetPooledObjects();

            if (newPriceAddedToBid != null)
            {
                newPriceAddedToBid.SetActive(true);
                newPriceAddedToBid.transform.SetSiblingIndex(0);
                newPriceAddedToBid.transform.GetChild(0).GetComponent<Text>().text = amount.ToString();
            }

            // Set last Child to inactive
            _content.transform.GetChild(5).transform.gameObject.SetActive(false);
        }


        private void OnDestroy()
        {
            Confirm.OnAccepted -= AddBidToList;
        }
    }
}