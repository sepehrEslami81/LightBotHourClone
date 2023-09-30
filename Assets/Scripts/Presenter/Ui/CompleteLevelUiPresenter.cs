using Presenter.Level;
using UnityEngine;

namespace Presenter.Ui
{
    /// <summary>
    /// This class is responsible for managing the level completion menu
    /// </summary>
    public class CompleteLevelUiPresenter : MonoBehaviour
    {
        
        [SerializeField] private GameObject root;
        
        /// <summary>
        /// change panel activate status
        /// </summary>
        /// <param name="s"></param>
        public void ChangePanelActiveStatus(bool s) => root.SetActive(s);

        /// <summary>
        /// Button click listener: load next level
        /// </summary>
        public void LoadNextLevel()
        {
            LevelPresenter.LoadLevelById(LevelPresenter.CurrentLevel.Id + 1);
        }
    }
}