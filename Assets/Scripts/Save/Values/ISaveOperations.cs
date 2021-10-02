using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Save
{
    public interface ISaveOperations
    {
        public void SaveData();
        public void LoadData();
    }

}