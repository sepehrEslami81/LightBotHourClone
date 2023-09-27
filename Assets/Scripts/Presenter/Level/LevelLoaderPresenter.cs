using Cube;
using Model.Level;
using Presenter.Robot;
using Robot;
using UnityEngine;

namespace Presenter.Level
{
    public class LevelLoaderPresenter : MonoBehaviour
    {
        [SerializeField] private LevelModel level;
        [SerializeField] private RobotPresenter robot;
        [SerializeField] private GameObject tileCubePrefab;

        private void Start()
        {
            LoadLevel();
        }


        public void LoadLevel()
        {
            print("loading level " + level.Id);
            
            robot.SetCurrentLevel(level);

            foreach (var cubeTile in level.CubeTileModels)
            {
                LoadCubeTile(cubeTile);

                if (cubeTile.IsStartPoint)
                {
                    var startPos = new Vector3(cubeTile.Position.x, cubeTile.Height, cubeTile.Position.z);
                    robot.SetStartPosition(startPos);
                    robot.ResetRobotPosition();
                    
                }
            }
        }

        private void LoadCubeTile(CubeTileModel levelCubeTile)
        {
            var instantiatedObject = Instantiate(tileCubePrefab, levelCubeTile.CalculatedPosition, Quaternion.identity);
            
            instantiatedObject.transform.localScale = CalcTileScale(levelCubeTile); 

            if (levelCubeTile.IsLightTile)
            {
                ConfigureLightCubeTile(instantiatedObject);
            }

            
            // create scale v3 according to height value
            Vector3 CalcTileScale(CubeTileModel cubeTileData) => new Vector3(1, cubeTileData.Height, 1);
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