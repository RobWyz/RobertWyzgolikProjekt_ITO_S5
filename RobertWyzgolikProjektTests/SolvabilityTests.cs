using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RobertWyzgolikProjekt; 

namespace RobertWyzgolikProjektTests
{
    public class SolvabilityTests
    {
        [Fact]
        public static void givenUsersBoardPuzzleProblemShouldBeSolvable()
        {
            //given
            List<int> usersBoard = new List<int>() { 1, 0, 2, 3, 5, 4, 6, 8, 7 };
            List<int> finalBoard = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            //when
            //then
            Assert.True(Solvability.assessSolvability(usersBoard, finalBoard, 3));
        }
        
    }
}
