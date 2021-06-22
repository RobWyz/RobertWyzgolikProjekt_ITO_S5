using System;
using Xunit;
using System.Collections.Generic;
using RobertWyzgolikProjekt;


namespace RobertWyzgolikProjektTests
{
    public class HeuristicCountTests
    {
        [Fact]
        public static void HammingHeuristicTest()
        {
            //given
            Puzzle puzzle = new Puzzle(new List<int>() { 1, 2, 3, 0, 4, 5, 6, 7, 8 }, 1245778);
            int expectedHeuristic = 3;
            int size = 3;
            List<int> finalBoard = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            //when
            int actualHeuristic = HammingHeuristics.countHammingHeuristic(puzzle, finalBoard, size);
            //then
            Assert.Equal(expectedHeuristic, actualHeuristic);
        }

        [Fact]
        public static void ManhattanHeuristicTest()
        {
            //given
            Puzzle puzzle = new Puzzle(new List<int>() { 1, 0, 2, 4, 3, 5, 8, 7, 6 }, 124854);
            int expectedHeuristic = 7;
            int size = 3;
            List<int> finalBoard = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            //when
            int actualHeuristic = ManhattanHeuristics.countManhattanHeuristic(puzzle, finalBoard, size);
            //then
            Assert.Equal(expectedHeuristic, actualHeuristic);
        }
    }
}
