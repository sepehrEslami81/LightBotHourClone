using System;
using Model.Level;
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

        internal void LoadLevel()
        {
            var tileMapPresenter = GetTileMapPresenter();
            
            tileMapPresenter.SetLevel(level); 
            tileMapPresenter.CreateLevel();
        }


        private TileMapPresenter GetTileMapPresenter()
        {
            var component = GameObject.FindObjectOfType<TileMapPresenter>();
            if (component is null)
            {
                throw new NullReferenceException("failed to find Tile map presenter");
            }
            
            return component;
        }

    }
}