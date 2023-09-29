using System;
using Model.Level;
using Presenter.Procedure;
using Presenter.Robot;
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
        }

    }
}