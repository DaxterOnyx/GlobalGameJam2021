using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerTrace
    {
        public int Index = -1;

        //TODO ADD TIME OF RECORD, NOT SAVE DUPLICATE VALUE
        public List<Vector2> Positions = new List<Vector2>();
        public List<Vector2> Directions = new List<Vector2>();

        public void Add(Vector2 pos, Vector2 dir)
        {
            Positions.Add(pos);
            Directions.Add(dir);
        }

        public Vector2 GetPos(int frameCounter)
        {
            var i = frameCounter % Positions.Count;
            var scope = frameCounter / Positions.Count;
            if (scope % 2 == 1)
                i = Positions.Count - 1 - i;
//            Debug.Log(frameCounter + "->" + i + "/" + Positions.Count);
            return Positions[i];
        }

        public Vector2 GetDir(int frameCounter)
        {
            var i = frameCounter % Directions.Count;
            var scope = frameCounter / Directions.Count;
            if (scope % 2 == 1)
                i = Directions.Count - 1 - i;
            return Directions[i];
        }
    }
}