using UnityEngine;
using UnityEngine.Serialization;

namespace Model.Level
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Levels/Create New Level", order = 0)]
    public class LevelModel : ScriptableObject
    {
        [SerializeField] private string levelName;
        [SerializeField] private CubeTileModel[] cubeTileModels;

        public string LevelName => levelName;
        public CubeTileModel[] CubeTileModels => cubeTileModels;
    }
}