using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Train;


namespace AnimationsFloors
{
    public class AnimationRobotsBuilding : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Animator _animator1;
        [SerializeField] private Animator _animator2;
        #endregion

        private void Awake()
        {
            _animator1.enabled = false;
            _animator2.enabled = false;

            BuildRobotsOperations.OnStartOperation += StartAnimation;

            BuildRobotsOperations.OnBuildingProcessFinished += StopAnimation;
            BuildRobots.OnBuildingProcessFinished += StopAnimation;
        }


        private void StartAnimation()
        {
            _animator1.enabled = true;
            _animator2.enabled = true;

            _animator1.Play("RobotArmAnimation-BuildingRobots", 0, 0.2f);
            _animator2.Play("RobotArmAnimation-BuildingRobots", 0, 0.6f);
        }
        private void StopAnimation()
        {
            _animator1.enabled = false;
            _animator2.enabled = false;
        }
    }
}