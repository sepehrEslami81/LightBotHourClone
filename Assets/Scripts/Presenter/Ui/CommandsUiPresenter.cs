using System;
using Model.Commands;
using Presenter.Procedure;
using UnityEngine;

namespace Presenter.Ui
{
    public class CommandsUiPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject rootPanel;
        [SerializeField] private GameObject commandButtonPrefab;

        private static CommandsUiPresenter _instance;

        private void Awake()
        {
            _instance = this;

            if (rootPanel is null)
                rootPanel = gameObject;
        }

        public static void LoadCommands(CommandNames[] commands)
        {
            _instance.CreateCommandButtons(commands);
        }

        private void CreateCommandButtons(CommandNames[] commands)
        {
            // remove all children for safety
            RemoveChildren();

            foreach (var command in commands)
            {
                var btnPresenter = CreateCommandButton();
                btnPresenter.UpdateButtonUi(command, CommandButtonPlace.InCommandsPanel);
            }

            CommandButtonPresenter CreateCommandButton() => CommandButtonPresenter.CreateCommandButton(
                commandButtonPrefab, rootPanel.transform);
        }


        private void RemoveChildren()
        {
            for (int i = 0; i < rootPanel.transform.childCount; i++)
            {
                var child = rootPanel.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }
    }
}