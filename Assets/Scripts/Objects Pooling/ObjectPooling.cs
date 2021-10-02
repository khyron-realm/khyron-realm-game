using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPooling : MonoBehaviour
    {
        #region "Input data" 
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private int amountToPool;

        [SerializeField] private GameObject _canvasToPool;
        #endregion

        private List<GameObject> pooledObjects;

        private void Start()
        {
            pooledObjects = new List<GameObject>();
            GameObject tmp;
            for (int i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(objectToPool);
                tmp.transform.SetParent(_canvasToPool.transform, false);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }


        public GameObject GetPooledObjects()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }
    }
}