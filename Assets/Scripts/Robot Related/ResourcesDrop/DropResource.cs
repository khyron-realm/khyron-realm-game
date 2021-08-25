using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropResource : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    private int _scoreInt = 0;

    private void Awake()
    {
        _scoreText.text = "0";
        StoreDataAboutTiles.OnMinedBlock += Drop;
    }

    private void Drop(MineResources temp)
    {
        int randomValue = Random.Range(temp.dropValueMin, temp.dropValueMax);
        _scoreInt += randomValue;

        try
        {
            _scoreText.text = _scoreInt.ToString();
        }
        catch
        {

        }
    }
}
