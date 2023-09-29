using Model.Robot;
using UnityEngine;

namespace Model.Level
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Levels/Create New Level", order = 0)]
    public class LevelModel : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private RobotDirection startRobotDirection;
        [SerializeField] private CubeTileModel[] cubeTileModels;


        public int Id => id;
        public CubeTileModel[] CubeTileModels => cubeTileModels;
    }
}