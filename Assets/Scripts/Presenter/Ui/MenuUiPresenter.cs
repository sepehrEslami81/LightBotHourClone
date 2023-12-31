﻿using System.Linq;
using Presenter.Level;
using UnityEngine;

namespace Presenter.Ui
{
    
    /// <summary>
    /// This class is responsible for managing the levels menu
    /// </summary>
    public class MenuUiPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject levelCardPrefab;

        private void Start()
        {
            LoadLevelCards();
        }

        /// <summary>
        /// load levels buttons
        /// </summary>
        private void LoadLevelCards()
        {
            var levels = LevelPresenter.LevelModels.OrderBy(l => l.Id);
            foreach (var levelModel in levels)
            {
                LevelCardUiPresenter.CreateNewLevelCard(levelCardPrefab, transform, levelModel);
            }
        }
    }
}