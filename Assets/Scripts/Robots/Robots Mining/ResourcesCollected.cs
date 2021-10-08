using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AuxiliaryClasses;
using DG.Tweening;

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
            resource.transform.position = gameObject.transform.position;

            resource.SetActive(true);
            resource.transform.DOMoveY(position.y + 2f, 1.2f).OnComplete(()=> resource.SetActive(false));
            resource.GetComponent<SpriteRenderer>().DOFade(0, 1f);
        }
    }
}