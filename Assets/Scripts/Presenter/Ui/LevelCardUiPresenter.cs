using Model.Level;
using Presenter.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.Ui
{
    public class LevelCardUiPresenter : MonoBehaviour
    {
        [SerializeField] private Text label;
        
        private LevelModel _levelModel;

        public static void CreateNewLevelCard(GameObject prefab, Transform parent, LevelModel levelModel)
        {
            var instantiate = Instantiate(prefab, parent);
            var component = instantiate.GetComponent<LevelCardUiPresenter>();

            component._levelModel = levelModel;
            component.label.text = levelModel.Id.ToString();
        }


        public void ClickEventListener()
        {
            LevelLoaderPresenter.LoadLevel(_levelModel);
        }
    }
}