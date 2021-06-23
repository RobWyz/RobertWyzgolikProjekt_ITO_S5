using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertWyzgolikProjekt
{
    // General purpose class that contains method that perform various actions that are not alligned to any other module but are necessary to solve N puzzle problem
    public class HelperActions
    {

        private static List<int> Indexes = new List<int>();
        // allows to convert a matrix (list of lists) to a single list
        public static List<int> matrixToList(List<List<int>> matrix)
        {
            List<int> board = new List<int>();
            for (int i = 0; i < matrix[0].Count; i++)
            {
                for (int j = 0; j < matrix[0].Count; j++)
                {
                    board.Add(matrix[i][j]);
                }
            }
            return board;
        }
        // allows to find a row number of an empty file in a matrix
        public static int findXPos(List<List<int>> matrix, int size)
        {
            int row = 0;
            int N = size;
            for (int i = N - 1; i >= 0; i--)
            {
                for (int j = N - 1; j >= 0; j--)
                {
                    if (matrix[i][j] == 0)
                    {
                        row = N - i;
                    }
                }
            }
            return row;
        }
        // allows to convert a single list to a matrix (list of lists)
        public static List<List<int>> splitData(Puzzle data, int width)
        {
            List<List<int>> board = new List<List<int>>();
            for (int i = 0; i < width; i++)
            {
                List<int> newdd = new List<int>();
                newdd = data.puzzle.Skip(i * width).Take(width).ToList();
                board.Add(newdd);
            }
            return board;
        }
        // allows to shuffle items order in a given list
        public static List<int> shuffleBoard(List<int> board, int size)
        {
            Random rnd = new Random();
            var count = board.Count;
            var last = count - 1;
            for (var j = 0; j < last; ++j)
            {
                var r = rnd.Next(j, count);
                var tmp = board[j];
                board[j] = board[r];
                board[r] = tmp;
            }
            List<int> puzzle = new List<int>(board);
            return puzzle;
        }
        // allows to find indexes of an empty tile in a matrix
        public static List<int> find_zero_index(List<List<int>> board, int size)
        {

            List<int> indexes = new List<int>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i][j] == 0)
                    {
                        HelperActions.Indexes.Add(i);
                        HelperActions.Indexes.Add(j);
                    }
                }
            }

            return indexes;
        }
        // allows to run preferred heursitic based on users choice
        public static int splitHeuristics(Puzzle currentBoard, List<int> finalBoard, int number, int size)
        {
            int heuristicValue = 0;
            if (number == 1)
            {
                return ManhattanHeuristics.countManhattanHeuristic(currentBoard, finalBoard, size);
            }
            else
            {
                return HammingHeuristics.countHammingHeuristic(currentBoard, finalBoard, size);
            }

        }
        // allows to find possible moves in a given puzzle state
        public static List<int> returnMoves(int index, int size)
        {
            List<int> cords = new List<int>();
            cords = HelperActions.calcCordsFromIndex(index, size);
            List<int> moves = new List<int>();
            if (cords[1] > 0)
            {
                moves.Add(4);
            }
            if (cords[1] < size - 1)
            {
                moves.Add(6);
            }
            if (cords[0] > 0)
            {
                moves.Add(8);
            }
            if (cords[0] < size - 1)
            {
                moves.Add(2);
            }

            return moves;
        }
        // allows to calculate cords from a given index and puzzle size
        public static List<int> calcCordsFromIndex(int index, int size)
        {
            List<int> cords = new List<int>();
            cords.Add(index / size);
            cords.Add(index % size);
            return cords;
        }
        // allows to calculate index from given cords and puzzle size
        public static int calcIndexFromCords(List<int> cords, int size)
        {
            int index;
            index = cords[0] * size + cords[1];
            return index;
        }

        // allows to print a list
        public static void printBoard(List<int> board)
        {
            foreach (var item in board)
            {
                Console.Write($"{item} ");

            }
            Console.WriteLine(" ");
        }
        // A* purpose method which allows to specify whether the requested state has already been visited by an agent
        public static bool wasNodeVisited(IDictionary<int, Puzzle> visited, List<int> currentState, int size)
        {
            foreach (KeyValuePair<int, Puzzle> item in visited)
            {
                if (item.Key != 0 && item.Key != 1)
                {
                    if (Movement.assessEqual(item.Value, currentState, size))
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        // RTA* purpose method which allows to specify whether requesed state is already known
        public static bool isStateKnown(IDictionary<Puzzle, int> knownStates, List<int> currentState, int size)
        {
            foreach (KeyValuePair<Puzzle, int> item in knownStates)
            {
                if (Movement.assessEqual(item.Key, currentState, size))
                {
                    return true;
                }
            }
            return false;
        }
        // allows to get a specific puzzle state by providing specific Puzzle's object id
        public static List<int> getStateById(IDictionary<Puzzle, int> knownStates, int id)
        {

            foreach (KeyValuePair<Puzzle, int> item in knownStates)
            {
                if (item.Key.id == id)
                {
                    return new List<int>(item.Key.puzzle);
                }
            }

            return new List<int>();   
        }
        // allows to get a specific puzzle's state id by providing a specific Puzzle's puzzle board
        public static int getIdByState(IDictionary<Puzzle, int> knownStates, List<int> state, int size)
        {
            foreach (KeyValuePair<Puzzle, int> item in knownStates)
            {
                if (Movement.assessEqual(item.Key, state, size))
                {
                    return item.Key.id;
                }
            }

            return 0; 
        }
        // allows to update heuristic of a specific node
        public static void updateHeuristicById(IDictionary<Puzzle, int> knownStates, int id, int heuristic)
        {
            foreach (KeyValuePair<Puzzle, int> item in knownStates)
            {
                if (item.Key.id == id)
                {
                    knownStates[item.Key] = heuristic;
                }
            }
        }
    }

}
