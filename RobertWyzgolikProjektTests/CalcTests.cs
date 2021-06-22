using System;
using Xunit;
using System.Collections.Generic;
using RobertWyzgolikProjekt;


namespace RobertWyzgolikProjektTests
{
    public class CalcTests
    {
        
        [Theory]
        [InlineData(2, 1, 7, 3)]
        [InlineData(2, 2, 8, 3)]
        [InlineData(3, 1, 13, 4)]
        [InlineData(3, 3, 15, 4)]
        public static void calcCordsFromIndexTest(int x, int y, int index, int size)
        {
            //given
            List<int> expectedIndexes = new List<int>() { x, y};

            //when
            List<int> actualIndexes = new List<int>(HelperActions.calcCordsFromIndex(index, size));
            //then
            for (int i = 0; i < actualIndexes.Count; i++)
            {
                Assert.Equal(expectedIndexes[i], actualIndexes[i]);
            }
        }

        [Theory]
        [InlineData(2, 2, 8, 3)]
        [InlineData(1, 3, 7, 4)]
        public static void calcIndexFromCordsTest(int x, int y, int expectedIndex, int size)
        {
            //given
            List<int> cords = new List<int>() { x, y };
            //when
            int actualIndex = HelperActions.calcIndexFromCords(cords, size);
            //then
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public static void calcPossibleMovesMiddle()
        {
            //given
            int index = 4;
            int size = 3;
            List<int> expectedMoves = new List<int>() { 2, 4, 6, 8};
            //when
            List<int> actualMoves = new List<int>(HelperActions.returnMoves(index, size));
            actualMoves.Sort();
            //then
            for (int i = 0; i < actualMoves.Count; i++)
            {
                Assert.Equal(expectedMoves[i], actualMoves[i]);
            }
            
        }
        [Fact]
        public static void calcPossibleMovesLeftUpper()
        {
            //given
            int index = 0;
            int size = 4;
            List<int> expectedMoves = new List<int>() { 2, 6};
            //when
            List<int> actualMoves = new List<int>(HelperActions.returnMoves(index, size));
            actualMoves.Sort();
            //then
            for (int i = 0; i < actualMoves.Count; i++)
            {
                Assert.Equal(expectedMoves[i], actualMoves[i]);
            }

        }
        [Fact]
        public static void calcPossibleMovesRightUpper()
        {
            //given
            int index = 4;
            int size = 5;
            List<int> expectedMoves = new List<int>() { 2, 4 };
            //when
            List<int> actualMoves = new List<int>(HelperActions.returnMoves(index, size));
            actualMoves.Sort();
            //then
            for (int i = 0; i < actualMoves.Count; i++)
            {
                Assert.Equal(expectedMoves[i], actualMoves[i]);
            }

        }
        [Fact]
        public static void calcPossibleMovesLeftBottom()
        {
            //given
            int index = 6;
            int size = 3;
            List<int> expectedMoves = new List<int>() { 6, 8 };
            //when
            List<int> actualMoves = new List<int>(HelperActions.returnMoves(index, size));
            actualMoves.Sort();
            //then
            for (int i = 0; i < actualMoves.Count; i++)
            {
                Assert.Equal(expectedMoves[i], actualMoves[i]);
            }

        }
        [Fact]
        public static void calcPossibleMovesRightBottom()
        {
            //given
            int index = 24;
            int size = 5;
            List<int> expectedMoves = new List<int>() { 4, 8 };
            //when
            List<int> actualMoves = new List<int>(HelperActions.returnMoves(index, size));
            actualMoves.Sort();
            //then
            for (int i = 0; i < actualMoves.Count; i++)
            {
                Assert.Equal(expectedMoves[i], actualMoves[i]);
            }

        }


    }
}
