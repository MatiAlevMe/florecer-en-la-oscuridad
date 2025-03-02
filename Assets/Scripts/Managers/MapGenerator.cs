using UnityEngine;
using UnityEngine.Tilemaps;

namespace Managers
{
    public class MapGenerator : MonoBehaviour
    {
        public Tilemap map;
        public TileBase[] groundTiles;

        void Start()
        {
            GenerateMap();
        }

        void GenerateMap()
        {
            // Implementación básica de un mapa 2D
            for (int x = -10; x < 10; x++)
            {
                for (int y = -10; y < 10; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    map.SetTile(position, groundTiles[0]);
                }
            }
        }
    }
}
