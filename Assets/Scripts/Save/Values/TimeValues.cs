using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Save;

namespace Save
{
    public class TimeValues : MonoBehaviour, ISaveOperations
    {
        #region "Public members"
        [HideInInspector] public int TimeTillFinished;
        #endregion

        private void Awake()
        {
            LoadData();
        }

        /// <summary>
        /// Saves data about the time
        /// </summary>
        public void SaveData()
        {
            SaveSystem.SaveTimeData(this, gameObject);
        }


        /// <summary>
        /// Load data about the time
        /// </summary>
        public void LoadData()
        {
            TimeData temp = SaveSystem.LoadTimeData(gameObject);

            if (temp != null)
            {
                TimeTillFinished = temp.TimeTillFinish;
            }
            else
            {
                SaveData();
            }
        }
    }
}