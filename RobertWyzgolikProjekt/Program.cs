using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RobertWyzgolikProjekt
{
    // Main program loop which allows user to specify loggint path, problem, algorithm, puzzle's size, heuristic etc. and run it as many times as he wants 
    public class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            int problem = 0;
            
            while(run == true)
            {
                List<int> usersPuzzle = new List<int>(); // init input puzzle
                List<int> finalPuzzle = new List<int>(); // init final puzzle
                UserInteraction.welcomeMessage();
                UserInteraction.setLoggingPath(); // choose file to log into
                // clear log file only once program rerun
                if (problem == 0)
                {
                    Logger.clearLogFile();
                }
                var size = UserInteraction.sizeChoice(); // choose board size i.e. 3x3
                var heuristic = UserInteraction.heuristicChoice(); // choose heuristic 
                finalPuzzle = UserInteraction.modeChoice(size); // ask user whether he wants to get to zero first, zero last or snail state
                usersPuzzle = UserInteraction.generateChoice(size); // ask user whether he wants to generate random board, read it from a file or provide it using keyboard
                UserInteraction.displayCurrentState(HelperActions.splitData(new Puzzle(usersPuzzle, 999999), size)); // displays the very first state
                UserInteraction.runPreferredAlgorithm(usersPuzzle, finalPuzzle, heuristic, size); // runs preferred algorithm
                run = UserInteraction.doesUserWantToRunOnceMore(); // asks user whether he wants to run once more
                problem++;
            }

            UserInteraction.goodbyeMessage();
            Console.ReadKey();
        }
    }
}
