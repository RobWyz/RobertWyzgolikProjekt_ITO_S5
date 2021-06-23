using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertWyzgolikProjekt
{
    // class containing Manhattan heuristic counter method that is in this case used in order to solve N puzzle problem
    public class ManhattanHeuristics
    {
        // class method which allows to count manhattan heuristic while given a problem
        public static int countManhattanHeuristic(Puzzle board, List<int> finalBoard, int size)
        {
            int heuristic = 0;
            int value;
            List<int> currentCords = new List<int>();
            List<int> destinationCords = new List<int>();
            int xErr;
            int yErr;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if ((board.puzzle[i * size + j] != 0) && (board.puzzle[i * size + j] != finalBoard[i * size + j]))
                    {
                        value = board.puzzle[i * size + j];
                        currentCords = HelperActions.calcCordsFromIndex(board.puzzle.IndexOf(value), size);
                        destinationCords = HelperActions.calcCordsFromIndex(finalBoard.IndexOf(value), size);
                        xErr = currentCords[0] - destinationCords[0];
                        yErr = destinationCords[1] - currentCords[1];
                        heuristic += (Math.Abs(xErr) + Math.Abs(yErr));
                    }
                    
                }
            }
            return heuristic;
        }
        
    }
}
