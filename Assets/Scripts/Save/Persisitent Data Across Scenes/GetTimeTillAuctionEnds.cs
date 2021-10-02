using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    // Static class that PERSIST during the aplication runtime 
    // The static values remain the same even if you change the scenes
    // Class must not inherit from MonoBehaviour
    // Used for comunication between scenes
    public static class GetTimeTillAuctionEnds
    {
        public static int TimeOfTheMine;
    }
}