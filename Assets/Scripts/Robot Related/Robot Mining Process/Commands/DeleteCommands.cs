using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Responsible for deleting the last command given
/// 
/// </summary>

namespace RobotActions
{
    public class DeleteCommands : MonoBehaviour, IDeleteCommand<List<Vector3>>
    {
        private GameObject _commandBlock;

        private void Awake()
        {
            GetComponent<IMining<Vector3>>().OnFinishedMining += SetCommandBlockToRobotPosition;
        }

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
        public void DeleteCommand(List<List<Vector3>> _allHits)
        {
            _allHits.RemoveAt(_allHits.Count - 1);

            if (_allHits.Count > 0)
            {
                SetCommandBlockPosition(_allHits[_allHits.Count - 1]);
            }
        }


        // command block position == last command pisition
        private void SetCommandBlockPosition(List<Vector3> hits)
        {
            List<Vector3> lastCommand = new List<Vector3>(hits);
            Vector3 position = lastCommand[lastCommand.Count - 1];
            _commandBlock.transform.position = new Vector3(position.x, position.y, -6f);
        }


        // command block position == robot position
        private void SetCommandBlockToRobotPosition()
        {
            _commandBlock.transform.position = new Vector3(transform.position.x, transform.position.y, -6f);
        }
        #endregion
    }
}