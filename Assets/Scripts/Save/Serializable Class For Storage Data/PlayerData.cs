using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Save
{
    [System.Serializable]
    public class PlayerData
    {
        public string UserName;
        public string Password;

        public PlayerData(PlayerValues values)
        {
            UserName = values.Username;
            Password = values.Password;
        }
    }
}