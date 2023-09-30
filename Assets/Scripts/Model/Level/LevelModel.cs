using System.Collections.Generic;
using System.Linq;
using Model.Commands;
using Model.Robot;
using UnityEngine;

namespace Model.Level
{
    /// <summary>
    /// A model to store the information of each level. This class is defined as scriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "New Level", menuName = "Levels/Create New Level", order = 0)]
    public class LevelModel : ScriptableObject
    {
        [Header("Level settings")] [SerializeField]
        private int id;

        [SerializeField] private RobotDirection startRobotDirection;

        [Header("Commands Settings")] [SerializeField]
        private CommandName[] commands;

        [SerializeField] private ProcedureModel[] procedures = new[]
        {
            new ProcedureModel()
        };


        [Header("Tile map coordinates")] [SerializeField]
        private CubeTileModel[] cubeTileModels;


        public int Id => id;
        public IEnumerable<CommandName> Commands => commands;
        public CubeTileModel[] CubeTileModels => cubeTileModels;
        public List<ProcedureModel> Procedures => procedures.ToList();
        public RobotDirection StartRobotDirection => startRobotDirection;
    }
}