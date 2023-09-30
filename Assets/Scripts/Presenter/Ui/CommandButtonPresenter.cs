using System;
using System.Collections.Generic;
using System.Linq;
using Model.Commands;
using Model.Ui;
using Presenter.Procedure;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.Ui
{
    public enum CommandButtonPlace
    {
        InCommandsPanel,
        InProcedure
    }

    /// <summary>
    /// This class is responsible for managing the command button in the procedures panel and command list
    /// </summary>
    public class CommandButtonPresenter : MonoBehaviour
    {
        [HideInInspector] public int procedureIndex;
        [HideInInspector] public int commandIndexInProcedure;
        
        [SerializeField] private Image image;
        [SerializeField] private List<CommandSpriteModel> sprites;

        
        /// <summary>
        /// create command button object by prefab and set as child of selected parent
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <returns>presenter of command button</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static CommandButtonPresenter CreateCommandButton(GameObject prefab, Transform parent)
        {
            var btn = Instantiate(prefab, parent);
            if (btn.TryGetComponent(out CommandButtonPresenter presenter))
            {
                return presenter;
            }

            throw new NullReferenceException("failed to get command button presenter.");
        }

        /// <summary>
        /// update button icon and set click event listener
        /// </summary>
        /// <param name="command"></param>
        /// <param name="buttonPlace"></param>
        public void UpdateButtonUi(CommandName command, CommandButtonPlace buttonPlace)
        {
            SetIcon(command);
            MakeButton(command, buttonPlace);
        }

        
        /// <summary>
        /// set button icon according by command name
        /// </summary>
        /// <param name="command"></param>
        private void SetIcon(CommandName command)
        {
            var sprite = sprites.First(p => p.CommandName == command);
            image.sprite = sprite.IconSprite;
        }

        /// <summary>
        /// set click event listener by button place
        /// </summary>
        /// <param name="command"></param>
        /// <param name="commandButtonPlace"></param>
        private void MakeButton(CommandName command, CommandButtonPlace commandButtonPlace)
        {
            if (TryGetComponent(out Button btnComponent))
            {
                btnComponent.onClick.RemoveAllListeners();

                if (commandButtonPlace == CommandButtonPlace.InCommandsPanel)
                    btnComponent.onClick.AddListener(() => AddCommandToSelectedProc(command));
                else
                    btnComponent.onClick.AddListener(RemoveCommandFromSelectedProc);
            }
            else
            {
                Debug.LogError("failed to get button component");
            }
        }

        /// <summary>
        /// add command to current procedure
        /// </summary>
        /// <param name="command"></param>
        private void AddCommandToSelectedProc(CommandName command)
        {
            ProcedurePresenter.AddNewCommand(command);
        }

        
        /// <summary>
        /// remove current command from procedure
        /// </summary>
        private void RemoveCommandFromSelectedProc()
        {
            ProcedurePresenter.SelectProcedureById(procedureIndex);
            ProcedurePresenter.RemoveCommand(procedureIndex, commandIndexInProcedure);
        }
    }
}