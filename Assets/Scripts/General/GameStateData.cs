using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace General
{
    public struct BallData
    {
        public Vector2 Position;
        public Vector2 Velocity;
    }

    public struct BrickData
    {
        public Vector2 Position;
        public int Health;
    }
    public class GameStateData
    {
        public int Level;
        public int Points;
        public int Life;
        public int Bullets;
        public int PlatformSize;
        public bool BallOnThePlatform;
        public List<BrickData> BrickData;
        public List<BallData> BallsData;
    }
}
