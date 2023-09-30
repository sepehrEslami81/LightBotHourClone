using UnityEngine;

namespace Model.Robot
{
    public enum RobotDirection { Forward = 0, Right, Backward, Left  }
    
    public class RobotModel : MonoBehaviour
    {
        [SerializeField] private float robotHeight = .5f;

        public Position Position { get; set; }
        public float RobotHeight => robotHeight;
        public Position StartPosition { get; set; }
        public Transform RobotGameObject => transform;
        public RobotDirection StartDirection { get; set; }
        public Vector3 CurrentWorldPosition => transform.position;
        public RobotDirection Direction { get; set; } = RobotDirection.Forward;
        
        // In order to place the robot on the tile, the height of the robot is added to the Y axis.
        // To get the current coordinates in the Y axis, we subtract the height of the robot from the current Y axis
        public int CurrentRobotYAxis => (int)(CurrentWorldPosition.y - RobotHeight);


        
    }
}