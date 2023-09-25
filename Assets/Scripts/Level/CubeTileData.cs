using System;
using UnityEngine;

namespace Level
{
    [Serializable]
    public class CubeTileData
    {
        [SerializeField] private bool isLightTile;
        [SerializeField] private bool isStartPoint;
        [SerializeField] private Vector3 position;
        [SerializeField] private int height = 1;

        public int Height => height;
        public Vector3 Position => position;
        public bool IsLightTile => isLightTile;
        public bool IsStartPoint => isStartPoint;
    }
}