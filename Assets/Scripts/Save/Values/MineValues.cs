using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;

namespace Save
{
    // Creates Mine Values
    public class MineValues : MonoBehaviour
    {
        [SerializeField] private RefreshMineValues _refreshValues;

        [HideInInspector] public int HiddenSeed;
        [HideInInspector] public List<ResourcesData> ResourcesData;
        
        private void Awake()
        {
            LoadData();
        }

        public void Refresh()
        {
            _refreshValues.DoTheRefresh(ref HiddenSeed, ref ResourcesData);
            GetComponent<StartAuction>().Restart();

            SaveData();
        }


        // Saves Data to file
        public void SaveData()
        {
            SaveSystem.SaveMineData(this, gameObject);
        }


        //Load data from files
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