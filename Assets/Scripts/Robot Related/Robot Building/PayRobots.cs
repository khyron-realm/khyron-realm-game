using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Robots;
using Manager.Store;

namespace Manager.PayOperation
{
    public class PayRobots : MonoBehaviour
    {
        public static bool StartPaymenetProcedure(Robot robot)
        {
            PriceToBuildOrUpgrade price = GetCost(robot);
            return ManageResourcesOperations.Remove(StoreDataResources.energy, price.energy);
        }
        public static void RefundRobot(Robot robot)
        {
            PriceToBuildOrUpgrade price = GetCost(robot);
            ManageResourcesOperations.Add(StoreDataResources.energy, price.energy);
        }


        private static PriceToBuildOrUpgrade GetCost(Robot robot)
        {
            int level = RobotsManager.robotsData[robot.nameOfTheRobot].robotLevel;
            return robot.robotLevel[level].priceToBuild;
        }
    }
}