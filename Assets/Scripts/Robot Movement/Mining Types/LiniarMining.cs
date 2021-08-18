using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mine in straight line every command
public class LiniarMining : MonoBehaviour, IMining
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
    public void Mine(List<List<Collider2D>> _allHits, int damage)
    {
        _damage = damage;
        StartCoroutine("Mining", _allHits);
    }

    private IEnumerator Mining(List<List<Collider2D>> _allHits)
    {
        for (int i = 0; i < _allHits.Count; i++)
        {
            for (int j = 0; j < _allHits[i].Count; j++)
            {
                Collider2D temp = _allHits[i][j];

                bool breaked = false;

                if (temp.GetComponent<HealthManager>().Health <= 0)
                {
                    breaked = true;
                }

                if (breaked == false)
                {
                    bool check = false;

                    OnMining?.Invoke();

                    while (!check)
                    {
                        check = temp.GetComponent<HealthManager>().DoDamage((int)(_damage / 5f));
                        yield return _time;
                    }
                }

                // !-- BUG -- !
                Vector3 destination = new Vector3(temp.transform.position.x, temp.transform.position.y, transform.position.z);

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