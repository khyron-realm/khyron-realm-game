using System;
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
        [SerializeField] private GameObject _healthStatus;
        [SerializeField] private Ease _fadingStyle;
        #endregion

        private GameObject _circleScan;

        private Vector2Int _centerPosition;
        private int _radiusToScan;


        private void Awake()
        {
            _circleScan = Instantiate(_circle);
        }


        public void StartMineOperation(Robot robot, GameObject robotGameObject)
        {
            _centerPosition = new Vector2Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y);
            _radiusToScan = 8;

            AnimationDetection();
            StartCoroutine(StartRevealing());
            StartCoroutine(Timer());
        }


        private void AnimationDetection()
        {
            _circleScan.transform.position = gameObject.transform.position;
            _circleScan.SetActive(true);
            _circleScan.transform.DOScale(4, 2f).OnComplete(() => _circleScan.SetActive(false));
            _circleScan.GetComponent<SpriteRenderer>().DOFade(0, 2f).SetEase(_fadingStyle);
        }


        /// <summary>
        /// Reveals the area of mining
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartRevealing()
        {
            for (int i  = _centerPosition.x - _radiusToScan; i <= _centerPosition.x; i++)
            {
                for (int j = _centerPosition.y - _radiusToScan; j <= _centerPosition.y; j++)
                {
                    // we don't have to take the square root, it's slow
                    if ((i - _centerPosition.x) * (i - _centerPosition.x) + (j - _centerPosition.y) * (j - _centerPosition.y) <= _radiusToScan * _radiusToScan)
                    {
                        int xSym = _centerPosition.x - (i - _centerPosition.x);
                        int ySym = _centerPosition.y - (j - _centerPosition.y);

                        RevealAllSimetricalBlocks(i, j, xSym, ySym);
                    }

                    yield return null;
                }

                yield return null;
            }
        }


        /// <summary>
        /// Hide all blocks revealed
        /// </summary>
        /// <returns></returns>
        private IEnumerator StopRevealingZone()
        {
            for (int i = _centerPosition.x - _radiusToScan; i <= _centerPosition.x; i++)
            {
                for (int j = _centerPosition.y - _radiusToScan; j <= _centerPosition.y; j++)
                {
                    // we don't have to take the square root, it's slow
                    if ((i - _centerPosition.x) * (i - _centerPosition.x) + (j - _centerPosition.y) * (j - _centerPosition.y) <= _radiusToScan * _radiusToScan)
                    {
                        int xSym = _centerPosition.x - (i - _centerPosition.x);
                        int ySym = _centerPosition.y - (j - _centerPosition.y);

                        HideAllSimetricalBlocks(i, j, xSym, ySym);
                    }

                    yield return null;
                }

                yield return null;
            }
        }


        /// <summary>
        /// Reveals all blocks simetrical to initial one
        /// </summary>
        /// <param name="i"> x position </param>
        /// <param name="j"> y position </param>
        /// <param name="xSym"> x simetric position </param>
        /// <param name="ySym"> y simetric position </param>
        private static void RevealAllSimetricalBlocks(int i, int j, int xSym, int ySym)
        {
            RevealBlock(i, j);
            RevealBlock(i, ySym);
            RevealBlock(xSym, j);
            RevealBlock(xSym, ySym);
        }


        /// <summary>
        /// Reveals the block
        /// </summary>
        /// <param name="i"> x position </param>
        /// <param name="j"> y position </param>
        private static void RevealBlock(int i, int j)
        {
            if (StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int((int)(i), (int)(j), 0)) != null && StoreAllTiles.Instance.Tiles[i][j].Resource != null)
            {
                StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int((int)(i), (int)(j), 0), StoreAllTiles.Instance.Tiles[i][j].Resource.ResourceTile);
                StoreAllTiles.Instance.Tiles[i][j].Discovered += 1;
            }
        }


        /// <summary>
        /// Hides all blocks from one iteration
        /// </summary>
        /// <param name="i"> x pos </param>
        /// <param name="j"> y pos </param>
        /// <param name="xSym"> x simetric pos </param>
        /// <param name="ySym"> y simetric pos</param>
        private static void HideAllSimetricalBlocks(int i, int j, int xSym, int ySym)
        {
            HideBlock(i, j);
            HideBlock(i, ySym);
            HideBlock(xSym, j);
            HideBlock(xSym, ySym);
        }


        /// <summary>
        /// Hides the block
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private static void HideBlock(int i, int j)
        {
            if (StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int((int)(i), (int)(j), 0)) != null)
            {
                StoreAllTiles.Instance.Tiles[i][j].Discovered -= 1;

                if (StoreAllTiles.Instance.Tiles[i][j].Discovered < 1)
                {
                    StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int((int)(i), (int)(j), 0), StoreAllTiles.Instance.Tiles[i][j].StandardBlock);
                }
            }
        }


        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(20f);
            StartCoroutine(StopRevealingZone());
            gameObject.GetComponent<SpriteRenderer>().DOFade(0, 2f).OnComplete(() => gameObject.SetActive(false));
            _healthStatus.GetComponent<SpriteRenderer>().DOFade(0, 2f);
        }
    }
}