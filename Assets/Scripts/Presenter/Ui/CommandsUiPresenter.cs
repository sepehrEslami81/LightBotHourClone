using System;
using Model.Commands;
using UnityEngine;

namespace Presenter.Ui
{
    public class CommandsUiPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject panelRoot;
        [SerializeField] private GameObject commandButtonPrefab;

        private static CommandsUiPresenter _instance;

        private void Awake()
        {
            _instance = this;
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
                var btnPresenter = CreateCommandButton(command);
                btnPresenter.SetCommand(command, CommandButtonPlace.InCommandsPanel);
            }
        }

        private CommandButtonPresenter CreateCommandButton(CommandNames commandName)
        {
            var btn = Instantiate(commandButtonPrefab, panelRoot.transform);
            if (btn.TryGetComponent(out CommandButtonPresenter presenter))
            {
                return presenter;
            }

            throw new NullReferenceException("failed to get command button presenter.");
        }
        
        private void RemoveChildren()
        {
            for (int i = 0; i < panelRoot.transform.childCount; i++)
            {
                var child = panelRoot.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }
    }
}