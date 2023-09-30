using Model.Level;
using Presenter.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.Ui
{
    
    /// <summary>
    /// This class is responsible for managing the button to enter the level
    /// </summary>
    public class LevelCardUiPresenter : MonoBehaviour
    {
        [SerializeField] private Text label;
        
        private LevelModel _levelModel;

        
        /// <summary>
        /// create level card object with prefab
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <param name="levelModel"></param>
        public static void CreateNewLevelCard(GameObject prefab, Transform parent, LevelModel levelModel)
        {
            var instantiate = Instantiate(prefab, parent);
            var component = instantiate.GetComponent<LevelCardUiPresenter>();

            component._levelModel = levelModel;
            component.label.text = levelModel.Id.ToString();
        }


        /// <summary>
        /// Button click listener: load level
        /// </summary>
        public void ClickEventListener()
        {
            LevelPresenter.LoadLevelById(_levelModel.Id);
        }
    }
}