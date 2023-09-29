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
        [SerializeField] private List<OprationCommand> commands;
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

        public static void CreateProcedure(ProcedureModel model)
        {
            Debug.Log($"load {model.Name} procedure");

           _instance.CreateNewProc(model);
           _instance.proceduresUiPresenter.NewProcedurePanel(model);
        }

        public static void SelectProcedureById(int index, bool isSelected)
        {
            _instance.proceduresUiPresenter.SelectProcedureById(index, isSelected);
        }

        public static bool AddNewCommand(CommandNames commandName) => _instance.AddCommand(commandName);

        public void StartProgram()
        {
            StartCoroutine(RunAllProcedures());
        }

        public void StopProgram()
        {
            StopAllCoroutines();
        }
        
        private OprationCommand GetCommandByName(CommandNames commandName) =>
            commands.First(c => c.CommandName == commandName);

        private IEnumerator RunProcedure(Procedure proc)
        {
            var selectedCommands = proc.Commands;

            foreach (var command in selectedCommands)
            {
                yield return command.Execute();
            }
        }
        
        private IEnumerator RunAllProcedures()
        {
            return _procedures.Select(RunProcedure).GetEnumerator();
        }

        private void CreateNewProc(ProcedureModel model)
        {
            var proc = new Procedure(model);
            _instance._procedures.Add(proc);
        }
        
        private Procedure GetSelectedProc() => _procedures[_selectedProcedure];
        
        
        private bool AddCommand(CommandNames commandName)
        {
            var command = GetCommandByName(commandName);
            var selectedProc = GetSelectedProc();
            var result = selectedProc.AddCommand(command);

            if(result)
            {
                var commandPanelUiIndex = proceduresUiPresenter.AddCommandToPanel(_selectedProcedure, commandName);
                
            }

            return result;
        }

    }
}