using System;
using Cube;
using UnityEngine;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelData level;
        [SerializeField] private GameObject tileCubePrefab;

        private void Start()
        {
            LoadLevel();
        }


        public void LoadLevel()
        {
            print("loading level " + level.LevelName);

            foreach (var levelCubeTile in level.CubeTiles)
            {
                LoadCubeTile(levelCubeTile);
            }
        }

        private void LoadCubeTile(CubeTileData levelCubeTile)
        {
            var instantiatedObject = Instantiate(tileCubePrefab, levelCubeTile.Position, Quaternion.identity);
            if (levelCubeTile.IsLightTile)
            {
                ConfigureLightCubeTile(instantiatedObject);
            }
        }

        private void ConfigureLightCubeTile(GameObject instantiatedObject)
        {
            instantiatedObject.tag = "LightCube";
            
            if (instantiatedObject.TryGetComponent(out CubeTile cube))
            {
                cube.ChangeTileStatus(CubeType.TurnedOffTile);
            }
            else
            {
                Debug.LogError("failed to get CubeTile component from cube!");
            }
        }
    }
}