using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mine;
using Bidding;

namespace Save
{
    // Creates Mine Values
    public class MineValues : MonoBehaviour, ISaveOperations
    {
        #region "Input data"
        [SerializeField] private RefreshMineValues _refreshValues;
        #endregion

        #region "Public members"
        [HideInInspector] public int HiddenSeed;
        [HideInInspector] public List<ResourcesData> ResourcesData;
        #endregion

        private void Awake()
        {
            LoadData();
        }

        /// <summary>
        /// Refreshes Mine data and saves it
        /// </summary>
        public void Refresh()
        {
            _refreshValues.DoTheRefresh(ref HiddenSeed, ref ResourcesData);
            GetComponent<StartAuction>().Restart();

            SaveData();
        }


        /// <summary>
        /// Saves the data to binary format
        /// </summary>
        public void SaveData()
        {
            SaveSystem.SaveMineData(this, gameObject);
        }


        /// <summary>
        /// Loads specific data from binary format
        /// </summary>
        public void LoadData()
        {
            MineData temp = SaveSystem.LoadMineData(gameObject);

            if(temp == null)
            {
                Refresh();
            }
            else
            {
                HiddenSeed = temp.HiddenSeed;
                ResourcesData = new List<ResourcesData>(temp.ResourcesData);
            }          
        }
    }
}