﻿using System;
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
        [SerializeField] private LevelModel[] levels;
        
        private int _turnedOnLightCubes;
        private static LevelPresenter _instance;
        
        private LevelModel _currentLevel;

        public static LevelModel[] LevelModels => _instance.levels;
        public static LevelModel CurrentLevel => _instance._currentLevel;
        
        
        public static int CountOfTurnedOnLightCubes
        {
            get => _instance._turnedOnLightCubes;
            set
            {
                _instance._turnedOnLightCubes = value;
                if (_instance.GetCountOfLightCubes() == _instance._turnedOnLightCubes)
                    _instance.LevelCompleted();
            }
        }

        private void Awake()
        {
            _instance = this;

            DontDestroyOnLoad(this);
        }

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

            _currentLevel = model;
            BuildLevel(model);
        }

        private LevelModel GetLevelById(int i) => levels.FirstOrDefault(l => l.Id == i);

        private void BuildLevel(LevelModel level)
        {
            Debug.Log($"load level {level.Id}");

            LoadTileMap(level);
            LoadProcedures(level);
            LoadUiElements(level);
            ResetCompletePanel();
            ResetTurnedOnTiles();
        }
        
        private void ResetCompletePanel()
        {
            var completeUiPresenter = GetCompleteUiPresenter();
            completeUiPresenter.HidePanel();

        }

        private void ResetTurnedOnTiles()
        {
            _turnedOnLightCubes = 0;
        }
        
        private int GetCountOfLightCubes() => _currentLevel.CubeTileModels.Count(c => c.IsLightTile);


        private CompleteLevelUiPresenter GetCompleteUiPresenter()
        {
            var completeUiPresenter = FindObjectOfType<CompleteLevelUiPresenter>();
            if (completeUiPresenter is null)
            {
                throw new NullReferenceException("failed to get complete ui presenter");
            }

            return completeUiPresenter;
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
        
        private void LevelCompleted()
        {
            var completeUi = GetCompleteUiPresenter();
            completeUi.LevelCompleted();
        }
    }
}