using Model.Commands;
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

        /// <summary>
        /// load level commands to commands list panel
        /// </summary>
        /// <param name="commands"></param>
        public static void LoadCommands(CommandName[] commands)
        {
            _instance.CreateCommandButtons(commands);
        }

        /// <summary>
        /// load level commands to commands list panel
        /// </summary>
        /// <param name="commands"></param>
        private void CreateCommandButtons(CommandName[] commands)
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


        /// <summary>
        /// remove all children of commands list
        /// </summary>
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