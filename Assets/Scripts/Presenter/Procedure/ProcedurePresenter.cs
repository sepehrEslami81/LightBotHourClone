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
        #region PRIVATE_FIELDS

        [SerializeField] private List<OperationCommand> commands;
        [SerializeField] private ProceduresUiPresenter proceduresUiPresenter;

        private int _selectedProcedure;
        private List<Procedure> _procedures;
        private static ProcedurePresenter _instance;

        #endregion

        #region UNITY_METHODS

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

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// add new procedure into memory by procedure model
        /// </summary>
        /// <param name="model">procedure model</param>
        /// <param name="index">procedure id (index)</param>
        public static void CreateProcedure(ProcedureModel model, int index)
        {
            Debug.Log($"load {model.Name} procedure");

            _instance.CreateNewProc(model);
            _instance.proceduresUiPresenter.NewProcedurePanel(model, index);
        }

        /// <summary>
        /// select procedure. update ui (disable previous procedure and select current procedure) and
        /// store selected procedure index
        /// </summary>
        /// <param name="index">new procedure to select</param>
        public static void SelectProcedureById(int index)
        {
            _instance.proceduresUiPresenter.SelectProcedureById(_instance._selectedProcedure, false);
            _instance._selectedProcedure = index;
            _instance.proceduresUiPresenter.SelectProcedureById(index, true);
        }

        /// <summary>
        /// add new command to selected procedure
        /// </summary>
        /// <param name="commandName">selected command</param>
        public static void AddNewCommand(CommandName commandName) => _instance.AddCommand(commandName);


        /// <summary>
        /// remove command from procedure by procedure index
        /// </summary>
        /// <param name="procIndex">procedure index</param>
        /// <param name="commandIndex">command index</param>
        public static void RemoveCommand(int procIndex, int commandIndex) =>
            _instance.RemoveCommandByIndex(procIndex, commandIndex);

        /// <summary>
        /// start main procedure coroutine
        /// </summary>
        public void StartProgram()
        {
            StartCoroutine(RunMainProc());
        }

        /// <summary>
        /// stop all coroutines for stop main procedure
        /// </summary>
        public void StopProgram()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// get procedure by procedure id
        /// </summary>
        /// <param name="index">procedure id</param>
        /// <returns>procedure</returns>
        public Procedure GetProcedureByIndex(int index) => _procedures[index];

        #endregion


        #region PRIVATE_METHODS

        /// <summary>
        /// Get a command from the command repository
        /// </summary>
        /// <param name="commandName">command name</param>
        /// <returns>selected command MonoBehaviour script</returns>
        private OperationCommand GetCommandByName(CommandName commandName) =>
            commands.First(c => c.CommandName == commandName);


        /// <summary>
        /// start main procedure (index 0) coroutine 
        /// </summary>
        /// <returns></returns>
        private IEnumerator RunMainProc()
        {
            yield return _procedures[0].RunProcedure();
        }

        /// <summary>
        /// create new procedure by procedure model and add to procedures
        /// </summary>
        /// <param name="model"></param>
        private void CreateNewProc(ProcedureModel model)
        {
            var proc = new Procedure(model);
            _instance._procedures.Add(proc);
        }


        /// <summary>
        /// add command to procedure commands and show command button in procedure panel
        /// </summary>
        /// <param name="commandName"></param>
        private void AddCommand(CommandName commandName)
        {
            var command = GetCommandByName(commandName);
            var selectedProc = GetSelectedProc();
            var result = selectedProc.AddCommand(command);

            if (result)
            {
                proceduresUiPresenter.AddCommandToPanel(_selectedProcedure, commandName);
            }

            
            Procedure GetSelectedProc() => GetProcedureByIndex(_selectedProcedure);
        }

        
        /// <summary>
        /// remove command by procedure and command index
        /// </summary>
        /// <param name="procIndex"></param>
        /// <param name="commandIndex"></param>
        private void RemoveCommandByIndex(int procIndex, int commandIndex)
        {
            var selectedProc = _procedures[procIndex];
            selectedProc.RemoveCommand(commandIndex);
            proceduresUiPresenter.RemoveCommandByIndex(procIndex, commandIndex);
        }

        #endregion
    }
}