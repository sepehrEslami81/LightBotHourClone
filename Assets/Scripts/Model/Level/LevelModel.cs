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
        private CommandName[] commands;

        [SerializeField] private ProcedureModel[] procedures = new[]
        {
            new ProcedureModel()
        };


        [Header("Tile map coordinates")] [SerializeField]
        private CubeTileModel[] cubeTileModels;


        public int Id => id;
        public CubeTileModel[] CubeTileModels => cubeTileModels;
    }
}