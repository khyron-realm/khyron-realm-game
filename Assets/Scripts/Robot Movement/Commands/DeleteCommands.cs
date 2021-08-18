using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Responsible for deleting the last command given
/// 
/// </summary>
public class DeleteCommands : MonoBehaviour, IDeleteCommand<List<Collider2D>>
{
    private GameObject _commandBlock;

    #region "Setters and Getters"
    public GameObject CommandBlock
    {
        set
        {
            _commandBlock = value;
        }
    }
    #endregion

    #region "Methods"
    public void DeleteCommand(List<List<Collider2D>> _allHits)
    {
        if (_allHits.Count == 1)
        {
            _commandBlock.transform.position = new Vector3(_allHits[0][0].transform.position.x, _allHits[0][0].transform.position.y, -6f);
        }

        _allHits.RemoveAt(_allHits.Count - 1);

        if (_allHits.Count > 0)
        {
            SetCommandBlockPosition(_allHits[_allHits.Count - 1]);
        }
    }

    private void SetCommandBlockPosition(List<Collider2D> hits)
    {
        List<Collider2D> lastCommand = new List<Collider2D>(hits);
        Vector3 position = lastCommand[lastCommand.Count - 1].transform.position;
        _commandBlock.transform.position = new Vector3(position.x, position.y, -6f);
    }
    #endregion
}