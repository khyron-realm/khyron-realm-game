using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panels;
using Networking.Headquarters;
using Networking.Auctions;
using PlayerDataUpdate;
using Levels;


namespace Manager.Store
{
    public class EnergyOperationForAuction : MonoBehaviour
    {
        [SerializeField] private ProgressBar _energyBar;

        private Coroutine energy;


        private void Awake()
        {
            HeadquartersManager.OnPlayerDataReceived += InitAll;
            PlayerDataOperations.OnEnergyModified += EnergyUpdate;
            AuctionsManager.OnOverbid += EnergyUpdate;
            AuctionsManager.OnSuccessfulAddBid += EnergyUpdate;
        }


        private void InitAll()
        {
            _energyBar.MaxValue = (int)LevelMethods.MaxEnergyCount(HeadquartersManager.Player.Level);
            _energyBar.CurrentValue = (int)HeadquartersManager.Player.Energy;
        }


        private void EnergyUpdate(Bid bid)
        {
            if (energy != null)
                StopCoroutine(energy);
            energy = StartCoroutine(BarAnimation(_energyBar, (int)HeadquartersManager.Player.Energy));
        }
        private void EnergyUpdate(byte tag)
        {
            if (energy != null)
                StopCoroutine(energy);
            energy = StartCoroutine(BarAnimation(_energyBar, (int)HeadquartersManager.Player.Energy));
        }


        public static IEnumerator BarAnimation(ProgressBar bar, int endGoal)
        {
            float temp = 0f;
            while (temp < 1)
            {
                temp += Time.deltaTime * 0.3f;
                bar.CurrentValue = Mathf.RoundToInt(Mathf.Lerp(bar.CurrentValue, endGoal, temp));
                yield return null;
            }
        }


        private void OnDestroy()
        {
            HeadquartersManager.OnPlayerDataReceived -= InitAll;
            PlayerDataOperations.OnEnergyModified -= EnergyUpdate;
            AuctionsManager.OnOverbid -= EnergyUpdate;
            AuctionsManager.OnSuccessfulAddBid -= EnergyUpdate;
        }
    }
}