using System;
using System.Linq;
using Model;
using Model.Level;
using Presenter.Cube;
using Presenter.Robot;
using UnityEngine;

namespace Presenter.Level
{
    /// <summary>
    /// This class is responsible for managing the map of tiles
    /// </summary>
    public class TileMapPresenter : MonoBehaviour
    {
        #region PRIVATE_FIELDS

        [SerializeField] private GameObject cubeTilePrefab;

        private RobotPresenter _robot;
        private static TileMapPresenter _instance;

        #endregion

        #region UNITY_METHODS

        private void Awake()
        {
            _instance = this;

            // load robot
            var robotObject = GetRobotObject();
            _robot = GetRobotPresenter(robotObject);
        }

        private void OnDestroy()
        {
            _robot = null;
            _instance = null;
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// Get tiles based on position
        /// </summary>
        /// <param name="position">coordinate of selected tile</param>
        /// <returns>if position is wrong, returns null</returns>
        public static CubeTileModel GetTileByPosition(Position position) =>
            LevelPresenter.CurrentLevel.CubeTileModels.FirstOrDefault(p => p.Position == position);


        /// <summary>
        /// Turns off all tiles that can be turned on
        /// </summary>
        public void ResetAllLightCubes()
        {
            var lightCubeTileModels = LevelPresenter.CurrentLevel.CubeTileModels.Where(c => c.IsLightTile);
            foreach (var lightCubeTileModel in lightCubeTileModels)
            {
                lightCubeTileModel.CubeTilePresenter.ChangeTileStatus(CubeType.TurnedOffTile);
            }
        }

        #endregion


        #region INTERNAL_METHODS

        /// <summary>
        /// Builds the tiles and prepares the stage
        /// </summary>
        internal static void BuildMap()
        {
            _instance.CreateTiles();
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// build cube tile map
        /// </summary>
        private void CreateTiles()
        {
            var currentLevelCubeTileModels = LevelPresenter.CurrentLevel.CubeTileModels;
            foreach (var tileModel in currentLevelCubeTileModels)
            {
                CreateCubeTileObject(tileModel);

                // Places the robot on the starting tile
                if (tileModel.IsStartPoint)
                    _robot.ResetRobot();
            }
        }

        /// <summary>
        /// Creates the tile cube and adjusts its settings such as position, height and tile type
        /// </summary>
        /// <param name="levelCubeTile">cube tile data model</param>
        private void CreateCubeTileObject(CubeTileModel levelCubeTile)
        {
            var instantiatedObject = CreateCubeTile(parent: transform);
            instantiatedObject.transform.localScale = CalcTileScale(levelCubeTile);
            levelCubeTile.CubeTilePresenter = instantiatedObject.GetComponent<CubeTilePresenter>();

            // If the tile can be turned on, it will configure it
            if (levelCubeTile.IsLightTile)
            {
                SetTileAsLightCubeTile(instantiatedObject);
            }


            // calculate scale v3 according to height value
            Vector3 CalcTileScale(CubeTileModel cubeTileData) => new Vector3(1, cubeTileData.Height, 1);


            // Creates a tile object in the scene
            GameObject CreateCubeTile(Transform parent)
            {
                var pos = CalculatedPosition(levelCubeTile.WorldPosition, levelCubeTile.Height);
                var go = Instantiate(cubeTilePrefab, pos, Quaternion.identity);
                go.transform.parent = parent;

                return go;
            }

            // calculate position according tile height
            Vector3 CalculatedPosition(Vector3 worldPos, int height) =>
                new Vector3(worldPos.x, height / 2f, worldPos.z);
        }

        /// <summary>
        /// Turns the tile into a light tile
        /// </summary>
        /// <param name="instantiatedObject">tile game object</param>
        private void SetTileAsLightCubeTile(GameObject instantiatedObject)
        {
            instantiatedObject.tag = "LightCube";

            if (instantiatedObject.TryGetComponent(out CubeTilePresenter cube))
            {
                cube.ChangeTileStatus(CubeType.TurnedOffTile);
            }
            else
            {
                Debug.LogError("failed to get CubeTile component from cube!");
            }
        }

        /// <summary>
        /// find robot object with "Player" tag
        /// </summary>
        /// <returns>robot GameObject</returns>
        /// <exception cref="NullReferenceException">Nothing found with player tag</exception>
        private GameObject GetRobotObject()
        {
            var robotObject = GameObject.FindWithTag("Player");
            if (robotObject is null)
            {
                throw new NullReferenceException("player object not found!");
            }

            return robotObject;
        }

        /// <summary>
        /// get robot presenter from roboto GameObject
        /// </summary>
        /// <param name="robotObject">robot GameObject</param>
        /// <returns>if presenter doesnt load on object, then return null</returns>
        private RobotPresenter GetRobotPresenter(GameObject robotObject)
        {
            if (robotObject.TryGetComponent(out RobotPresenter presenter))
            {
                return presenter;
            }

            Debug.LogError("failed to load robot presenter.");
            return null;
        }

        #endregion
    }
}