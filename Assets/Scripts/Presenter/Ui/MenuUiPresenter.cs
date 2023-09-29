using System.Linq;
using Model.Level;
using UnityEngine;

namespace Presenter.Ui
{
    public class MenuUiPresenter : MonoBehaviour
    {
        [SerializeField] private LevelModel[] levelModels;
        [SerializeField] private GameObject levelCardPrefab;

        private void Start()
        {
            LoadLevelCards();
        }

        private void LoadLevelCards()
        {
            foreach (LevelModel levelModel in levelModels.OrderBy(l => l.Id))
            {
                LevelCardUiPresenter.CreateNewLevelCard(levelCardPrefab, transform, levelModel);
            }
        }
    }
}