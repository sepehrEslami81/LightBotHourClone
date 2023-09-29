using System;
using System.Collections.Generic;
using Model.Level;
using UnityEngine;

namespace Presenter.Ui
{
    public class ProceduresUiPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject rootPanel;
        [SerializeField] private GameObject procedurePrefab;

        private static ProceduresUiPresenter _instance;
        private List<ProcedurePanelUiPresenter> _procedures;

        private void Awake()
        {
            _instance = this;

            if (rootPanel is null)
                rootPanel = gameObject;

            _procedures = new List<ProcedurePanelUiPresenter>();
        }

        private void OnDestroy()
        {
            _instance = this;
            _procedures.Clear();
        }

        public static void AddNewProcedure(ProcedureModel model)
        {
            _instance.CreateProcedurePanel(model);
        }

        public void SelectProcedureById(int index, bool isSelected)
        {
            _procedures[index].IsSelected = isSelected;
        }
        
        private void CreateProcedurePanel(ProcedureModel model)
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
    }
}