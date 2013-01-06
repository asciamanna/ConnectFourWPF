using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ConnectFourWPF;

namespace ConnectFourWPFTests {
  [TestFixture]
  public class GameBoardTest {
    GameBoard board;

    [SetUp]
    public void Setup() {
      board = new GameBoard();
      board.Initialize();
    }

    [Test]
    public void Initialize() {
      Assert.IsTrue(board.IsEmpty);
    }

    [Test]
    public void PlayDisc() {
      var expectedFirstLocation = new Location(GameBoard.MaxRow, 1);

      var result = board.PlayDisc(Disc.Red, 1);
      Assert.AreEqual(expectedFirstLocation, board.LastLocationPlayed);

      var expectedSecondLocation = new Location(GameBoard.MaxRow - 1, 1);
      board.PlayDisc(Disc.Black, 1);
      Assert.AreEqual(expectedSecondLocation, board.LastLocationPlayed);
      Assert.IsTrue(result);
    }

    [Test]
    public void PlayDisc_Fails_If_Not_A_Valid_Column() {
      Assert.Throws<ArgumentException>(() => board.PlayDisc(Disc.Red, 8), "Column must be between 1 and 7");
      Assert.Throws<ArgumentException>(() => board.PlayDisc(Disc.Red, 0), "Column must be between 1 and 7");
    }

    [Test]
    public void PlayDisc_Returns_False_If_Disc_Was_Not_Placed_On_Board_Because_Column_Is_Full() {
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Red, 1);
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Red, 1);
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Red, 1);

      Assert.IsFalse(board.PlayDisc(Disc.Black, 1));
    }

    [Test]
    public void HorizontalWins_Middle_Board() {
      board.PlayDisc(Disc.Black, 2);
      board.PlayDisc(Disc.Black, 3);
      board.PlayDisc(Disc.Black, 5);
      board.PlayDisc(Disc.Black, 4);
      Assert.IsTrue(board.Winner());
    }

    [Test]
    public void HorizontalWins_LeftEdge_Board() {
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Black, 4);
      board.PlayDisc(Disc.Black, 3);
      board.PlayDisc(Disc.Black, 2);
      Assert.IsTrue(board.Winner());
    }

    [Test]
    public void HorizontalWins_RightEdge_Board() {
      board.PlayDisc(Disc.Black, 6);
      board.PlayDisc(Disc.Black, 5);
      board.PlayDisc(Disc.Black, 4);
      board.PlayDisc(Disc.Black, 7);
      Assert.IsTrue(board.Winner());
    }

    [Test]
    public void HorizontalWins_Does_Not_Win_When_Discs_Are_Not_Contiguous() {
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Black, 3);
      board.PlayDisc(Disc.Black, 6);
      board.PlayDisc(Disc.Black, 4);
      Assert.IsFalse(board.Winner());
    }

    [Test]
    public void VerticalWins_Middle_Board() {
      board.PlayDisc(Disc.Red, 1);
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Black, 1);
      Assert.IsTrue(board.Winner());
    }

    [Test]
    public void VerticallWins_Bottom_Board() {
      board.PlayDisc(Disc.Black, 3);
      board.PlayDisc(Disc.Black, 3);
      board.PlayDisc(Disc.Black, 3);
      board.PlayDisc(Disc.Black, 3);
      Assert.IsTrue(board.Winner());
    }

    [Test]
    public void VerticalWins_Top_Board() {
      board.PlayDisc(Disc.Red, 5);
      board.PlayDisc(Disc.Red, 5);
      board.PlayDisc(Disc.Black, 5);
      board.PlayDisc(Disc.Black, 5);
      board.PlayDisc(Disc.Black, 5);
      board.PlayDisc(Disc.Black, 5);
      Assert.IsTrue(board.Winner());
    }

    [Test]
    public void VerticallWins_Does_Not_Win_When_Discs_Are_Not_Contiguous() {
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Red, 1);
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Black, 1);
      Assert.IsFalse(board.Winner());
    }

    [Test]
    public void Diagonal_NorthWestSouthEastWins() {
      board.PlayDisc(Disc.Red, 6);
      board.PlayDisc(Disc.Black, 5);
      board.PlayDisc(Disc.Red, 5);
      board.PlayDisc(Disc.Black, 4);
      board.PlayDisc(Disc.Black, 4);
      board.PlayDisc(Disc.Red, 4);
      board.PlayDisc(Disc.Black, 3);
      board.PlayDisc(Disc.Black, 3);
      board.PlayDisc(Disc.Black, 3);
      board.PlayDisc(Disc.Red, 3);
      Assert.IsTrue(board.Winner());
    }

    [Test]
    public void Diagonal_NorthEastSouthWestWins() {
      board.PlayDisc(Disc.Red, 1);
      board.PlayDisc(Disc.Black, 2);
      board.PlayDisc(Disc.Red, 2);
      board.PlayDisc(Disc.Black, 3);
      board.PlayDisc(Disc.Black, 3);
      board.PlayDisc(Disc.Red, 3);
      board.PlayDisc(Disc.Black, 4);
      board.PlayDisc(Disc.Black, 4);
      board.PlayDisc(Disc.Black, 4);
      board.PlayDisc(Disc.Red, 4);
      Assert.IsTrue(board.Winner());
    }

    [Test]
    public void LastLocationPlayedAsOrderedIndex() {
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Black, 1);
      board.PlayDisc(Disc.Black, 1);
      var index = board.LastLocationPlayedAsOrderedIndex();
      Assert.AreEqual(21, index);
    }
  }
}