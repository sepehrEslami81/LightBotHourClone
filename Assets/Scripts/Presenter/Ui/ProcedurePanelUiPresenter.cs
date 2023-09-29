using System;
using System.Collections.Generic;
using Model.Commands;
using Presenter.Procedure;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.Ui
{
    public class ProcedurePanelUiPresenter : MonoBehaviour
    {
        [SerializeField] private Text procedureLabel;
        [SerializeField] private Transform commandsHolder;

        [Header("Prefabs")] [SerializeField] private GameObject commandButtonPrefab;

        [Header("Color settings")] [SerializeField]
        private Color normalColor;

        [SerializeField] private Color selectedColor;

        private int _procIndex;
        private bool _isSelected;
        private int _countOfCommands;
        private Image _procHolderImage;
        private Dictionary<int, CommandButtonPresenter> _buttons;

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

            _buttons = new Dictionary<int, CommandButtonPresenter>();
        }

        internal void SetProcedureLabel(string label)
        {
            procedureLabel.text = label;
        }

        internal void SetProcedureIndex(int index)
        {
            _procIndex = index;
        }

        public void SelectProcedure()
        {
            ProcedurePresenter.SelectProcedureById(_procIndex);
        }

        internal void AddCommand(CommandNames command)
        {
            var btn = CreateButton();
            btn.UpdateButtonUi(command, CommandButtonPlace.InProcedure);

            var index = _countOfCommands++; // index of object
            _buttons.Add(index, btn);

            btn.procedureIndex = _procIndex;
            btn.commandIndexInProcedure = index;

            CommandButtonPresenter CreateButton() =>
                CommandButtonPresenter.CreateCommandButton(commandButtonPrefab, commandsHolder);
        }

        internal void RemoveCommand(int index)
        {
            var btn = _buttons[index].gameObject;
            Destroy(btn);
            
            _buttons.Remove(index);
        }
    }
}