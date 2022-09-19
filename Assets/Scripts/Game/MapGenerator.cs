using System.Collections.Generic;
using Map;
using Patterns;
using UnityEngine;

namespace Game
{
    public class MapGenerator : MonoBehaviourSingleton<MapGenerator>
    {
        private const int MAX_HEALTH = 5;
        private const float X_SIZE = 0.67f;
        private const float Y_SIZE = 0.27f;
    
        [SerializeField] private int yCells;
        [SerializeField] private int xCells;
    
        private List<Brick> bricks = new List<Brick>();

        private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < xCells; i++)
            {
                for (int j = 0; j < yCells; j++)
                {
                    Gizmos.DrawWireCube(GetPosition(i, j), new Vector3(X_SIZE, Y_SIZE, 1));
                }
            }
        }

        public void GenerateMap(int levelNumber)
        {
            Unity.Mathematics.Random r = new Unity.Mathematics.Random((uint)levelNumber);
        
            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].DestroyBlock();
            }
            bricks.Clear();
            for (int i = 0; i < xCells; i++)
            {
                for (int j = 0; j < yCells; j++)
                {
                    Brick block = Pool<Brick>.Instance.GetObject();
                    if(block==null)
                        return;
                    block.transform.position = GetPosition(i, j);
                    block.SetHealth(r.NextInt(0, MAX_HEALTH));
                    bricks.Add(block);
                }
            }
        }

        private Vector3 GetPosition(int x, int y)
        {
            return transform.position - new Vector3(x*X_SIZE,y*Y_SIZE,0) - new Vector3(X_SIZE, Y_SIZE, 0)*0.5f;
        }
    }
}
