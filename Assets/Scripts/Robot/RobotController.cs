using UnityEngine;

namespace Robot
{
    public class RobotController : MonoBehaviour
    {
        [Range(0f, 2f)] [SerializeField] private float robotBodyHeight;
        
        public void ForceMove(Vector3 dest, int height)
        {
            transform.position = new Vector3(dest.x, height + robotBodyHeight, dest.z);
        }
    }
}