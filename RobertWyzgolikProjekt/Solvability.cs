using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertWyzgolikProjekt
{
    public class Solvability
    {
        // method which returns a number of inversion in a given puzzle state
        public static int countInversions(List<int> puzzle, List<int> finalBoard, int size)
        {
            int inv = 0;
            for (int i = 0; i < (size * size - 1); i++)
            {
                for (int j = i + 1; j < size * size; j++)
                {
                    int tileI = puzzle[i];
                    int tileJ = puzzle[j];
                    if (finalBoard.IndexOf(tileI) > finalBoard.IndexOf(tileJ))
                    {
                        inv++;
                    }
                }
            }
            return inv;
        }

        // methods which assesses whether specified puzzle problem is solvable
        public static bool assessSolvability(List<int> puzzle, List<int> finalBoard, int size)
        {
            int inv = countInversions(puzzle, finalBoard, size);
            int puzzleZeroRow = puzzle.IndexOf(0) / size;
            int puzzleZeroCol = puzzle.IndexOf(0) % size;
            int finalBoardZeroRow = puzzle.IndexOf(0) / size;
            int finalBoardZeroCol = puzzle.IndexOf(0) % size;
            int counter = (Math.Abs(puzzleZeroRow - finalBoardZeroRow) + Math.Abs(puzzleZeroCol - finalBoardZeroCol));
            if (counter % 2 == 0 && inv % 2 == 0)
            {
                return true;
            }
            if (counter % 2 == 1 && inv % 2 == 1)
            {
                return true;
            }
            return false;
        }
    }
}
