using System.Collections.Generic;
using Model.Commands;
using Model.Level;
using UnityEngine;

namespace Presenter.Ui
{
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
        
        public void SelectProcedureById(int index, bool isSelected)
        {
            _procedures[index].IsSelected = isSelected;
        }
        
        public void NewProcedurePanel(ProcedureModel model)
        {
            var procPanel = Instantiate(procedurePrefab, rootPanel.transform);

            if (procPanel.TryGetComponent(out ProcedurePanelUiPresenter presenter))
            {
                presenter.SetProcedureLabel(model.Name);
                
                _procedures.Add(presenter);
                
            }
            else
            {
                Debug.LogError("failed to get procedure panel ui presenter");
            }
        }

        public void AddCommandToPanel(int index, CommandNames commandName)
        {
            _procedures[index].AddCommand(commandName, index);
        }

        public void RemoveCommandByIndex(int procIndex, int commandIndex)
        {
            _procedures[procIndex].RemoveCommand(commandIndex);
        }
    }
}