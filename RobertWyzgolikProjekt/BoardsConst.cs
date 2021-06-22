using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertWyzgolikProjekt
{
    public class BoardsConst
    {
        // method which is responsible for generating a random order board in a given size
        public static List<int> createRandomBoard(int size)
        {
            List<int> puzzle = new List<int>();
            for (int i = 0; i < size*size; i++)
            {
                puzzle.Add(i);
            }
            puzzle = HelperActions.shuffleBoard(puzzle, size);
            return puzzle;
        }
        // method which is responsible for generating a board of a specified size in a zero first order
        public static List<int> createZeroFirstBoard(int size)
        {
            List<int> board = new List<int>();
            for (int i = 0; i < size * size; i++)
            {
                board.Add(i);
            }
            return board;
        }
        // method which is responsible for generating a board of a specified size in a zero last order
        public static List<int> createZeroLastBoard(int size)
        {
            List<int> board = new List<int>();
            for (int i = (size*size -1); i >= 0; --i)
            {
                board.Add(i);
            }
            return board;
        }
    }
    
}
