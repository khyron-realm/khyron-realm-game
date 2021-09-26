using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class CreateMinesOnTheMiniMap : MonoBehaviour
    { 
        private void Awake()
        {
            
        }


        public void Refresh()
        {
            

        }
         
        public int GenerateRandomValues()
        {
            return Random.Range(-10000, 10000);
        }
    }
}