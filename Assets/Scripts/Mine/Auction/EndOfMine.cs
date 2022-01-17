using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine.Ending
{
    public class EndOfMine : MonoBehaviour
    {
        [SerializeField] private GameObject _canvasToRender;
        [SerializeField] private GameObject _backGround;
        [SerializeField] private GameObject _infoPanel;


        private List<RobotSO> _robots;

        private int _lithiumAmount;
        private int _siliconAmount;
        private int _titaniumAmount;

        private void EndOfMiningProcedure()
        {

        }

        private void AnimationEndOfMine()
        { 


        }
    }
}