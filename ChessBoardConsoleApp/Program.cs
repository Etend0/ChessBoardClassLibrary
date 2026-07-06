using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;
using System.Drawing;

/*
 * Elijah Hodge
 * CST - 250
 * 03/09/2026
 * Chess Board Project
 * Activity 2
 */

//------------------------------------------------------
// Start of Main Method
// -----------------------------------------------------

// Declare and initialize
string piece = "";
Tuple<int, int>? result;

// Print a welcome message for the user
Console.WriteLine("Hello, Chess Players!");

// Create a new chess board
BoardModel board = new BoardModel(8);

// Show the empty board
Utility.PrintBoard(board);

// Prompt the user for the type of chess piece
Console.Write("Please enter the type of piece you want to place (Knight, Rook, Bishop, Queen, King): ");
piece = Console.ReadLine();

// Prompt the user for the location of the chess piece
result = Utility.GetRowAndCol();

// Mark the legal moves based on the input

// Print out the new chess board

//------------------------------------------------------
// End of Main Method
// -----------------------------------------------------

//------------------------------------------------------
// Define a utility class
// -----------------------------------------------------
public static class Utility
{
    /// <summary>
    /// Print the given board to the console
    /// </summary>
    /// <param name="board"></param>
    internal static void PrintBoard(BoardModel board)
    {
        // Loop over the rows of the board
        for (int row = 0; row < board.Size; row++)
        {
            // Loop over the columns of the board
            for (int col = 0; col < board.Size; col++)
            {
                // Get the current cell from the grid
                CellModel cell = board.Grid[row, col];
                // Check if the current cell is a legal move
                if (cell.IsLegalNextMove)
                {
                    // Print a + for a legal move
                    Console.Write(" + ");
                }
                else if (cell.PieceOccupyingCell != "")
                {
                    // Print the chess piece letter
                    Console.Write($" {cell.PieceOccupyingCell} ");
                }
                else
                {
                    // Print a . for anything else
                    Console.Write(" . ");
                }
            }
            // Start a mew line after every row
            Console.WriteLine();
        }
    } // End of PrintBoard method

    /// <summary>
    /// Get the row and column for the piece
    /// </summary>
    /// <param name="boardSize"></param>
    /// <returns></returns>
    internal static Tuple<int, int> GetRowAndCol()
    {
        // Get the row from the user
        Console.Write("Enter the row number of the piece: ");
        int row = int.Parse(Console.ReadLine());

        // Get the column from the user
        Console.Write("Enter the column number of the piece: ");
        int col = int.Parse(Console.ReadLine());

        // Return the data
        return Tuple.Create(row, col);
    }
}