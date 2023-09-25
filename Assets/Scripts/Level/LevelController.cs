using System;
using Cube;
using UnityEngine;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelData level;
        [SerializeField] private GameObject tileCubePrefab;

        private void Start()
        {
            LoadLevel();
        }


        public void LoadLevel()
        {
            print("loading level " + level.LevelName);

            foreach (var levelCubeTile in level.CubeTiles)
            {
                LoadCubeTile(levelCubeTile);
            }
        }

        private void LoadCubeTile(CubeTileData levelCubeTile)
        {
            var instantiatedObject = Instantiate(tileCubePrefab,
                CalcTilePosAccordingToYScale(levelCubeTile.Position, levelCubeTile.Height), Quaternion.identity);
            instantiatedObject.transform.localScale = new Vector3(1, levelCubeTile.Height, 1);

            if (levelCubeTile.IsLightTile)
            {
                ConfigureLightCubeTile(instantiatedObject);
            }


            // if increase height (y scale) we need move cube in y axis
            Vector3 CalcTilePosAccordingToYScale(Vector3 pos, int height) => new Vector3(pos.x, height - 1, pos.z);
        }

        private void ConfigureLightCubeTile(GameObject instantiatedObject)
        {
            instantiatedObject.tag = "LightCube";

            if (instantiatedObject.TryGetComponent(out CubeTile cube))
            {
                cube.ChangeTileStatus(CubeType.TurnedOffTile);
            }
            else
            {
                Debug.LogError("failed to get CubeTile component from cube!");
            }
        }
    }
}