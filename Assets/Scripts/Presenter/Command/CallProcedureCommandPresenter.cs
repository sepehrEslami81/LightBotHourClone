using System.Collections;
using Presenter.Procedure;
using UnityEngine;

namespace Presenter.Command
{
    /// <summary>
    /// Executes commands inside a procedure
    /// </summary>
    public class CallProcedureCommandPresenter : OperationCommand
    {
        [SerializeField] private int procedureIndex = 1; // Index 0 is used for the main procedure
        
        [Header("References")]
        [SerializeField] private ProcedurePresenter procedurePresenter;

        
        /// <summary>
        /// By using the procedure index, it executes the commands inside that procedure.
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Execute()
        {
            var proc = procedurePresenter.GetProcedureByIndex(procedureIndex);
            yield return proc.RunProcedure();
        }
    }
}