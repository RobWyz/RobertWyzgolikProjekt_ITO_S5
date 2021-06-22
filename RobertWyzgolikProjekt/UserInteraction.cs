using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RobertWyzgolikProjekt
{
    public class UserInteraction
    {
        // method which displays a welcome message
        public static void welcomeMessage()
        {
            Console.WriteLine("Witam w programie służącym do rozwiązania problemu N puzzli.");
            Console.WriteLine("Wykorzystany zostanie algorytm A* lub RTA*");
        }
        // this method allows user to choose his desired board size
        public static int sizeChoice()
        {
            int size = 0;
            while (size > 15 || size < 3)
            {
                Console.WriteLine("Proszę wprowadzić rozmiar planszy z zakresu <3,15>");
                Console.WriteLine("Będzie to plansza rozmiaru NxN gdzie N to wprowadzona wartość");
                size = Convert.ToInt32(Console.ReadLine());
            }
            StringBuilder sb = new StringBuilder();
            sb.Append($"Wybrany rozmiar planszy: {size}x{size}");
            File.AppendAllText(@Logger.logPath, sb.ToString() + Environment.NewLine);
            return size;
        }
        // this method allows user to choose his desired heuristic
        public static int heuristicChoice()
        {
            int choice = 0;
            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("Proszę wybrać heurystykę.");
                Console.WriteLine("1 - Heurystyka Manhattan \n2 - Heurystyka Hamming");
                choice = Convert.ToInt32(Console.ReadLine());
            }
            StringBuilder sb = new StringBuilder();
            switch (choice)
            {
                case 1:
                    sb.Append("Wybrano heurystykę Manhattan");
                    break;
                case 2:
                    sb.Append("Wybrano heurystykę Hamminga");
                    break;
            }
            
            File.AppendAllText(@Logger.logPath, sb.ToString() + Environment.NewLine);
            return choice;
        }
        // this method allows user to choose his desired final board state
        public static List<int> modeChoice(int size)
        {
            List<int> puzzle = new List<int>();
            int choice = 0;
            while(choice != 1 && choice != 2 && choice != 3){
                Console.WriteLine("Proszę wybrać pożądany tryb");
                Console.WriteLine("1 - zero first\n2 - zero last\n3 - snail");
                choice = Convert.ToInt32(Console.ReadLine());
            }
            Logger.customLog("Docelowy stan puzzli:");
            switch (choice)
            {
                case 1:
                    puzzle = BoardsConst.createZeroFirstBoard(size);
                    Logger.logCurrentState(HelperActions.splitData(new Puzzle(puzzle, 77777), size));
                    return puzzle;
                case 2:
                    puzzle = BoardsConst.createZeroLastBoard(size);
                    Logger.logCurrentState(HelperActions.splitData(new Puzzle(puzzle, 77777), size));
                    return puzzle;
                case 3:
                    puzzle = UserInteraction.readUsersSnailBoard(size);
                    Logger.logCurrentState(HelperActions.splitData(new Puzzle(puzzle, 77777), size));
                    return puzzle;
                default:
                    return puzzle;
            }
        }
        // this method allows user to choose how does he want to provide an initial state board
        public static List<int> generateChoice(int size)
        {
            int choice = 0;
            List<int> puzzle = new List<int>();
            while(choice != 1 && choice != 2 && choice != 3){
                Console.WriteLine("Czy chcesz losowo wygenerowac plansze?");
                Console.WriteLine("1 - Tak, wygeneruj losowo wybraną planszę\n2 - Nie, chcę wczytać stan początkowy z pliku");
                Console.WriteLine("3 - Chcę sam wprowadzić stan początkowy.");
                choice = Convert.ToInt32(Console.ReadLine());
            }
            Logger.customLog("Stan początkowy:");
            switch (choice)
            {
                case 1:
                    puzzle = HelperActions.shuffleBoard(BoardsConst.createZeroFirstBoard(size), size);
                    Logger.logCurrentState(HelperActions.splitData(new Puzzle(puzzle, 88888), size));
                    return puzzle;
                case 2:
                    puzzle = readBoardFromFile(size);
                    Logger.logCurrentState(HelperActions.splitData(new Puzzle(puzzle, 88888), size));
                    return puzzle;
                case 3:
                    puzzle = UserInteraction.readUserBoard(size);
                    Logger.logCurrentState(HelperActions.splitData(new Puzzle(puzzle, 88888), size));
                    return puzzle;
            }
            return puzzle;
        }
        // this method allows user to set logging path so that the entire problem trace will be saved there
        public static void setLoggingPath()
        {
            Console.WriteLine("Proszę wprowadzić ścieżkę do pliku .txt, w którym zapisany zostanie przebieg programu.");
            
            string path = Console.ReadLine();
            Logger.logPath = @path;
            if (!File.Exists(@Logger.logPath))
            {
                File.Create(@Logger.logPath).Dispose();
                Console.WriteLine($"Nie odnaleziono preferowanego pliku, utworzono nowy pod ścieżką: {Logger.logPath}");
            }
            else{
                Console.WriteLine($"Pomyślnie ustawiono ścieżkę: {Logger.logPath}");
            }
        }
        // this method allows user to provide his desired puzzle problem from a keyboard
        public static List<int> readUserBoard(int size)
        {
            int len = 0;
            int[] intArray;
            List<int> board = new List<int>();
            while (len != size*size)
            {
                Console.WriteLine("Proszę wprowadzić stan początkowy problemu N puzzli podając kolejno liczby oddzielone spacją.");
                Console.WriteLine("Na przykład 1 0 2 3 5 4 6 8 7");
                Console.WriteLine("Wprowadzona wartość 0 oznacza puste pole");
                string input = Console.ReadLine();
                string[] stringArray = input.Split(' ');
                intArray = Array.ConvertAll(stringArray, int.Parse);
                board = new List<int>((int[])intArray);
                len = intArray.Length;
            }
            
            return board;
        }
        // method which allows user to provied his own snail board
        public static List<int> readUsersSnailBoard(int size)
        {
            int len = 0;
            int[] intArray;
            List<int> board = new List<int>();
            while (len != size * size)
            {
                Console.WriteLine($"Proszę wprowadzić tablicę w formacie snail i rozmiarze {size}x{size}");
                Console.WriteLine("Wartość każdej kafalki powinna być oddzielona spacja np. 0 1 2 7 8 3 6 5 4");
                Console.WriteLine("Wprowadzona wartość 0 oznacza puste pole");
                string input = Console.ReadLine();
                string[] stringArray = input.Split(' ');
                intArray = Array.ConvertAll(stringArray, int.Parse);
                board = new List<int>((int[])intArray);
                len = intArray.Length;
            }
            return board;
        }
        // this method allows user to provide his desired puzzle problem from a specific file
        public static List<int> readBoardFromFile(int size)
        {
            int[] intArray;
            Console.WriteLine("Proszę określić ścieżkę do pliku, z którego planszę powinienem wczytać");
            string puzzlePath = Console.ReadLine();
            List<int> puzzle = new List<int>();
            if (!File.Exists(puzzlePath))
            {
                Console.WriteLine($"Nie odnalazłem określonego pliku. Wykorzystam losowo wygenerowaną planszę w rozmiarze {size}x{size}");
                return BoardsConst.createRandomBoard(size);
            }
            string text = File.ReadAllText(@puzzlePath);
            string[] stringArray = text.Split(' ');
            intArray = Array.ConvertAll(stringArray, int.Parse);
            puzzle = new List<int>((int[])intArray);
            return puzzle;
        }

        // method which displays thank you message
        public static void goodbyeMessage()
        {
            Console.WriteLine("Dziękuję za skorzystanie z mojego programu.");
        } 

        // method which allows to display current board state on a console
        public static void displayCurrentState(List<List<int>> data)
        {
            for (int i = 0; i < data[0].Count; i++)
            {
                Console.WriteLine($"[{String.Join(",", data[i])}]");
            }
            
        }
        // method which allows to get to know whether user wants to solve one more problem
        public static bool doesUserWantToRunOnceMore()
        {
            string decision = " ";
            Console.WriteLine("Czy chcesz kontynuować? Wpisz y jeśli tak, n jeśli nie");
            while(decision != "y" && decision != "n")
            {
                Console.WriteLine("Proszę podać swój wybór");
                decision = Console.ReadLine();
                if(decision == "y")
                {
                    return true;
                }
                if(decision == "n")
                {
                    return false;
                }
            }
            return true;
        }
        // method which allows to get user's choice when it comes to a preferred algorithm and run program appropriately based on this
        public static void runPreferredAlgorithm(List<int> usersPuzzle, List<int> finalPuzzle, int heuristic, int size)
        {
            int input = 0;
            while(input != 1 && input != 2)
            {
                Console.WriteLine("Proszę wybrać pożądany algorytm");
                Console.WriteLine("1 - A* \n2 - RTA*");
                input = Convert.ToInt32(Console.ReadLine());
            }
            switch (input)
            {
                case 1:
                    AStar.runAStarSearch(usersPuzzle, finalPuzzle, heuristic, size);
                    return;
                case 2:
                    RtaStar.runRtaStarSearch(usersPuzzle, finalPuzzle, heuristic, size);
                    return;
            }

        }
        // method which allows to display the whole path to the end user
        public static void displayPath(List<Puzzle> steps, int size)
        {
            int iter = 0;
            foreach (Puzzle step in steps)
            {
                Console.WriteLine($"Iteracja {iter}...");
                for (int i = 0; i < size*size; i++)
                {
                    Console.Write($"{step.puzzle[i]} ");
                }
                iter++;
                Console.WriteLine("");
            }
            Console.WriteLine($"Koszt: {iter}");
            Console.WriteLine("");
        }
    }
}
