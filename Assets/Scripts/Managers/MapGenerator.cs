using UnityEngine;                                                                                                                                      
 using UnityEngine.Tilemaps;                                                                                                                             
                                                                                                                                                         
 namespace Managers                                                                                                                                      
 {                                                                                                                                                       
     public class MapGenerator : MonoBehaviour                                                                                                           
     {                                                                                                                                                   
         public Tilemap map;                                                                                                                             
         public TileBase[] groundTiles;                                                                                                                  
         public TileBase[] wallTiles;                                                                                                                    
         public TileBase[] decorationTiles;                                                                                                              
         private int mapSize = 20;                                                                                                                       
                                                                                                                                                         
         void Start()                                                                                                                                    
         {                                                                                                                                               
             GenerateMap();                                                                                                                              
         }                                                                                                                                               
                                                                                                                                                         
         void GenerateMap()                                                                                                                              
         {                                                                                                                                               
             // Crear un borde para el mapa                                                                                                              
             for (int x = -mapSize; x <= mapSize; x++)                                                                                                   
             {                                                                                                                                           
                 for (int y = -mapSize; y <= mapSize; y++)                                                                                               
                 {                                                                                                                                       
                     Vector3Int position = new Vector3Int(x, y, 0);                                                                                      
                                                                                                                                                         
                     // Usar ruido para generar un terreno más natural                                                                                   
                     if (Mathf.PerlinNoise(x / 4f, y / 4f) > 0.5f)                                                                                       
                     {                                                                                                                                   
                         map.SetTile(position, groundTiles[Random.Range(0, groundTiles.Length)]);                                                        
                     }                                                                                                                                   
                     if (IsBorder(position))                                                                                                             
                     {                                                                                                                                   
                         map.SetTile(position, wallTiles[0]);                                                                                            
                     }                                                                                                                                   
                 }                                                                                                                                       
             }                                                                                                                                           
                                                                                                                                                         
             // Añadir decoración aleatoria                                                                                                              
             for (int i = 0; i < 20; i++)                                                                                                                
             {                                                                                                                                           
                 Vector3Int randomPosition = new Vector3Int(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize), 0);                        
                 if (IsGround(randomPosition))                                                                                                           
                 {                                                                                                                                       
                     map.SetTile(randomPosition, decorationTiles[Random.Range(0, decorationTiles.Length)]);                                              
                 }                                                                                                                                       
             }                                                                                                                                           
         }                                                                                                                                               
                                                                                                                                                         
         bool IsBorder(Vector3Int position)                                                                                                              
         {                                                                                                                                               
             return position.x == -mapSize || position.x == mapSize || position.y == -mapSize || position.y == mapSize;                                  
         }                                                                                                                                               
                                                                                                                                                         
         bool IsGround(Vector3Int position)                                                                                                              
         {                                                                                                                                               
             return map.GetTile(position) != null && groundTiles.Contains(map.GetTile(position));                                                        
         }                                                                                                                                               
     }                                                                                                                                                   
 }      