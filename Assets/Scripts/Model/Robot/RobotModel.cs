using UnityEngine;

namespace Model.Robot
{
    public class RobotModel
    {
        public float RobotHeight { get; set; }
        public Transform RobotGameObject { get; set; }
        public Vector3 CurrentWorldPosition => RobotGameObject.transform.position;
        public Position CurrentPosition { get; set; }
    }
}