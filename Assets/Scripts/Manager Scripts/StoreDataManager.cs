using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoreDataManager : MonoBehaviour
{
    // Progress bar available in the game
    [SerializeField] private ProgressBar xpBar;
    [SerializeField] private ProgressBar energyBar;
    [SerializeField] private ProgressBar lithiumBar;
    [SerializeField] private ProgressBar titaniumBar;
    [SerializeField] private ProgressBar silliconBar;
    
    [Space(20f)]

    // The maximum level of the resources
    [SerializeField] private int _maximumLevel;


    // Resources level
    public static int energyLevel;
    public static int lithiumLevel;
    public static int titaniumLevel;
    public static int silliconLevel;


    // Thresholds for leveling up
    [SerializeField] private LevelsThresholds _levelsThresholds;


    // Level and experience
    public static int currentLevel;
    public static int currentXp;


    // Events
    public static event Action OnNotEnoughResources;
    public static event Action OnToMuchResources;
    public static event Action<int> OnLevelUp;


    private void Awake()
    {
        xpBar.MaxValue = _levelsThresholds.levelsThresholds[0];
        InitValuesOfStoredData();
        InitMaximumLevels();
    }


    // Init values at the start of the game 
    // Shall be server related
    private static void InitValuesOfStoredData()
    {
        energyLevel = 0;
        lithiumLevel = 0;
        titaniumLevel = 0;
        silliconLevel = 0;
        currentLevel = 1;
        currentXp = 0;
    }
    private void InitMaximumLevels()
    {
        energyBar.MaxValue = _maximumLevel;
        lithiumBar.MaxValue = _maximumLevel;
        titaniumBar.MaxValue = _maximumLevel;
        silliconBar.MaxValue = _maximumLevel;
    }


    // Add resources to the level
    private int Add(string resource, int amount)
    {
        ProgressBar bar = WhatBarToUpdate(resource);
        int level = WhatLevelToUpdate(resource);

        if (ValidateOperation(level, amount, true))
        {
            level += amount;
            StartCoroutine(BarAnimation(bar, level, 0.6f));
        }
        else
        {
            level = _maximumLevel;
            StartCoroutine(BarAnimation(bar, level, 0.6f));
            OnToMuchResources?.Invoke();     
        }
       
        return level;
    }
    public void AddResource(string resource, int amount)
    {
        switch (resource)
        {
            case "energy":
                energyLevel = Add("energy", amount);
                break;

            case "lithium":
                lithiumLevel = Add("lithium", amount);
                break;

            case "titanium":
                titaniumLevel = Add("titanium", amount);
                break;

            case "silicon":
                silliconLevel = Add("silicon", amount);
                break;
        }
    }


    // Remove resources from the level
    private int Remove(string resource, int amount)
    {
        ProgressBar bar = WhatBarToUpdate(resource);
        int level = WhatLevelToUpdate(resource);

        if (ValidateOperation(level, amount, false))
        {
            level -= amount;
            StartCoroutine(BarAnimation(bar, level, 0.6f));
        }
        else
        {
            StartCoroutine(BarAnimation(bar, level, 0.6f));
            OnNotEnoughResources?.Invoke();
        }
        return level;
    }
    public void RemoveResource(string resource, int amount)
    {
        switch (resource)
        {
            case "energy":
                energyLevel = Remove("energy", amount);
                break;

            case "lithium":
                lithiumLevel = Remove("lithium", amount);
                break;

            case "titanium":
                titaniumLevel = Remove("titanium", amount);
                break;

            case "silicon":
                silliconLevel = Remove("silicon", amount);
                break;
        }
    }


    // Auxiliary methods for the add/remove operations
    private ProgressBar WhatBarToUpdate(string resource)
    {
        switch (resource)
        {
            case "energy":
                return energyBar;
            case "lithium":
                return lithiumBar;
            case "titanium":
                return titaniumBar;
            case "silicon":
                return silliconBar;
            default:
                return null;
        }
    }
    private int WhatLevelToUpdate(string resource)
    {
        switch (resource)
        {
            case "energy":
                return energyLevel;
            case "lithium":
                return lithiumLevel;
            case "titanium":
                return titaniumLevel;
            case "silicon":
                return silliconLevel;
            default:
                return 0;
        }
    }
    private bool ValidateOperation(int resource, int amount, bool type)
    {
        if(!type)
        {
            if(resource - amount > 0)
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
            if (resource + amount < _maximumLevel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    // XP and User Level related
    public void AddXP(int xp)
    {
        currentXp += xp;
        
        CheckIfLevelUp();
        StartCoroutine(BarAnimation(xpBar, currentXp, 0.6f));
    }
    private void CheckIfLevelUp()
    {
        if (currentLevel < _levelsThresholds.levelsThresholds.Count && currentXp > _levelsThresholds.levelsThresholds[currentLevel - 1])
        {
            currentXp -= _levelsThresholds.levelsThresholds[currentLevel - 1];
            xpBar.MaxValue = _levelsThresholds.levelsThresholds[currentLevel];
            StartCoroutine(BarAnimation(xpBar, currentXp, 2));

            currentLevel++;

            OnLevelUp?.Invoke(currentLevel);
        }
    }


    // Animation for the bars
    private IEnumerator BarAnimation(ProgressBar bar, int endGoal, float time)
    {
        float temp = 0f;
        while (temp < time)
        {
            temp += Time.deltaTime;        
            bar.CurrentValue = (int)Mathf.Lerp(bar.CurrentValue, endGoal, temp) + 1;
            yield return null;
        }
    }

}