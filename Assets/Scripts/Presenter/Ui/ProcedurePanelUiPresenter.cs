using System;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.Ui
{
    public class ProcedurePanelUiPresenter : MonoBehaviour
    {
        [SerializeField] private Text procedureLabel;
        [SerializeField] private Transform commandsHolder;
        
        [Header("Prefabs")]
        [SerializeField] private GameObject commandButton;
        
        [Header("Color settings")]
        [SerializeField] private Color normalColor;
        [SerializeField] private Color selectedColor;

        private bool _isSelected;
        private Image _procHolderImage;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                _procHolderImage.color = _isSelected ? selectedColor : normalColor;
            }
        }

        private void Awake()
        {
            if (commandsHolder.TryGetComponent(out Image image))
            {
                _procHolderImage = image;
            }
            else
            {
                throw new NullReferenceException("failed to get image");
            }
        }

        public void SetProcedureLabel(string label)
        {
            procedureLabel.text = label;
        }
    }
}