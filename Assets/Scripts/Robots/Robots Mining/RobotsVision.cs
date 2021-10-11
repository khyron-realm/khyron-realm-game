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
            StartCoroutine(Circle(new Vector2Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y), 4));
        }


        private void AnimationDetection()
        {
            _circleScan.transform.position = gameObject.transform.position;
            _circleScan.SetActive(true);
            _circleScan.transform.DOScale(4, 2f).OnComplete(() => _circleScan.SetActive(false));
            _circleScan.GetComponent<SpriteRenderer>().DOFade(0, 2f).SetEase(_fadingStyle);
        }


        private IEnumerator Circle(Vector2Int centerPosition, int radius)
        {
            for (int i  = centerPosition.x - radius; i <= centerPosition.x; i++)
            {
                for (int j = centerPosition.y - radius; j <= centerPosition.y; j++)
                {
                    // we don't have to take the square root, it's slow
                    if ((i - centerPosition.x) * (i - centerPosition.x) + (j - centerPosition.y) * (j - centerPosition.y) <= radius * radius)
                    {
                        int xSym = centerPosition.x - (i - centerPosition.x);
                        int ySym = centerPosition.y - (j - centerPosition.y);

                        //StoreAllTiles.Instance.Tilemap.SetColor(new Vector3Int((int)(xSym), (int)(ySym), 0), Color.green);
                        //StoreAllTiles.Instance.Tilemap.SetColor(new Vector3Int((int)(i), (int)(ySym), 0), Color.green);
                        //StoreAllTiles.Instance.Tilemap.SetColor(new Vector3Int((int)(i), (int)(j), 0), Color.green);
                        //StoreAllTiles.Instance.Tilemap.SetColor(new Vector3Int((int)(xSym), (int)(j), 0), Color.green);

                        int ttt = Random.Range(0, 2);

                        if(ttt == 0)
                        {
                            if (StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int((int)(i), (int)(j), 0)) != null && StoreAllTiles.Instance.Tiles[i][j].Resource != null)
                            {
                                StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int((int)(i), (int)(j), 0), StoreAllTiles.Instance.Tiles[i][j].Resource.ResourceTile);
                            }

                            if (StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int((int)(i), (int)(ySym), 0)) != null && StoreAllTiles.Instance.Tiles[i][ySym].Resource != null)
                            {
                                StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int((int)(i), (int)(ySym), 0), StoreAllTiles.Instance.Tiles[i][ySym].Resource.ResourceTile);
                            }

                            if (StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int((int)(xSym), (int)(j), 0)) != null && StoreAllTiles.Instance.Tiles[xSym][j].Resource != null)
                            {
                                StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int((int)(xSym), (int)(j), 0), StoreAllTiles.Instance.Tiles[xSym][j].Resource.ResourceTile);
                            }

                            if (StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int((int)(xSym), (int)(ySym), 0)) != null && StoreAllTiles.Instance.Tiles[xSym][ySym].Resource != null)
                            {
                                StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int((int)(xSym), (int)(ySym), 0), StoreAllTiles.Instance.Tiles[xSym][ySym].Resource.ResourceTile);
                            }
                        }                     
                    }

                    yield return null;
                }

                yield return null;
            }
        }
    }
}