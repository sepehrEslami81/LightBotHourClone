using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Model.Level
{
    [Serializable]
    public class CubeTileModel
    {
        [SerializeField] private bool isLightTile;
        [SerializeField] private bool isStartPoint;
        [SerializeField] private Vector3 worldPosition;
        [SerializeField] private Position position;
        [SerializeField] private int height = 1;

        public int Height => height;
        public Vector3 WorldPosition => worldPosition;
        public bool IsLightTile => isLightTile;
        public bool IsStartPoint => isStartPoint;
        public Position Position => position;
        
        public Vector3 CalculatedPosition => new Vector3(worldPosition.x, height / 2f, worldPosition.z);
    }
}