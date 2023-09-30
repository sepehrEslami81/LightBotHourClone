using Model.Level;
using Presenter.Level;
using UnityEngine;
using UnityEngine.Serialization;

namespace Presenter.Ui
{
    public class CompleteLevelUiPresenter : MonoBehaviour
    {
        
        [SerializeField] private GameObject root;

        public void HidePanel() => root.SetActive(false);

        public void LoadNextLevel()
        {
            LevelPresenter.LoadLevelById(LevelPresenter.CurrentLevel.Id + 1);
        }
        
        public void LevelCompleted()
        {
            root.SetActive(true);
        }
    }
}