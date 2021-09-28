using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;

namespace Save
{
    // Creates Mine Values
    public class MineValues : MonoBehaviour
    {
        public int HiddenSeed;

        [HideInInspector]
        public List<ResourcesData> ResourcesData;
        

        private void Awake()
        {
            LoadData();
        }


        // Refresh the values
        public void Refresh()
        {
            HiddenSeed = GenerateRandomValuesForSeeds();

            ResourcesData.Clear();
            ResourcesData.Add(new ResourcesData(0, 0, 0));
            ResourcesData.Add(new ResourcesData(0, 0, 0));
            ResourcesData.Add(new ResourcesData(0, 0, 0));

            for (int i = 0; i < ResourcesData.Count; i++)
            {
                ResourcesData[i].Seed = GenerateRandomValuesForSeeds();
                ResourcesData[i].RarityCoeficient = GenerateRandomValuesForCoeficients();
                ResourcesData[i].Frequency = GenerateRandomValuesForCoeficients();
            }

            SaveData();
        }

        public static int GenerateRandomValuesForSeeds()
        {
            return Random.Range(-99999, 99999);
        }


        public static float GenerateRandomValuesForCoeficients()
        {
            return Random.Range(0.18f, 0.24f);
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