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
            foreach (var procModel in level.Procedures)
            {
                ProcedurePresenter.CreateProcedure(procModel);
            }
            
            ProcedurePresenter.SelectProcedureById(0, true);
        }

    }
}