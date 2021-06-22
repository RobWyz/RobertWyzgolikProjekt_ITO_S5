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
        // method which is responsible for generating a board of a specified size in a snail order
        public static List<int> createSnailPuzzle(int size)
        {
            List<int> snailPuzzle = new List<int>();
            List<List<int>> board = new List<List<int>>();
            List<int> innerRow = new List<int>();
            for (int loopCol = 0; loopCol < size; loopCol++)
            {
                innerRow.Add(0);
            }
            for (int loopRow = 0; loopRow < size; loopRow++)
            {
                board.Add(new List<int>(innerRow));
            }
            // INIT all of the movements
            List<List<int>> moves = new List<List<int>>();
            moves.Add(new List<int>() { 0, 1 });
            moves.Add(new List<int>() { 1, 0 });
            moves.Add(new List<int>() { 0, -1 });
            moves.Add(new List<int>() { -1, 0 });
            int col = 0;
            int row = 0;
            int i = 1;
            int final = size * size;
            size--;
            while(i != final && size > 0)
            {
                for (int move = 0; move < moves.Count; move++)
                {
                    if(i == final)
                    {
                        break;
                    }
                    for (int iter = 0; iter < size; iter++)
                    {
                        board[row][col] = i;
                        row += moves[move][0];
                        col += moves[move][1];
                        i++;
                        if(i == final)
                        {
                            break;
                        }
                    }
                }
                row++;
                col++;
                size -= 2;
            }
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    snailPuzzle.Add(board[x][y]);
                }
            }
            return snailPuzzle;
        }
    }
    
}
