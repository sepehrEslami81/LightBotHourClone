﻿using System;
using UnityEngine;

namespace Cube
{
    public class CubeTile : MonoBehaviour
    {
        private Renderer _renderer;
        [SerializeField] private Color turnedOnColor;
        [SerializeField] private Color turnedOffColor;

        public CubeType Type { get; private set; } = CubeType.Tile;

        private void Awake()
        {
            if(gameObject.CompareTag("LightCube"))
                ChangeTileMaterial(turnedOffColor);
        }

        public void ChangeTileStatus(CubeType type)
        {
            switch (type)
            {
                case CubeType.TurnedOffTile:
                    ChangeTileMaterial(turnedOffColor);
                    Type = CubeType.TurnedOffTile;
                    break;
                
                case CubeType.TurnedOnTile:
                    ChangeTileMaterial(turnedOnColor);
                    Type = CubeType.TurnedOnTile;
                    break;
            }
        }

        private void ChangeTileMaterial(Color color)
        {
            // we dont need renderer for normal tiles.
            // so we get this component when need it.
            if(_renderer == null)
                LoadRendererComponent();
            
            _renderer.material.color = color;
        }

        private void LoadRendererComponent()
        {
            if (TryGetComponent(out Renderer component))
            {
                _renderer = component;
            }
            else
            {
                Debug.LogError("failed to load renderer component!");
            }
        }
    }
}