using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Priority_Queue;
using System.Diagnostics;


namespace RobertWyzgolikProjekt
{
    public class AStar
    {
        public const int movementCost = 1;
        
        private static void printPuzzle(List<int> board)
        {
            foreach (int elemn in board)
            {
                Console.Write($"{elemn} ");
            }
            Console.WriteLine("");
        }

        // AStar class method contatining algorithm implementation based on a pseudecode privded in a raport.

        public static void runAStarSearch(List<int> board, List<int> finalBoard, int heuristic, int size)
        {
            
            Stopwatch sw = new Stopwatch();
            Logger.customLog("Rozpoczynam działanie algorytmu A* ...");
            int id = 1;
            List<int> possibleMoves = new List<int>();
            int zeroIndex;
            int jointG;
            var heuristicValues = new Dictionary<int, int>();
            List<List<int>> openSet = new List<List<int>>();
            List<List<int>> closedSet = new List<List<int>>();
            List<QueueObject> queue = new List<QueueObject>();
            Puzzle joint;
            Puzzle predecessor;
            int singleMovementCost = QueueObject.movementCost;
            queue.Add(new QueueObject(0, new Puzzle(board, id), 0, new Puzzle(new List<int>(), 0)));
            int heuristicValue = 0;
            int nodeGFuncValue = 0;
            int movementDecision = 0;
            IDictionary<int, List<int>> openSetDict = new Dictionary<int, List<int>>();
            IDictionary<int, Puzzle> closedSetDict = new Dictionary<int, Puzzle>();
            List<Puzzle> steps = new List<Puzzle>();
            while (queue.Count > 0)
            {
                sw.Start();
                queue = queue.OrderBy(x => x.totalCost).ToList();
                joint = new Puzzle(queue[0].joint.puzzle, queue[0].joint.id);
                predecessor = new Puzzle(queue[0].predecessor.puzzle, queue[0].predecessor.id);
                jointG = queue[0].nodeG;
                queue.RemoveAt(0);
                if (Movement.assessEqual(joint, finalBoard, size))
                {
                    sw.Stop();
                    steps.Add(joint);
                    while (predecessor.puzzle.Count != 0)
                    {
                        steps.Add(predecessor);
                        predecessor = new Puzzle(closedSetDict[predecessor.id].puzzle, closedSetDict[predecessor.id].id); 
                    }
                    steps.Reverse();
                    Logger.logSteps(steps, size);
                    UserInteraction.displayPath(steps, size);
                    Logger.customLog("Znaleziono docelowe rozwiązanie");
                    Logger.customLog($"Rozwiązanie znaleziono w {sw.ElapsedMilliseconds} ms");
                    Console.WriteLine("Znaleziono docelowe rozwiązanie");
                    Console.WriteLine($"Rozwiązanie znaleziono w { sw.ElapsedMilliseconds} ms \n");
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
                foreach (int move in possibleMoves)
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
                    openSetDict.Add(boardAfterMovement.id, new List<int>() { tempValueG, heuristicValue });
                    queue.Add(new QueueObject(tempValueG + heuristicValue, boardAfterMovement, tempValueG, joint));
                    
                }

            }
        }
    }
}
