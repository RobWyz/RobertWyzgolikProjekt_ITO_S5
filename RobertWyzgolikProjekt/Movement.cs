using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertWyzgolikProjekt
{
    // class which contatins methods that allow algorithms to perfrom different movements which is basically changing different tile's location
    public class Movement
    {
        public static void makeMovement(List<int> board, int movement, int size, int index)
        {
            int temp = 0;
            switch (movement)
            {
                case 4:
                    temp = board[index - 1];
                    board[index - 1] = board[index];
                    board[index] = temp;
                    break;
                case 6:
                    temp = board[index + 1];
                    board[index + 1] = board[index];
                    board[index] = temp;
                    break;
                case 8:
                    temp = board[index - size];
                    board[index - size] = board[index];
                    board[index] = temp;
                    break;
                case 2:
                    temp = board[index + size];
                    board[index + size] = board[index];
                    board[index] = temp;
                    break;
                default:
                    break;
            }
        }
        // return bool true value when boards are the same (have the same tile order)
        public static bool assessEqual(Puzzle board, List<int> finalBoard, int size)
        {
            for (int i = 0; i < size*size; i++)
            {
                if(board.puzzle[i] != finalBoard[i])
                {
                    return false;
                }
            }
            return true;
        }
        // updates 0 index after a specified movement
        public static int updateIndex(int index, int size, int movement)
        {
            switch (movement)
            {
                case 6: // once moving right zero's index increments by 1
                    index++;
                    return index;
                case 4: // once moving left zero's index decrements by 1
                    index--; 
                    return index;
                case 8: // once moving up zero's index decrements by board size
                    index -= size;
                    return index;
                case 2: // once moving down zero's index increments by board size
                    index += size;
                    return index;
                default:
                    return index;
            }
        }
        // method which returns a board after a specified movement
        public static List<int> returnBoardAfterMovement(List<int> board, int movement, int size, int index)
        {
            List<int> puzzle = new List<int>(board);
            int temp = 0;
            switch (movement)
            {
                case 4:
                    temp = puzzle[index - 1];
                    puzzle[index - 1] = puzzle[index];
                    puzzle[index] = temp;
                    return puzzle;
                case 6:
                    temp = puzzle[index + 1];
                    puzzle[index + 1] = puzzle[index];
                    puzzle[index] = temp;
                    return puzzle;
                case 8:
                    temp = puzzle[index - size];
                    puzzle[index - size] = puzzle[index];
                    puzzle[index] = temp;
                    return puzzle;
                case 2:
                    temp = puzzle[index + size];
                    puzzle[index + size] = puzzle[index];
                    puzzle[index] = temp;
                    return puzzle;
                default:
                    return puzzle;
            }
        }
    }
}
