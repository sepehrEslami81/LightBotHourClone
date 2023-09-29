using System.Collections;
using Presenter.Procedure;
using UnityEngine;

namespace Presenter.Command
{
    public class CallProcedureCommandPresenter : OprationCommand
    {
        [SerializeField] private int procedureIndex = 1; // use index 0 for main proc
        
        [Header("References")]
        [SerializeField] private ProcedurePresenter procedurePresenter;

        public override IEnumerator Execute()
        {
            var proc = procedurePresenter.GetProcedureByIndex(procedureIndex);
            yield return proc.RunProcedure();
        }
    }
}