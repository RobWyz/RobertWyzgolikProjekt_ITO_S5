using System;
using Xunit;
using System.Collections.Generic;
using RobertWyzgolikProjekt;


namespace RobertWyzgolikProjektTests
{
    public class AssessEqualTest
    {
        [Fact]
        public static void checkEqualTrue()
        {
            //given
            Puzzle puzzle = new Puzzle(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, 99999);
            List<int> compare = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            //when

            //then
            Assert.True(Movement.assessEqual(puzzle, compare, 3));
        }
    }
}
