using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Store;
using Manager.Robots;
using Manager.Train;

namespace Manager.Pay
{
    public class PayRobots : MonoBehaviour
    {
        private void Awake()
        {
            StoreTrainRobotsOperations.OnTransactionDone += PayRobotsBuilding;
            StoreTrainRobotsOperations.OnRobotRemoved += RefundRobot;
        }

        public static void PayRobotsBuilding(Robot robot)
        {
            PriceToBuildOrUpgrade price = GetCost(robot);

            ManageResourcesOperations.RemoveResource("energy", price.energy);
            ManageResourcesOperations.RemoveResource("lithium", price.lithium);
            ManageResourcesOperations.RemoveResource("titanium", price.titanium);
            ManageResourcesOperations.RemoveResource("silicon", price.silicon);
        }
        public static void RefundRobot(Robot robot)
        {
            PriceToBuildOrUpgrade price = GetCost(robot);

            ManageResourcesOperations.AddResource("energy", price.energy);
            ManageResourcesOperations.AddResource("lithium", price.lithium);
            ManageResourcesOperations.AddResource("titanium", price.titanium);
            ManageResourcesOperations.AddResource("silicon", price.silicon);
        }


        private static PriceToBuildOrUpgrade GetCost(Robot robot)
        {
            int level = RobotsManager.robotsData[robot.nameOfTheRobot].robotLevel;
            return robot.robotLevel[level].priceToBuild;
        }
    }
}