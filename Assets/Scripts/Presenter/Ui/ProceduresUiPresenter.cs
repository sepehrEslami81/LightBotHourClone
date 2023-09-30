using System.Collections.Generic;
using Model.Commands;
using Model.Level;
using UnityEngine;

namespace Presenter.Ui
{
    /// <summary>
    /// This class is responsible for managing procedure panels.
    /// </summary>
    public class ProceduresUiPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject rootPanel;
        [SerializeField] private GameObject procedurePrefab;

        private List<ProcedurePanelUiPresenter> _procedures;

        private void Awake()
        {
            if (rootPanel is null)
                rootPanel = gameObject;

            _procedures = new List<ProcedurePanelUiPresenter>();
        }

        private void OnDestroy()
        {
            _procedures.Clear();
        }
        
        /// <summary>
        /// select procedure ui panel by id
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isSelected"></param>
        public void SelectProcedureById(int index, bool isSelected)
        {
            _procedures[index].IsSelected = isSelected;
        }
        
        /// <summary>
        /// create new procedure ui panel by id
        /// </summary>
        /// <param name="model"></param>
        /// <param name="index"></param>
        public void NewProcedurePanel(ProcedureModel model, int index)
        {
            var procPanel = Instantiate(procedurePrefab, rootPanel.transform);

            if (procPanel.TryGetComponent(out ProcedurePanelUiPresenter presenter))
            {
                presenter.SetProcedureLabel(model.Name);
                presenter.SetProcedureIndex(index);
                
                _procedures.Add(presenter);
                
            }
            else
            {
                Debug.LogError("failed to get procedure panel ui presenter");
            }
        }

        /// <summary>
        /// add new command to select ui panel by id
        /// </summary>
        /// <param name="index"></param>
        /// <param name="commandName"></param>
        public void AddCommandToPanel(int index, CommandName commandName)
        {
            _procedures[index].AddCommand(commandName);
        }

        
        /// <summary>
        /// remove command from ui panel by id
        /// </summary>
        /// <param name="procIndex"></param>
        /// <param name="commandIndex"></param>
        public void RemoveCommandByIndex(int procIndex, int commandIndex)
        {
            _procedures[procIndex].RemoveCommand(commandIndex);
        }
    }
}