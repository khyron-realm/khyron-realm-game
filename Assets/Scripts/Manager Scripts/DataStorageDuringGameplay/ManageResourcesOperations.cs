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
        public static void Add(GameResources resource, int amount)
        {
            if (ValidateOperation(resource, amount, true))
            {
                resource.currentValue += amount;
            }
            else
            {
                resource.currentValue = resource.maxValue;
                OnToMuchResources?.Invoke();
            }

            OnResourcesModified?.Invoke(resource.nameOfResource);
        }
        public static void Remove(GameResources resource, int amount)
        {
            if (ValidateOperation(resource, amount, false))
            {
                resource.currentValue -= amount;
            }
            else
            {
                OnNotEnoughResources?.Invoke();
            }

            OnResourcesModified?.Invoke(resource.nameOfResource);
        }


        public static bool PayAllResources(int energyAmount, int lithiumAmount, int titaniumAmount, int siliconAmount)
        {
            bool energy = ValidateOperation(StoreDataResources.energy, energyAmount, false);
            bool lithium = ValidateOperation(StoreDataResources.lithium, lithiumAmount, false);
            bool titanium = ValidateOperation(StoreDataResources.titanium, titaniumAmount, false);
            bool silicon = ValidateOperation(StoreDataResources.silicon, siliconAmount, false);

            if(energy && lithium && titanium && silicon)
            {
                Remove(StoreDataResources.energy, energyAmount);
                Remove(StoreDataResources.lithium, lithiumAmount);
                Remove(StoreDataResources.titanium, titaniumAmount);
                Remove(StoreDataResources.silicon, siliconAmount);
                return true;
            }
            else
            {
                OnNotEnoughResources?.Invoke();
                return false;
            }
        }
        public static void RefundPayment(int energyAmount, int lithiumAmount, int titaniumAmount, int siliconAmount)
        {
            Add(StoreDataResources.energy, energyAmount);
            Add(StoreDataResources.lithium, lithiumAmount);
            Add(StoreDataResources.titanium, titaniumAmount);
            Add(StoreDataResources.silicon, siliconAmount);
        }


        private static bool ValidateOperation(GameResources resource, int amount, bool type)
        {
            if (!type)
            {
                if (resource.currentValue - amount > 0)
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