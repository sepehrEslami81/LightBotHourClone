using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Commands;
using Model.Level;
using Presenter.Command;
using Presenter.Ui;
using UnityEngine;

namespace Presenter.Procedure
{
    public class ProcedurePresenter : MonoBehaviour
    {
        [SerializeField] private List<OperationCommand> commands;
        [SerializeField] private ProceduresUiPresenter proceduresUiPresenter;

        private int _selectedProcedure = 0;
        private List<Procedure> _procedures;
        private static ProcedurePresenter _instance;

        private void Awake()
        {
            _instance = this;

            _procedures = new List<Procedure>();
            _selectedProcedure = 0;
        }

        private void OnDestroy()
        {
            _instance = null;
            _procedures.Clear();
        }

        public static void CreateProcedure(ProcedureModel model, int index)
        {
            Debug.Log($"load {model.Name} procedure");

            _instance.CreateNewProc(model);
            _instance.proceduresUiPresenter.NewProcedurePanel(model, index);
        }

        public static void SelectProcedureById(int index, bool isSelected = true)
        {
            _instance.proceduresUiPresenter.SelectProcedureById(_instance._selectedProcedure, false);
            _instance._selectedProcedure = index;
            _instance.proceduresUiPresenter.SelectProcedureById(index, isSelected);
        }

        public static bool AddNewCommand(CommandNames commandName) => _instance.AddCommand(commandName);

        public static void RemoveCommand(int procIndex, int commandIndex) =>
            _instance.RemoveCommandByIndex(procIndex, commandIndex);

        public void StartProgram()
        {
            StartCoroutine(RunAllProcedures());
        }

        public void StopProgram()
        {
            StopAllCoroutines();
        }

        public Procedure GetProcedureByIndex(int index) => _procedures[index];
        

        private OperationCommand GetCommandByName(CommandNames commandName) =>
            commands.First(c => c.CommandName == commandName);
        
        
        private IEnumerator RunAllProcedures()
        {
            return _procedures.Select(procedure => procedure.RunProcedure()).GetEnumerator();
        }

        private void CreateNewProc(ProcedureModel model)
        {
            var proc = new Procedure(model);
            _instance._procedures.Add(proc);
        }


        private bool AddCommand(CommandNames commandName)
        {
            var command = GetCommandByName(commandName);
            var selectedProc = GetSelectedProc();
            var result = selectedProc.AddCommand(command);

            if (result)
            {
                proceduresUiPresenter.AddCommandToPanel(_selectedProcedure, commandName);
            }

            return result;

            Procedure GetSelectedProc() => GetProcedureByIndex(_selectedProcedure);
        }

        private void RemoveCommandByIndex(int procIndex, int commandIndex)
        {
            var selectedProc = _procedures[procIndex];
            selectedProc.RemoveCommand(commandIndex);
            proceduresUiPresenter.RemoveCommandByIndex(procIndex, commandIndex);
        }
    }
}