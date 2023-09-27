using System;
using System.Linq;
using Cube;
using Model;
using Model.Level;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Level
{
    public class LevelPresenter : MonoBehaviour
    {
        [SerializeField] private LevelModel level;
        [SerializeField] private GameObject tileCubePrefab;

        private static LevelPresenter _instance;
        private RobotPresenter _robot;
        
        public static CubeTileModel StartTile => _instance.level.CubeTileModels.First(p => p.IsStartPoint);
        
        private void Awake()
        {
            _instance = this;
            
            var robotObject = GetPlayerObject();
            SetRobotPresenter(robotObject);
        }
        
        private void Start()
        {
            LoadLevel();
        }

        public static CubeTileModel GetTileByPosition(Position position) =>
            _instance.level.CubeTileModels.FirstOrDefault(p => p.Position == position);
        

        internal void LoadLevel()
        {
            print("loading level " + level.Id);

            foreach (var cubeTile in level.CubeTileModels)
            {
                LoadCubeTile(cubeTile);

                if (cubeTile.IsStartPoint)
                {
                    _robot.ResetRobotPosition();
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
        
        
        private GameObject GetPlayerObject()
        {
            var robotObject = GameObject.FindWithTag("Player");
            if (robotObject is null)
            {
                throw new NullReferenceException("player object not found!");
            }

            return robotObject;
        }

        private void SetRobotPresenter(GameObject robotObject)
        {
            if (robotObject.TryGetComponent(out RobotPresenter presenter))
            {
                _robot = presenter;
            }
            else
            {
                Debug.LogError("failed to load robot presenter.");
            }
        }
    }
}