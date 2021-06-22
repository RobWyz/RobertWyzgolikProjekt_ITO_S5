using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertWyzgolikProjekt
{   
    // this class defines a structure of objects that belong to A* prioritized queue
    class QueueObject
    {
        public int totalCost;
        public Puzzle joint;
        public Puzzle predecessor;
        public const int movementCost = 1;
        public int nodeG;
        
        public QueueObject(int cost, Puzzle puzzle, int nodeGcost, Puzzle parent)
        {
            totalCost = cost + movementCost;
            joint = new Puzzle(puzzle.puzzle, puzzle.id);
            nodeG = nodeGcost;
            predecessor = new Puzzle(parent.puzzle, parent.id);
        }

    }
}
