using System;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [Serializable]
    public struct ArmatureParts
    {
        [SerializeField]
        private GameObject _head;
        [SerializeField]
        private GameObject _torso;
        [SerializeField]
        private GameObject _leftShoulder;
        [SerializeField]
        private GameObject _leftForearm;
        [SerializeField]
        private GameObject _leftArm;
        [SerializeField]
        private GameObject _leftHand;
        [SerializeField]
        private GameObject _rightShoulder;
        [SerializeField]
        private GameObject _rightForearm;
        [SerializeField]
        private GameObject _rightArm;
        [SerializeField]
        private GameObject _rightHand;
    }
}