using UnityEngine;

namespace Presenter.Cube
{
    /// <summary>
    /// Specifies the type of tile
    /// </summary>
    public enum CubeType
    {
        Tile,
        TurnedOnTile,
        TurnedOffTile
    }
    
    /// <summary>
    /// Used to manage each tile. We can use this class to change the state of the tile
    /// </summary>
    public class CubeTilePresenter : MonoBehaviour
    {
        private Renderer _renderer;
        [SerializeField] private Color turnedOnColor;
        [SerializeField] private Color turnedOffColor;

        public CubeType Type { get; private set; } = CubeType.Tile;
        
        
        /// <summary>
        /// Changing the color of the cube based on the type of tile
        /// </summary>
        /// <param name="type">type of cube tile</param>
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

        /// <summary>
        /// change color of material
        /// </summary>
        /// <param name="color">selected color</param>
        private void ChangeTileMaterial(Color color)
        {
            // we dont need renderer for normal tiles.
            // so we get this component when we call this method.
            if(_renderer == null)
                LoadRendererComponent();
            
            _renderer.material.color = color;
        }

        
        /// <summary>
        /// load object renderer and store in memory
        /// </summary>
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