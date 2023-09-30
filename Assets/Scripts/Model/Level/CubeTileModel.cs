using System;
using Presenter.Cube;
using UnityEngine;

namespace Model.Level
{
    /// <summary>
    /// A model to store the information of each tile
    /// </summary>
    [Serializable]
    public class CubeTileModel
    {
        [SerializeField] private bool isLightTile;
        [SerializeField] private bool isStartPoint;
        [SerializeField] private Position position;
        [SerializeField] private int height = 1;

        public int Height => height;
        public Position Position => position;
        public bool IsLightTile => isLightTile;
        public bool IsStartPoint => isStartPoint;
        public CubeTilePresenter CubeTilePresenter { get; set; }
        public Vector3 WorldPosition => new Vector3(position.x, 0, position.y);

    }
}