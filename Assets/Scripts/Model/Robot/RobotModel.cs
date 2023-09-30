using UnityEngine;

namespace Model.Robot
{
    /// <summary>
    /// A model for storing robot information
    /// </summary>
    public class RobotModel : MonoBehaviour
    {
        [SerializeField] private float robotHeight = .5f;

        public Position Position { get; set; }
        public float RobotHeight => robotHeight;
        public Transform RobotGameObject => transform;
        public Vector3 CurrentWorldPosition => transform.position;
        public RobotDirection Direction { get; set; } = RobotDirection.Forward;
        
        // In order to place the robot on the tile, the height of the robot is added to the Y axis.
        // To get the current coordinates in the Y axis, we subtract the height of the robot from the current Y axis
        public int CurrentRobotYAxis => (int)(CurrentWorldPosition.y - RobotHeight);


        
    }
}