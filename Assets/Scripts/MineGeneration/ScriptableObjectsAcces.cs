using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectsAcces : MonoBehaviour
{
    [SerializeField]
    private BrokenSprites _brokenSprites;

    public static BrokenSprites brokenSprites;

    private void Awake()
    {
        brokenSprites = _brokenSprites;
    }
}
