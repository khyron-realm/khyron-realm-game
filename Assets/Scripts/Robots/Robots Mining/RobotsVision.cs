using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiles.Tiledata;
using DG.Tweening;

namespace Manager.Robots.Mining
{
    public class RobotsVision : MonoBehaviour, IMineOperations
    {
        #region "Input data"
        [SerializeField] private int _radius;
        [SerializeField] private GameObject _circle;
        [SerializeField] private Ease _fadingStyle;
        #endregion

        private GameObject _circleScan;

        private void Awake()
        {
            _circleScan = Instantiate(_circle);
        }


        public void StartMineOperation(Robot robot, GameObject robotGameObject)
        {
            AnimationDetection();
            StartCoroutine(DiscoverTheMine());
        }


        private IEnumerator DiscoverTheMine()
        {
            for (int i = -_radius; i < _radius; i++)
            {
                for (int j = -_radius; j < _radius; j++)
                {
                    Vector3Int temp = new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, 0) + i * Vector3Int.up + j * Vector3Int.left;

                    if (StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int((int)(temp.x), (int)(temp.y), 0)) != null && StoreAllTiles.Instance.Tiles[temp.x][temp.y].Resource != null)
                    {
                        StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int((int)(temp.x), (int)(temp.y), 0), StoreAllTiles.Instance.Tiles[temp.x][temp.y].Resource.ResourceTile);
                    }

                    yield return null;
                }
            }
        }


        private void AnimationDetection()
        {
            _circleScan.transform.position = gameObject.transform.position;
            _circleScan.SetActive(true);
            _circleScan.transform.DOScale(4, 2f).OnComplete(() => _circleScan.SetActive(false));
            _circleScan.GetComponent<SpriteRenderer>().DOFade(0, 2f).SetEase(_fadingStyle);
        }
    }
}