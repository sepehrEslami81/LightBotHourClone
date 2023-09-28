using System.Collections;
using Presenter.Command;
using UnityEngine;

namespace Presenter.Procedure
{
    public class ProcedurePresenter : MonoBehaviour
    {
        [Range(1, 5)] [SerializeField] private int proceduresCount = 1;

        private Procedure[] _procedures;
        private int _selectedProcedure = 0;

        private IEnumerator Start()
        {
            CreateProcedures(proceduresCount);

            var robot = GameObject.FindWithTag("Player");
            var forwardCommand = robot.GetComponent<ForwardWalkCommandPresenter>();
            var jumpCommand = robot.GetComponent<JumpCommandPresenter>();
            var lightStateCommand = robot.GetComponent<ChangeLightStateCommandPresenter>();

            // _procedures[0].AddCommand(jumpCommand);
            // _procedures[0].AddCommand(jumpCommand);
            // _procedures[0].AddCommand(jumpCommand);
            _procedures[0].AddCommand(forwardCommand);
            _procedures[0].AddCommand(forwardCommand);
            _procedures[0].AddCommand(forwardCommand);
            _procedures[0].AddCommand(lightStateCommand);

            yield return new WaitForSeconds(.5f);
            StartCoroutine(RunProcedure());
        }

        public void CreateProcedures(int count)
        {
            _procedures = new Procedure[count];
            for (var i = 0; i < _procedures.Length; i++)
                _procedures[0] = new Procedure();
        }

        public IEnumerator RunProcedure()
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