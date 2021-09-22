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
        public static bool Add(GameResources resource, int amount)
        {
            
            if (ValidateOperation(resource, amount, true))
            {
                resource.currentValue += amount;
                OnResourcesModified?.Invoke(resource.nameOfResource);
                return true;
            }
            else
            {
                resource.currentValue = resource.maxValue;
                OnResourcesModified?.Invoke(resource.nameOfResource);
                OnToMuchResources?.Invoke();
                return false;
            }       
        }
        public static bool Remove(GameResources resource, int amount)
        {
            
            if (ValidateOperation(resource, amount, false))
            {
                resource.currentValue -= amount;
                OnResourcesModified?.Invoke(resource.nameOfResource);
                return true;
            }
            else
            {
                OnNotEnoughResources?.Invoke();
                OnResourcesModified?.Invoke(resource.nameOfResource);
                return false;
            }     
        }


        public static void PayResources(int lithiumAmount, int titaniumAmount, int siliconAmount)
        {
            bool lithium = ValidateOperation(StoreDataResources.lithium, lithiumAmount, false);
            bool titanium = ValidateOperation(StoreDataResources.titanium, titaniumAmount, false);
            bool silicon = ValidateOperation(StoreDataResources.silicon, siliconAmount, false);

            if (lithium && titanium && silicon)
            {
                Remove(StoreDataResources.lithium, lithiumAmount);
                Remove(StoreDataResources.titanium, titaniumAmount);
                Remove(StoreDataResources.silicon, siliconAmount);
            }
            else
            {
                OnNotEnoughResources?.Invoke();
            }
        }
        public static void RefundPayment(int lithiumAmount, int titaniumAmount, int siliconAmount)
        {
            Add(StoreDataResources.lithium, lithiumAmount);
            Add(StoreDataResources.titanium, titaniumAmount);
            Add(StoreDataResources.silicon, siliconAmount);
        }


        // PayResources
        private static bool ValidateOperation(GameResources resource, int amount, bool type)
        {
            if (!type)
            {
                if (resource.currentValue - amount >= 0)
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
                if (resource.currentValue + amount <= resource.maxValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}