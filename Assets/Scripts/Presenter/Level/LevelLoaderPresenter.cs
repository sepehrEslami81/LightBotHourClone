using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Level;
using Presenter.Procedure;
using Presenter.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Presenter.Level
{
    public class LevelLoaderPresenter : MonoBehaviour
    {
        private static LevelLoaderPresenter _instance;

        private void Awake()
        {
            _instance = this;

            DontDestroyOnLoad(this);
        }

        internal static void LoadLevel(LevelModel model)
        {
            _instance.StartLoadLevel(model);
        }

        private void StartLoadLevel(LevelModel model)
        {
            StartCoroutine(WaitForLoadComplete(model));
        }
        
        private IEnumerator WaitForLoadComplete(LevelModel model)
        {
            var loadingData = SceneManager.LoadSceneAsync("GameScene");

            while (!loadingData.isDone)
            {
                yield return new WaitForFixedUpdate();
            }
            
            LoadLevelByModel(model);
        }

        private void LoadLevelByModel(LevelModel level)
        {
            Debug.Log($"load level {level.Id}");

            LoadTileMap(level);
            LoadProcedures(level);
            LoadUiElements(level);
            SetCountOfLightCubes(level);
        }

        private void SetCountOfLightCubes(LevelModel level)
        {
            var completeUiPresenter = FindObjectOfType<CompleteLevelUiPresenter>();
            if (completeUiPresenter is null)
            {
                Debug.LogError("failed to get level complete presenter");
                return;
            }

            completeUiPresenter.HidePanel();
            completeUiPresenter.CountOfLightCubes = GetCountOfLightCubes();

            int GetCountOfLightCubes() => level.CubeTileModels.Count(c => c.IsLightTile);
        }

        private void LoadUiElements(LevelModel level)
        {
            CommandsUiPresenter.LoadCommands(level.Commands.ToArray());
        }

        private void LoadTileMap(LevelModel level)
        {
            TileMapPresenter.SetLevel(level);
            TileMapPresenter.BuildMap();
        }

        private void LoadProcedures(LevelModel level)
        {
            for (int i = 0; i < level.Procedures.Count; i++)
            {
                var proc = level.Procedures[i];
                ProcedurePresenter.CreateProcedure(proc, i);
            }

            ProcedurePresenter.SelectProcedureById(0, true);
        }
    }
}