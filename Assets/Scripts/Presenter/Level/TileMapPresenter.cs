using System;
using System.Linq;
using Cube;
using Model;
using Model.Level;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Level
{
    public class TileMapPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject cubeTilePrefab;

        private LevelModel _level;
        private RobotPresenter _robot;
        private static TileMapPresenter _instance;

        private void Awake()
        {
            _instance = this;

            // load robot
            var robotObject = GetRobotObject();
            _robot = GetRobotPresenter(robotObject);
        }

        private void OnDestroy()
        {
            _level = null;
            _robot = null;
            _instance = null;
        }

        public static CubeTileModel StartTile => _instance._level.CubeTileModels.First(p => p.IsStartPoint);

        public static CubeTileModel GetTileByPosition(Position position) =>
            _instance._level.CubeTileModels.FirstOrDefault(p => p.Position == position);


        internal void SetLevel(LevelModel levelModel) => _level = levelModel;

        internal void CreateLevel()
        {
            foreach (var tileModel in _level.CubeTileModels)
            {
                LoadCubeTile(tileModel);

                if (tileModel.IsStartPoint)
                    SetRobotAtStartPosition();
            }
        }

        private void SetRobotAtStartPosition()
        {
            _robot.ResetRobotPosition();
        }

        private void LoadCubeTile(CubeTileModel levelCubeTile)
        {
            var instantiatedObject = CreateCubeTile();
            instantiatedObject.transform.localScale = CalcTileScale(levelCubeTile);
            
            SetParent(instantiatedObject);

            if (levelCubeTile.IsLightTile)
            {
                SetTileAsLightCubeTile(instantiatedObject);
            }

            // create scale v3 according to height value
            Vector3 CalcTileScale(CubeTileModel cubeTileData) => new Vector3(1, cubeTileData.Height, 1);

            GameObject CreateCubeTile() =>
                Instantiate(cubeTilePrefab, levelCubeTile.CalculatedPosition, Quaternion.identity);

            void SetParent(GameObject cube) => cube.transform.parent = transform;
        }

        private void SetTileAsLightCubeTile(GameObject instantiatedObject)
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

        private GameObject GetRobotObject()
        {
            var robotObject = GameObject.FindWithTag("Player");
            if (robotObject is null)
            {
                throw new NullReferenceException("player object not found!");
            }

            return robotObject;
        }

        private RobotPresenter GetRobotPresenter(GameObject robotObject)
        {
            if (robotObject.TryGetComponent(out RobotPresenter presenter))
            {
                return presenter;
            }
            else
            {
                Debug.LogError("failed to load robot presenter.");
                return null;
            }
        }
    }
}