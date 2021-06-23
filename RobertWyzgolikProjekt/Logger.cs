using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RobertWyzgolikProjekt
{
    // class which contains implementation that makes it enable for software to log all of the run information to a specified file
    public class Logger
    {
        public static string logPath { get; set; }
        
        public static void clearLogFile()
        {
            File.WriteAllText(Logger.logPath, string.Empty);
        }
        // responsible for logging present board state to a ile
        public static void logCurrentState(List<List<int>> board)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < board[0].Count; i++)
            {
                sb.Append($"[{String.Join(",", board[i])}] \n");
            }
            File.AppendAllText(@Logger.logPath, sb.ToString() + Environment.NewLine);
        }

        // responsible for logging the whole path by loging steps together with an information about an iteration number
        public static void logSteps(List<Puzzle> steps, int size)
        {
            StringBuilder sb = new StringBuilder();
            int iter = 0;
            foreach (var step in steps)
            {
                sb.Append($"Iteracja {iter}... \n");
                List<List<int>> board = new List<List<int>>(HelperActions.splitData(step, size));
                for (int i = 0; i < size; i++)
                {
                    sb.Append($"[{String.Join(",", board[i])}] \n");
                }
                sb.Append("\n");
                iter++;
            }
            File.AppendAllText(@Logger.logPath, sb.ToString() + Environment.NewLine);
        }
        // allows to log preferred string to a file
        public static void customLog(string log)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(log);
            File.AppendAllText(@Logger.logPath, sb.ToString() + Environment.NewLine);
        }

    }
}
