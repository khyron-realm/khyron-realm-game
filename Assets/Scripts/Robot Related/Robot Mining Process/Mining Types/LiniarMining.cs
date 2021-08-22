using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mine in straight line every command

[RequireComponent(typeof(Movement))]
public class LiniarMining : MonoBehaviour, IMining<Vector3>
{
    private int _damage;
    private WaitForSeconds _time;
    private IMove _move;

    public event Action OnMining;
    public event Action OnFinishedMining;

    private void Awake()
    {
        _time = new WaitForSeconds(0.2f);
        _move = GetComponent<IMove>();
    }

    #region "Methods"
    public void Mine(List<List<Vector3>> _allHits, int damage)
    {
        _damage = damage;
        StartCoroutine("Mining", _allHits);
    }

    private IEnumerator Mining(List<List<Vector3>> _allHits)
    {
        for (int i = 0; i < _allHits.Count; i++)
        {
            for (int j = 0; j < _allHits[i].Count; j++)
            {
                Vector3 temp = _allHits[i][j];

                bool breaked = false;

                if (StoreAllTiles.instance.tiles[(int)(temp.x - 0.5f)][(int)(temp.y - 0.5f)].Health <= 0)
                {
                    breaked = true;
                }

                if (breaked == false)
                {
                    bool check = false;

                    OnMining?.Invoke();

                    while (!check)
                    {
                        StoreAllTiles.instance.tiles[(int)(temp.x - 0.5f)][(int)(temp.y - 0.5f)].Health -= (int)(_damage / 5f);
                        
                        if(StoreAllTiles.instance.tiles[(int)(temp.x - 0.5f)][(int)(temp.y - 0.5f)].Health < 0)
                        {
                            check = true;
                        }

                        yield return _time;
                    }
                }

                // !-- BUG -- !
                Vector3 destination = new Vector3(temp.x, temp.y, 0);

                StoreAllTiles.instance.Tilemap.SetTile(new Vector3Int((int)(temp.x - 0.5f), (int)(temp.y - 0.5f), 0), null);

                yield return _move.MoveTo(gameObject, transform.position, destination, breaked);

                if (_allHits.Count > 0)
                {
                    // if command is deleted _allHits is null
                    // but coroutine continues until robot position is on a tile
                    // during movement, values are added to _allHits and then the first one is removed
                    _allHits[i].RemoveAt(j);
                    j--;    
                }
                else
                {
                    OnFinishedMining?.Invoke();
                    _allHits.Clear();

                    yield break;
                }

                // !-- BUG -- !
            }
            _allHits.RemoveAt(i);
            i--;
        }
        _allHits.Clear();

        OnFinishedMining?.Invoke();
    }
    #endregion
}