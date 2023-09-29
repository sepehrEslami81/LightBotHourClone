using UnityEngine;
using UnityEngine.Serialization;

namespace Presenter.Ui
{
    public class CompleteLevelUiPresenter : MonoBehaviour
    {
        
        [SerializeField] private GameObject root;
        private int _turnedOnLightCubes;
        
        public int CountOfLightCubes { get; set; }

        public int CountOfTurnedOnLightCubes
        {
            get => _turnedOnLightCubes;
            set
            {
                _turnedOnLightCubes = value;
                if (CountOfLightCubes == _turnedOnLightCubes)
                    LevelCompleted();
            }
        }

        public void HidePanel() => root.SetActive(false);
        
        private void LevelCompleted()
        {
            root.SetActive(true);
        }
    }
}