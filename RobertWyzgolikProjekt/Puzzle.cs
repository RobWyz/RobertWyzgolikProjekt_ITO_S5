using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertWyzgolikProjekt
{
    // specific state of a puzzle board is an object of this class together with it's assigned id
    public class Puzzle
    {
        public List<int> puzzle;
        public int id;
       
        public Puzzle(List<int> board, int identifier)
        {
            puzzle = new List<int>(board);
            id = identifier;
        }
    }
}
