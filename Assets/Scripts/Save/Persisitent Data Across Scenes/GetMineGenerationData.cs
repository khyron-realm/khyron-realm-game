using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Save;

namespace Mine
{
    // Static class that PERSIST during the aplication runtime 
    // The static values remain the same even if you change the scenes
    // Class must not inherit from MonoBehaviour
    // Used for comunication between scenes
    public static class GetMineGenerationData
    {
        public static int HiddenSeed;
        public static List<ResourcesData> ResourcesData;
    }
}