﻿using System;
using Presenter.Cube;
using UnityEngine;

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

        public GameObject CubeTileGameObject { get; set; }
    }
}