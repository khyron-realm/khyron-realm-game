using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AuxiliaryClasses;
using DG.Tweening;
using PlayerDataUpdate;

namespace Manager.Robots.Mining
{
    public class ResourcesCollected : MonoBehaviour
    {
        #region "Input data" 
        [SerializeField] private RobotMining _miningScript;

        [SerializeField] private Sprite _lithiumOre;
        [SerializeField] private Sprite _siliconOre;
        [SerializeField] private Sprite _titanOre;
        #endregion

        #region "Private members"
        private static ObjectPooling _objectPooled;

        private Sprite _resource;
        #endregion

        private void Start()
        {
            _objectPooled = GameObject.Find("PooledResources").GetComponent<ObjectPooling>();
            _miningScript.OnResourceMined += AnimateResource;
            _miningScript.OnResourceMined += AddResource;
        }

        private void AddResource(MineResources temp, Vector3 position)
        {
            switch (temp.name)
            {
                case "SiliconOre":
                    PlayerDataOperations.PayResources(40, 0, 0, 255);
                    break;
                case "LithiumOre":
                    PlayerDataOperations.PayResources(0, 22, 0, 255);
                    break;                
                case "TitaniumOre":
                    PlayerDataOperations.PayResources(0, 0, 12, 255);
                    break;
            }
        }
        private void AnimateResource(MineResources temp, Vector3 position)
        {
            switch(temp.name)
            {
                case "LithiumOre":
                    _resource = _lithiumOre;
                    break;
                case "SiliconOre":
                    _resource = _siliconOre;
                    break;
                case "TitaniumOre":
                    _resource = _titanOre;
                    break;
            }

            GameObject resource = _objectPooled.GetPooledObjects();

            resource.GetComponent<SpriteRenderer>().sprite = _resource;
            resource.GetComponent<SpriteRenderer>().color = Color.white;
            resource.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -8);

            resource.SetActive(true);
            resource.transform.DOMoveY(position.y + 3f, 1.4f).OnComplete(()=> resource.SetActive(false));
            resource.GetComponent<SpriteRenderer>().DOFade(0, 1.4f);
        }

        private void OnDestroy()
        {
            _miningScript.OnResourceMined -= AnimateResource;
            _miningScript.OnResourceMined -= AddResource;
        }
    }
}