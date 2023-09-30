using System.Linq;
using Model.Level;
using Presenter.Level;
using UnityEngine;

namespace Presenter.Ui
{
    public class MenuUiPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject levelCardPrefab;

        private void Start()
        {
            LoadLevelCards();
        }

        private void LoadLevelCards()
        {
            var levels = LevelLoaderPresenter.LevelModels.OrderBy(l => l.Id);
            foreach (var levelModel in levels)
            {
                LevelCardUiPresenter.CreateNewLevelCard(levelCardPrefab, transform, levelModel);
            }
        }
    }
}