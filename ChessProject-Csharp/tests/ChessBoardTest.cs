﻿using NUnit.Framework;
using System;

/*
Things to test:
- Cannot move past another piece (unless a Knight)
- Cannot move off the board
- Cannot Capture off the board
- Cannot perform invalid move
- Cannot perform invalid capture
- Can create custom board setup
- Cannot create a custom invalid board (size/dimensions)
- Can have multiple piece types on the board
- Can add multiple kinds of pieces to the board
*/

namespace SolarWinds.MSP.Chess
{
    [TestFixture]
	public class ChessBoardTest
	{
		private ChessBoard chessBoard;

        [SetUp]
		public void SetUp()
		{
			chessBoard = new ChessBoard();
		}

        [Test]
		public void Has_MaxBoardWidth_of_7()
		{
			Assert.AreEqual(ChessBoard.MaxBoardWidth, 7);
		}

        [Test]
		public void Has_MaxBoardHeight_of_7()
		{
			Assert.AreEqual(ChessBoard.MaxBoardHeight, 7);
		}

        [Test]
		public void IsLegalBoardPosition_True_X_equals_0_Y_equals_0()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(0, 0);
			Assert.IsTrue(isValidPosition);
		}

        [Test]
		public void IsLegalBoardPosition_True_X_equals_5_Y_equals_5()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(5, 5);
            Assert.IsTrue(isValidPosition);
		}

        [Test]
		public void IsLegalBoardPosition_False_X_equals_11_Y_equals_5()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(11, 5);
            Assert.IsFalse(isValidPosition);
		}

        [Test]
		public void IsLegalBoardPosition_False_X_equals_0_Y_equals_9()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(0, 9);
            Assert.IsFalse(isValidPosition);
		}

        [Test]
		public void IsLegalBoardPosition_False_X_equals_11_Y_equals_0()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(11, 0);
            Assert.IsFalse(isValidPosition);
		}

        [Test]
		public void IsLegalBoardPosition_False_For_Negative_X_Values()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(-1, 5);
            Assert.IsFalse(isValidPosition);
		}

        [Test]
		public void IsLegalBoardPosition_False_For_Negative_Y_Values()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(5, -1);
            Assert.IsFalse(isValidPosition);
		}

        [Test]
		public void Avoids_Duplicate_Positioning()
		{
			Pawn firstPawn = new Pawn(PieceColor.Black);
			Pawn secondPawn = new Pawn(PieceColor.Black);
			chessBoard.Add(firstPawn, 6, 3);
			try 
			{
				chessBoard.Add(secondPawn, 6, 3);
				Assert.Fail("Chessboard was able to add a piece to an already-occupied position.");
			} 
			catch (DuplicatePositioningException)
			{/* pass */}
			Assert.AreEqual(firstPawn.XCoordinate, 6);
            Assert.AreEqual(firstPawn.YCoordinate, 3);
            Assert.AreEqual(secondPawn.XCoordinate, -1);
            Assert.AreEqual(secondPawn.YCoordinate, -1);
		}

		[Test]
		public void Avoids_Invalid_Positioning()
		{
			Pawn firstPawn = new Pawn(PieceColor.Black);
			Pawn secondPawn = new Pawn(PieceColor.Black);
			chessBoard.Add(firstPawn, 6, 3);
			try 
			{
				chessBoard.Add(secondPawn, 10, 3);
				Assert.Fail("Chessboard was able to add a piece to an invalid position.");
			} 
			catch (InvalidPositioningException)
			{/* pass */}
			Assert.AreEqual(firstPawn.XCoordinate, 6);
            Assert.AreEqual(firstPawn.YCoordinate, 3);
            Assert.AreEqual(secondPawn.XCoordinate, -1);
            Assert.AreEqual(secondPawn.YCoordinate, -1);
		}

		[Test]
		public void IsPieceAvailable_Fails_When_Unavailable()
		{
			Pawn pawn = new Pawn(PieceColor.Black);
			for (int i = 0; i < 10; i++){
				if (i < 8)
				{
					Assert.IsTrue(chessBoard.IsPieceAvailable(pawn));
					// chessBoard.AvailablePiecesBlack.Pawns--;
				} else {
					Assert.IsFalse(chessBoard.IsPieceAvailable(pawn));
				}
			}
		}

        [Test]
		public void Add_Limits_The_Number_Of_Pawns()
		{
			for (int i = 0; i <= ChessBoard.MaxBoardHeight; i++)
			{
				Pawn pawn = new Pawn(PieceColor.Black);
				int row = i / ChessBoard.MaxBoardWidth;
				Console.WriteLine("Row: {0}, Col: {1}", (6 + row), i % ChessBoard.MaxBoardWidth);
				if (row < 1)
				{
					chessBoard.Add(pawn, (6 + row), i % ChessBoard.MaxBoardWidth);
					Assert.AreEqual(pawn.XCoordinate, (6 + row));
					Assert.AreEqual(pawn.YCoordinate, (i % ChessBoard.MaxBoardWidth));
				}
				else
				{
					try 
					{
						chessBoard.Add(pawn, 6 + row, i % ChessBoard.MaxBoardWidth);
					} 
					catch (InvalidPositioningException)
					{}
					Assert.AreEqual(pawn.XCoordinate, -1);
                    Assert.AreEqual(pawn.YCoordinate, -1);
				}
			}
		}
	}
}
