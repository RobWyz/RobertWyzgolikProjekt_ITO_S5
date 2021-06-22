using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RobertWyzgolikProjekt
{   
    public class RtaStar
    {
        // Real Time A* algorithm implementation based on a pseudocode provided in a raport
        public static void runRtaStarSearch(List<int> startingPuzzle, List<int> finalPuzzle, int heuristic, int size)
        {
            Stopwatch sw = new Stopwatch(); // watch class object to measure performance
            Logger.customLog("Rozpoczynam działanie algorytmu RTA*...");
            List<Puzzle> steps = new List<Puzzle>(); // list that will contain the whole puzzle problem solving path
            List<int> moves = new List<int>();
            int heuristicVal;
            int id = 0;
            List<Puzzle> successors = new List<Puzzle>(); // stores nodes that can be visited from current node
            IDictionary<Puzzle, int> heurisitcValues = new Dictionary<Puzzle, int>(); // Dictionary that maps current calculated heuristic value to a node
            IDictionary<int, int> heuristicValuesById = new Dictionary<int, int>(); // Dictionary that maps current calculated heuristic value to node's id
            Puzzle puzzle = new Puzzle(startingPuzzle, id);
            while (true)
            {
                sw.Start();
                if (Movement.assessEqual(puzzle, finalPuzzle, size)){ // assess whether it's a final state or just not yet
                    sw.Stop();
                    // Display and log the whole information to the user
                    Logger.logSteps(steps, size);
                    UserInteraction.displayPath(steps, size);
                    Logger.customLog("Znaleziono docelowe rozwiązanie");
                    Logger.customLog($"Rozwiązanie znaleziono w {sw.ElapsedMilliseconds} ms");
                    Console.WriteLine("Znaleziono docelowe rozwiązanie");
                    Console.WriteLine($"Rozwiązanie znaleziono w { sw.ElapsedMilliseconds} ms \n");
                    return;
                }
                if(!HelperActions.isStateKnown(heurisitcValues, puzzle.puzzle, size))
                {
                    
                    puzzle = new Puzzle(puzzle.puzzle, puzzle.id);
                    heuristicVal = HelperActions.splitHeuristics(puzzle, finalPuzzle, heuristic, size);
                    heurisitcValues.Add(puzzle, heuristicVal);
                    heuristicValuesById.Add(puzzle.id, heuristicVal);
                    
                }
                moves = HelperActions.returnMoves(puzzle.puzzle.IndexOf(0), size);
                foreach (var move in moves) // dig into successors 
                {
                    Puzzle successor = new Puzzle(Movement.returnBoardAfterMovement(puzzle.puzzle, move, size, puzzle.puzzle.IndexOf(0)), id);
                    if (!HelperActions.isStateKnown(heurisitcValues, successor.puzzle, size))
                    {
                        id++;
                        successor.id = id;
                    }
                    else
                    {
                        successor.id = HelperActions.getIdByState(heurisitcValues, successor.puzzle, size);
                    }
                    
                    successors.Add(successor); 
                }
                int mMin = int.MaxValue;
                int moveMin = 6; // default value
                int moveMinId = puzzle.id; // default value
                for (int i = 0; i < successors.Count; i++)
                {
                    if(!HelperActions.isStateKnown(heurisitcValues, successors[i].puzzle, size)) {
                        heuristicVal = HelperActions.splitHeuristics(successors[i], finalPuzzle, heuristic, size);
                        heurisitcValues.Add(successors[i], heuristicVal);
                        heuristicValuesById.Add(successors[i].id, heuristicVal);
                    }
                    if(heuristicValuesById[successors[i].id] < mMin)
                    {
                        mMin = heuristicValuesById[successors[i].id];
                        moveMin = moves[i];
                        moveMinId = successors[i].id;
                    }
                }
                HelperActions.updateHeuristicById(heurisitcValues, puzzle.id, 1 + mMin); // upodate heuristic with new values
                heuristicValuesById[puzzle.id] = 1 + mMin;
                puzzle = new Puzzle(Movement.returnBoardAfterMovement(puzzle.puzzle, moveMin, size, puzzle.puzzle.IndexOf(0)), moveMinId); // create node object
                steps.Add(puzzle); // add choosen node to the list of steps
                successors.Clear(); // clear all of the successors available in this iteration
            }
        }
    }
}
