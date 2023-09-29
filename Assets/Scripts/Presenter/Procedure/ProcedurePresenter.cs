using System.Collections;
using System.Collections.Generic;
using Model.Level;
using UnityEngine;

namespace Presenter.Procedure
{
    public class ProcedurePresenter : MonoBehaviour
    {
        private List<Procedure> _procedures;
        private int _selectedProcedure = 0;
        
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

        private IEnumerator RunProcedure()
        {
            var selectedProc = GetSelectedProc();
            var commands = selectedProc.Commands;

            foreach (var command in commands)
            {
                yield return StartCoroutine(command.Execute());
            }
        }

        private Procedure GetSelectedProc() => _procedures[_selectedProcedure];
    }
}