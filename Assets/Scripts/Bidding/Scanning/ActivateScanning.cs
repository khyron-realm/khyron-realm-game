using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Train;
using AuxiliaryClasses;
using DG.Tweening;
using CameraActions;
using Networking.Auctions;
using Networking.Mines;


namespace Bidding
{
    public class ActivateScanning : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private ScanMine _scanner;
        [SerializeField] private Button _button;
        [SerializeField] private ObjectPooling _poolOfObjects;
        [SerializeField] private TapGeneralPurpose _tap;
        #endregion


        #region "Private members"
        private int _scannCounts = 3;
        private bool _once = true;
        private Coroutine _coroute;
        #endregion


        private void Awake()
        {
            _button.onClick.AddListener(Scann);
            TapGeneralPurpose.OnTapDetected += ScanningInProcess;

            LoadSavedScans();
        }


        /// <summary>
        /// Load scans if exists for this mine
        /// </summary>
        private void LoadSavedScans()
        {
            foreach (MineScan item in AuctionsManager.CurrentAuctionRoom.Scans)
            {
                ScanningInProcess(new Vector3(item.X, item.Y, 0));
            }
        }


        /// <summary>
        /// Starts or Stops that scanning process based on touched button
        /// </summary>
        public void Scann()
        {
            if (_once)
            {
                _button.transform.DOScale(1.2f, 0.2f);
                _coroute = StartCoroutine(_tap.CheckFoScan());
            }
            else
            {
                _button.transform.DOScale(1, 0.2f);
                StopCoroutine(_coroute);
            }

            _once = !_once;
        }


        /// <summary>
        /// Coroutine that works while the scan button is pressed
        /// </summary>
        private void ScanningInProcess(Vector3 intPosition)
        {
            if (_scannCounts < 1) return;

            GameObject temp;
            Vector3Int position = new Vector3Int((int)intPosition.x, (int)intPosition.y, 0);

            Debug.LogWarning("Scanning");

            if (_scanner.Discover(position) == true)
            {                
                _scannCounts--;

                temp = _poolOfObjects.GetPooledObjects();
                temp.SetActive(true);

                temp.transform.position = new Vector3(intPosition.x + 0.5f, intPosition.y + 0.5f, -5f);

                Animations.AnimationForScan(temp, _button);
            }
            
            if (_scannCounts < 1)
            {
                _button.onClick.RemoveAllListeners();
                _button.enabled = false;
            }

            _button.transform.DOScale(1, 0.2f);
        }


        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            TapGeneralPurpose.OnTapDetected -= ScanningInProcess;
        }
    }
}