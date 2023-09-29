using System;
using UnityEngine;

namespace Model.Level
{
    [Serializable]
    public class ProcedureModel
    {
        [SerializeField] private string name = "main"; // 0 means infinite command
        [SerializeField] private int maximumCommands = 0; // 0 means infinite command
    }
}