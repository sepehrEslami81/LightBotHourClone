﻿using UnityEngine;

namespace Model.Robot
{
    public enum RobotDirection { Forward = 0, Backward, Left, Right }
    
    public class RobotModel : MonoBehaviour
    {
        [SerializeField] private float robotHeight = .5f;

        public float RobotHeight => robotHeight;
        public Transform RobotGameObject => transform;
        public Vector3 CurrentWorldPosition => transform.position;
        public Position Position { get; set; }
        public RobotDirection Direction { get; set; } = RobotDirection.Forward;
    }
}