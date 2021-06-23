using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Priority_Queue;
using System.Diagnostics;


namespace RobertWyzgolikProjekt
{
    // class which contains A* algorithm implementation that in this case will be used to solve N problem puzzles.  
    public class AStar
    {
        public const int movementCost = 1;
 
        // AStar class method contatining algorithm implementation based on a pseudecode privded in a raport.
        // It's design is based on an explanation of the A* algorithm from ITO subject lectures. 

        public static void runAStarSearch(List<int> board, List<int> finalBoard, int heuristic, int size)
        {
            
            Stopwatch sw = new Stopwatch();
            Logger.customLog("Rozpoczynam działanie algorytmu A* ...");
            Console.WriteLine("Rozpoczynam działanie algorytmu A* ...");
            int id = 1;
            List<int> possibleMoves = new List<int>();
            int zeroIndex;
            int jointG;
            var heuristicValues = new Dictionary<int, int>(); // dictionary that stores node's heuristic values
            List<QueueObject> queue = new List<QueueObject>(); // a prioritized queue that store current node, it's heuristic value and it's parent node
            Puzzle joint; // Puzzle object that represent current board state
            Puzzle predecessor; // puzzle object that represents 
            int singleMovementCost = QueueObject.movementCost;
            queue.Add(new QueueObject(0, new Puzzle(board, id), 0, new Puzzle(new List<int>(), 0))); // adds init state to the queue
            int heuristicValue = 0;
            int nodeGFuncValue = 0;
            int movementDecision = 0;
            IDictionary<int, List<int>> openSetDict = new Dictionary<int, List<int>>(); // Open set
            IDictionary<int, Puzzle> closedSetDict = new Dictionary<int, Puzzle>(); // Closed set
            List<Puzzle> steps = new List<Puzzle>(); // List of puzzle's states that allows to retrive the whole path
            while (queue.Count > 0)
            {
                sw.Start();
                queue = queue.OrderBy(x => x.totalCost).ToList(); // sort queue so that it becomes like prioritized one
                // assign specific values from the very first item from the prioritized queue
                joint = new Puzzle(queue[0].joint.puzzle, queue[0].joint.id);
                predecessor = new Puzzle(queue[0].predecessor.puzzle, queue[0].predecessor.id);
                jointG = queue[0].nodeG;
                queue.RemoveAt(0); // pop the first item out of the prioritzed queue
                if (Movement.assessEqual(joint, finalBoard, size)) // check whether we're already in a final state
                {
                    sw.Stop();
                    steps.Add(joint);
                    while (predecessor.puzzle.Count != 0)
                    {
                        steps.Add(predecessor);
                        predecessor = new Puzzle(closedSetDict[predecessor.id].puzzle, closedSetDict[predecessor.id].id); 
                    }
                    steps.Reverse();
                    // Display and log the whole information to the user
                    Logger.logSteps(steps, size);
                    UserInteraction.displayPath(steps, size);
                    Logger.customLog("Znaleziono docelowe rozwiązanie");
                    Logger.customLog($"Rozwiązanie znaleziono w {sw.ElapsedMilliseconds} ms");
                    Console.WriteLine("Znaleziono docelowe rozwiązanie");
                    Console.WriteLine($"Rozwiązanie znaleziono w { sw.ElapsedMilliseconds} ms \n");
                    Console.WriteLine("------------------------------------");
                    return;
                }
                if (closedSetDict.ContainsKey(joint.id))
                {
                    continue;
                }
                closedSetDict.Add(joint.id, predecessor);
                int tempValueG = nodeGFuncValue + QueueObject.movementCost;
                zeroIndex = joint.puzzle.IndexOf(0);
                possibleMoves = HelperActions.returnMoves(zeroIndex, size);
                foreach (int move in possibleMoves) // check all of the possible successors and count their heuristics
                {
                    List<int> suspectedBoard = new List<int>(Movement.returnBoardAfterMovement(joint.puzzle, move, size, zeroIndex));
                    if(!HelperActions.wasNodeVisited(closedSetDict, suspectedBoard, size))
                    {
                        id++;
                    }
                    Puzzle boardAfterMovement = new Puzzle(suspectedBoard, id);
                    if (closedSetDict.ContainsKey(boardAfterMovement.id))
                    {
                        continue;
                    }
                    if (openSetDict.ContainsKey(boardAfterMovement.id))
                    {
                        int moveG = openSetDict[boardAfterMovement.id][0];
                        int moveH = openSetDict[boardAfterMovement.id][1];
                        if (moveG <= tempValueG)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        heuristicValue = HelperActions.splitHeuristics(boardAfterMovement, finalBoard, heuristic, size);
                    }
                    openSetDict.Add(boardAfterMovement.id, new List<int>() { tempValueG, heuristicValue }); // add item to a open Set
                    queue.Add(new QueueObject(tempValueG + heuristicValue, boardAfterMovement, tempValueG, joint)); // add item to a queue
                    
                }

            }
        }
    }
}
