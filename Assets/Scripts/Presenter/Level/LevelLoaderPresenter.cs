using System;
using System.Linq;
using Model.Level;
using Presenter.Procedure;
using Presenter.Robot;
using Presenter.Ui;
using UnityEngine;

namespace Presenter.Level
{
    public class LevelLoaderPresenter : MonoBehaviour
    {
        [SerializeField] private LevelModel level;

        private RobotPresenter _robot;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            LoadLevel();
        }

        private void LoadLevel()
        {
            Debug.Log($"load level {level.Id}");
            
            LoadTileMap();
            LoadProcedures();
            LoadUiElements();
            SetCountOfLightCubes();
        }

        private void SetCountOfLightCubes()
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

        private void LoadUiElements()
        {
            CommandsUiPresenter.LoadCommands(level.Commands.ToArray());
        }

        private void LoadTileMap()
        {
            TileMapPresenter.SetLevel(level); 
            TileMapPresenter.BuildMap();
        }

        private void LoadProcedures()
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