using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scenes;
using Networking.Mines;


namespace Mine
{
    public class EnterMine : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Button _button;
        [SerializeField] private ChangeScene _scene;
        [SerializeField] private List<MineTouched> _mines;
        #endregion


        private void Awake()
        {
            foreach (MineTouched item in _mines)
            {
                item.OnMineSelected += MineTouched;
            }

            _button.onClick.AddListener(Entering);
        }


        private void MineTouched(byte index)
        {
            MineManager.CurrentMine = index;
        }
        private void Entering()
        {
            _scene.GoToScene();
        }


        private void OnDestroy()
        {
            foreach (MineTouched item in _mines)
            {
                item.OnMineSelected -= MineTouched;
            }

            _button.onClick.RemoveAllListeners();
        }
    }
}