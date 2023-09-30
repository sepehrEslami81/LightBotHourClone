using System;
using System.Collections.Generic;
using Model.Commands;
using Presenter.Procedure;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.Ui
{
    
    /// <summary>
    /// This class is responsible for managing the procedure panel.
    /// such as selecting, adding or removing commands and etc.
    /// </summary>
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

        /// <summary>
        /// panel selection status and update panel ui when change selection status
        /// </summary>
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

        /// <summary>
        /// set label
        /// </summary>
        /// <param name="label"></param>
        internal void SetProcedureLabel(string label)
        {
            procedureLabel.text = label.ToUpper();
        }

        /// <summary>
        /// set current procedure index
        /// </summary>
        /// <param name="index"></param>
        internal void SetProcedureIndex(int index)
        {
            _procIndex = index;
        }

        /// <summary>
        /// Button click listener: select this procedure
        /// </summary>
        public void SelectProcedure()
        {
            ProcedurePresenter.SelectProcedureById(_procIndex);
        }

        /// <summary>
        /// add command to ui panel
        /// </summary>
        /// <param name="command"></param>
        internal void AddCommand(CommandName command)
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

        
        /// <summary>
        /// remove command from panel ui
        /// </summary>
        /// <param name="index"></param>
        internal void RemoveCommand(int index)
        {
            var btn = _buttons[index].gameObject;
            Destroy(btn);
            
            _buttons.Remove(index);
        }
    }
}