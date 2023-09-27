using System;
using System.Collections;
using UnityEngine;

namespace Robot
{
    public class RobotController : MonoBehaviour
    {
        [Range(0f, 2f)] [SerializeField] private float robotBodyHeight;
        [Range(0f, 5f)] [SerializeField] private float animationDurationTime;
        
        public void SetPosition(Vector3 dest, int height)
        {
            transform.position = new Vector3(dest.x, height + robotBodyHeight, dest.z);
        }

        public void MoveToNextTile()
        {
            var start = transform.position;
            var end = new Vector3(start.x, start.y, --start.z);
            StartCoroutine(LerpPosition(start, end, animationDurationTime));
        }
        
        
        private IEnumerator LerpPosition(Vector3 start, Vector3 end, float duration)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                transform.position = Vector3.Lerp(start, end, t);

                elapsedTime += Time.deltaTime;
                yield return null; 
            }

            transform.position = end;
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MoveToNextTile();
            }
        }
    }
}