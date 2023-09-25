using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Levels/Create New Level", order = 0)]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private string levelName;
        [SerializeField] private CubeTileData[] cubeTiles;

        public string LevelName => levelName;
        public CubeTileData[] CubeTiles => cubeTiles;
    }
}