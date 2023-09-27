using System;
using Cube;
using Robot;
using UnityEngine;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelData level;
        [SerializeField] private RobotController robot;
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

                if (levelCubeTile.IsStartPoint)
                {
                    robot.SetPosition(levelCubeTile.Position, levelCubeTile.Height);
                }
            }
        }

        private void LoadCubeTile(CubeTileData levelCubeTile)
        {
            var instantiatedObject = Instantiate(tileCubePrefab, levelCubeTile.CalculatedPosition, Quaternion.identity);
            
            instantiatedObject.transform.localScale = CalcTileScale(levelCubeTile); 

            if (levelCubeTile.IsLightTile)
            {
                ConfigureLightCubeTile(instantiatedObject);
            }

            
            // create scale v3 according to height value
            Vector3 CalcTileScale(CubeTileData cubeTileData) => new Vector3(1, cubeTileData.Height, 1);
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