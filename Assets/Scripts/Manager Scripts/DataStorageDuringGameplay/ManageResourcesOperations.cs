using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.Store
{
    public class ManageResourcesOperations : MonoBehaviour
    {
        public static event Action<string> OnResourcesModified;

        public static event Action OnNotEnoughResources;
        public static event Action OnToMuchResources;

        // Add resources
        private int Add(string resource, int amount)
        {
            int level = WhatLevelToUpdate(resource);

            if (ValidateOperation(level, amount, true))
            {
                level += amount;
            }
            else
            {
                level = StoreDataResources.maximumLevel;
                OnToMuchResources?.Invoke();
            }

            return level;
        }
        public void AddResource(string resource, int amount)
        {
            switch (resource)
            {
                case "energy":
                    StoreDataResources.energyLevel = Add("energy", amount);
                    OnResourcesModified?.Invoke(resource);
                    break;

                case "lithium":
                    StoreDataResources.lithiumLevel = Add("lithium", amount);
                    OnResourcesModified?.Invoke(resource);
                    break;

                case "titanium":
                    StoreDataResources.titaniumLevel = Add("titanium", amount);
                    OnResourcesModified?.Invoke(resource);
                    break;

                case "silicon":
                    StoreDataResources.silliconLevel = Add("silicon", amount);
                    OnResourcesModified?.Invoke(resource);
                    break;
            }
        }


        // Remove resources 
        private int Remove(string resource, int amount)
        {
            int level = WhatLevelToUpdate(resource);

            if (ValidateOperation(level, amount, false))
            {
                level -= amount;
            }
            else
            {
                //StartCoroutine(BarAnimation(bar, level, 0.6f));
                OnNotEnoughResources?.Invoke();
            }

            OnResourcesModified?.Invoke(resource);
            return level;
        }
        public void RemoveResource(string resource, int amount)
        {
            switch (resource)
            {
                case "energy":
                    StoreDataResources.energyLevel = Remove("energy", amount);
                    OnResourcesModified?.Invoke(resource);
                    break;

                case "lithium":
                    StoreDataResources.lithiumLevel = Remove("lithium", amount);
                    OnResourcesModified?.Invoke(resource);
                    break;

                case "titanium":
                    StoreDataResources.titaniumLevel = Remove("titanium", amount);
                    OnResourcesModified?.Invoke(resource);
                    break;

                case "silicon":
                    StoreDataResources.silliconLevel = Remove("silicon", amount);
                    OnResourcesModified?.Invoke(resource);
                    break;
            }
        }


        // Aux methods
        private bool ValidateOperation(int resource, int amount, bool type)
        {
            if (!type)
            {
                if (resource - amount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (resource + amount < StoreDataResources.maximumLevel)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private int WhatLevelToUpdate(string resource)
        {
            switch (resource)
            {
                case "energy":
                    return StoreDataResources.energyLevel;
                case "lithium":
                    return StoreDataResources.lithiumLevel;
                case "titanium":
                    return StoreDataResources.titaniumLevel;
                case "silicon":
                    return StoreDataResources.silliconLevel;
                default:
                    return 0;
            }
        }


        public void Adagsgfd()
        {
            AddResource("energy", 50);
        }
    }
}