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

    public class CommandButtonPresenter : MonoBehaviour
    {
        [HideInInspector] public int procedureIndex;
        [HideInInspector] public int commandIndexInProcedure;
        
        [SerializeField] private Image image;
        [SerializeField] private List<CommandSpriteModel> sprites;

        public static CommandButtonPresenter CreateCommandButton(GameObject prefab, Transform parent)
        {
            var btn = Instantiate(prefab, parent);
            if (btn.TryGetComponent(out CommandButtonPresenter presenter))
            {
                return presenter;
            }

            throw new NullReferenceException("failed to get command button presenter.");
        }

        public void UpdateButtonUi(CommandNames command, CommandButtonPlace buttonPlace)
        {
            SetIcon(command);
            MakeButton(command, buttonPlace);
        }

        private void SetIcon(CommandNames command)
        {
            var sprite = sprites.First(p => p.CommandName == command);
            image.sprite = sprite.IconSprite;
        }

        private void MakeButton(CommandNames command, CommandButtonPlace commandButtonPlace)
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

        private void AddCommandToSelectedProc(CommandNames command)
        {
            print($"try add {command.ToString()} to selected proc");
            ProcedurePresenter.AddNewCommand(command);
        }

        private void RemoveCommandFromSelectedProc()
        {
            print($"try remove from selected proc");
        }
    }
}