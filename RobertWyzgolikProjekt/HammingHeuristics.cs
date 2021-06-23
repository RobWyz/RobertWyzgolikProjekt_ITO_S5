using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertWyzgolikProjekt
{   
    // class containing Hamming heuristic counter method that is in this case used in order to solve N puzzle problem
    public class HammingHeuristics
    {
        // class method which allows to count hamming heuristic while given a problem
        public static int countHammingHeuristic(Puzzle board, List<int> finalBoard, int size)
        {
            int hammingHeuristicCount = 0;
            int properRow;
            int properCol;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if(board.puzzle[i*size + j] != 0 && board.puzzle[i*size + j] != finalBoard[i*size + j])
                    {
                        hammingHeuristicCount++;
                    }
                }
            }
            return hammingHeuristicCount;
        }
    }
}
