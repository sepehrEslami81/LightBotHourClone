﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Commands;
using Model.Level;
using Presenter.Command;
using UnityEngine;

namespace Presenter.Procedure
{
    public class ProcedurePresenter : MonoBehaviour
    {
        [SerializeField] private List<OprationCommand> commands;

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

            var proc = new Procedure(model);
            _instance._procedures.Add(proc);
        }

        public bool AddNewCommand(CommandNames commandName)
        {
            var command = GetCommandByName(commandName);
            var selectedProc = GetSelectedProc();
            var result = selectedProc.AddCommand(command);

            return result;
        }

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


        private Procedure GetSelectedProc() => _procedures[_selectedProcedure];
    }
}