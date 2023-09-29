using System.Collections.Generic;
using System.Linq;
using Model.Commands;
using Model.Robot;
using UnityEngine;

namespace Model.Level
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Levels/Create New Level", order = 0)]
    public class LevelModel : ScriptableObject
    {
        [Header("Level settings")] [SerializeField]
        private int id;

        [SerializeField] private RobotDirection startRobotDirection;

        [Header("Commands Settings")] [SerializeField]
        private CommandNames[] commands;

        [SerializeField] private ProcedureModel[] procedures = new[]
        {
            new ProcedureModel()
        };


        [Header("Tile map coordinates")] [SerializeField]
        private CubeTileModel[] cubeTileModels;


        public int Id => id;
        public CubeTileModel[] CubeTileModels => cubeTileModels;
        public IEnumerable<CommandNames> Commands => commands;
        public List<ProcedureModel> Procedures => procedures.ToList();
        public RobotDirection StartRobotDirection => startRobotDirection;
    }
}