using System;
using UnityEngine;

namespace Model.Level
{
    [Serializable]
    public class ProcedureModel
    {
        [SerializeField] private string name = "main";
        [SerializeField] private int maximumCommands = 0; // 0 means can use infinite commands in this proc

        public string Name => name;
        public int MaximumCommands => maximumCommands;
    }
}