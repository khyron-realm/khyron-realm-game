using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Mine
{
    public class EnterMine : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private List<MineTouched> _mines;

        private void Awake()
        {
            foreach (MineTouched item in _mines)
            {

            }
        }

        private void MineTouched()
        {

        }

    }
}