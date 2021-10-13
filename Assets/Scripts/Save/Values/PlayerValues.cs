using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Save
{
    public class PlayerValues : MonoBehaviour, ISaveOperations
    {
        #region "Public members"
        [HideInInspector] public string Username;
        [HideInInspector] public string Password;
        #endregion

        private void Awake()
        {
            LoadData();
        }


        /// <summary>
        /// Load data about user credentials
        /// </summary>
        public void LoadData()
        {
            PlayerData temp = SaveSystem.LoadPlayerData();
            
            if (temp != null)
            {
                Username = temp.UserName;
                Password = temp.Password;
            }    
        }


        /// <summary>
        /// Saves data about user credentials
        /// </summary>
        public void SaveData()
        {
            SaveSystem.SavePlayerData(this);
        }
    }
}