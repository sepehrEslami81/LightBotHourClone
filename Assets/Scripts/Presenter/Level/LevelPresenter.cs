using System;
using System.Collections;
using System.Linq;
using Model.Level;
using Presenter.Procedure;
using Presenter.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Presenter.Level
{
    public class LevelPresenter : MonoBehaviour
    {
        #region PRIVATE_FIELDS

        [SerializeField] private LevelModel[] levels;

        private int _turnedOnLightCubes;
        private LevelModel _currentLevel;
        private static LevelPresenter _instance;
        
        #endregion

        #region PUBLIC_PROPS

        public static LevelModel[] LevelModels => _instance.levels;
        public static LevelModel CurrentLevel => _instance._currentLevel;

        /// <summary>
        /// Save the number of tiles that are lit. If all the tiles are lit, the game is over
        /// </summary>
        public static int CountOfTurnedOnLightCubes
        {
            get => _instance._turnedOnLightCubes;
            set
            {
                _instance._turnedOnLightCubes = value;
                if (_instance.CountOfLightCubes == _instance._turnedOnLightCubes)
                    _instance.LevelCompleted();
            }
        }

        #endregion

        #region PRIVATE_PROBS
        /// <summary>
        /// Calculate the number of tiles that can be lit (IsLightTile == true)
        /// </summary>
        private int CountOfLightCubes => _currentLevel.CubeTileModels.Count(c => c.IsLightTile);
        #endregion

        #region UNITY_METHODS

        private void Awake()
        {
            _instance = this;

            DontDestroyOnLoad(this);
        }

        #endregion

        #region INTERNAL_METHODS

        /// <summary>
        /// Loads the selected level based on level id
        /// </summary>
        /// <param name="id">selected level id to load</param>
        internal static void LoadLevelById(int id)
        {
            var level = _instance.GetLevelById(id);
            if (level is null)
            {
                Debug.LogWarning($"level {id} not found!");
                return;
            }

            _instance.StartLoadLevel(level);
        }

        #endregion


        #region PRIVATE_METHODs

        /// <summary>
        /// start coroutine for load level scene
        /// </summary>
        /// <param name="model">selected level model</param>
        private void StartLoadLevel(LevelModel model)
        {
            StartCoroutine(WaitForLoadComplete(model));
        }

        /// <summary>
        /// load scene and create waiter to complete loading progress
        /// </summary>
        /// <param name="model">select level model</param>
        /// <returns></returns>
        private IEnumerator WaitForLoadComplete(LevelModel model)
        {
            var loadingData = SceneManager.LoadSceneAsync("GameScene");

            // wait for complete async opration
            while (!loadingData.isDone)
            {
                // Creating an break in the processor to protect the processor from blocking
                yield return new WaitForFixedUpdate();
            }

            _currentLevel = model;
            BuildLevel(model);
        }

        /// <summary>
        /// get level model by id
        /// </summary>
        /// <param name="id">selected level id</param>
        /// <returns></returns>
        private LevelModel GetLevelById(int id) => levels.FirstOrDefault(l => l.Id == id);

        /// <summary>
        /// build level map, commands and procedure and update ui
        /// </summary>
        /// <param name="level">selected level model</param>
        private void BuildLevel(LevelModel level)
        {
            ResetCompletePanel();
            ResetTurnedOnTiles();
            TileMapPresenter.BuildMap();
            LoadProcedures(level);
            CommandsUiPresenter.LoadCommands(level.Commands.ToArray());


            void ResetTurnedOnTiles() => _turnedOnLightCubes = 0;
        }

        /// <summary>
        /// reset complete level ui
        /// </summary>
        private void ResetCompletePanel()
        {
            var completeUiPresenter = GetCompleteUiPresenter();
            completeUiPresenter.ChangePanelActiveStatus(false);
        }

        /// <summary>
        /// get complete ui presenter MonoBehaviour script 
        /// </summary>
        /// <returns>MonoBehaviour script</returns>
        /// <exception cref="NullReferenceException">when this script doesnt loaded in scene</exception>
        private CompleteLevelUiPresenter GetCompleteUiPresenter()
        {
            var completeUiPresenter = FindObjectOfType<CompleteLevelUiPresenter>();
            if (completeUiPresenter is null)
            {
                throw new NullReferenceException("failed to get complete ui presenter");
            }

            return completeUiPresenter;
        }


        /// <summary>
        /// create level procedures
        /// </summary>
        /// <param name="level"></param>
        private void LoadProcedures(LevelModel level)
        {
            for (int i = 0; i < level.Procedures.Count; i++)
            {
                var proc = level.Procedures[i];
                ProcedurePresenter.CreateProcedure(proc, i);
            }

            ProcedurePresenter.SelectProcedureById(0);
        }

        /// <summary>
        /// This command is executed when all the tiles are lit and the screen shows the end of the step
        /// </summary>
        private void LevelCompleted()
        {
            var completeUi = GetCompleteUiPresenter();
            completeUi.ChangePanelActiveStatus(true);
        }

        #endregion
    }
}