using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;
using System.Drawing;

/*
 * Elijah Hodge
 * CST - 250
 * 07/05/2026
 * Chess Board Project
 * Activity 2
 */

//------------------------------------------------------
// Start of Main Method
// -----------------------------------------------------

// Declare and initialize
string piece = "";
Tuple<int, int>? result;
BoardLogic boardLogic = new BoardLogic();

// Print a welcome message for the user
Console.WriteLine("Welcome, Chess Players!");

// Make a line break
Console.WriteLine();

// Create a new chess board
BoardModel board = new BoardModel(8);

// Show the empty board
Utility.PrintBoard(board);

// Prompt the user for the type of chess piece
piece = Utility.GetPiece(piece);

// Prompt the user for the location of the chess piece
result = Utility.GetRowAndCol(board.Size);

// Mark the legal moves based on the input
board = boardLogic.MarkLegalMoves(board, board.Grid[result.Item1, result.Item2], piece);

// Print out the new chess board
Utility.PrintBoard(board);

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
        // Column headers
        Console.Write("   ");

        // Loop through the columns and print the column index as a header
        for (int k = 0; k < board.Size; k++)
        {
            Console.Write($" {k,2} ");
        }
        Console.WriteLine();

        // Top border
        Console.Write("   ");
        for (int k = 0; k < board.Size; k++)
        {
            Console.Write("+---");
        }
        Console.WriteLine("+");

        // Loop over the rows of the board
        for (int row = 0; row < board.Size; row++)
        {
            Console.Write($"{row,2} ");

            // Loop over the columns of the board
            for (int col = 0; col < board.Size; col++)
            {
                // Get the current cell from the grid
                CellModel cell = board.Grid[row, col];
                // Check if the current cell is a legal move
                if (cell.IsLegalNextMove)
                {
                    // Print a + for a legal move and draw the cell borders
                    Console.Write($"|");
                    Console.Write(" + ");
                }
                else if (cell.PieceOccupyingCell != "")
                {
                    // Print the chess piece letter and draw the cell borders
                    Console.Write($"|");
                    Console.Write($" {cell.PieceOccupyingCell} ");
                }
                else
                {
                    // Print a space for an empty cell and draw the cell borders
                    Console.Write($"|");
                    Console.Write("   ");
                }
            }
            // Draw ending cell border
            Console.Write($"| ");

            // Start a mew line after each row
            Console.WriteLine();

            Console.Write("   ");
            // Draw the horizontal border after each row
            for (int k = 0; k < board.Size; k++)
            {
                Console.Write("+---");
            }
            Console.WriteLine("+");
        }
    } // End of PrintBoard method

    /// <summary>
    /// Get the type of chess piece from the user and validate it
    /// </summary>
    /// <param name="piece"></param>
    /// <returns></returns>
    internal static string GetPiece(string piece)
    {
        // Declare a boolean variable to track if the input is valid
        bool validPiece = false;

        // Loop until the user provides valid input
        while (!validPiece)
        {
            // Promt the user for the type of chess piece
            Console.Write("Please enter the type of piece you want to place (Knight, Rook, Bishop, Queen, King): ");
            piece = Console.ReadLine();

            // Validate the input to ensure it's not a number
            if (int.TryParse(piece, out _))
            {
                Console.WriteLine("Invalid input. Please enter Knight, Rook, Bishop, Queen, or King.");
                continue;
            }
            else
            {
                // Validate the input by comparing it to all the valid chess piece types
                if (piece.Equals("Knight", StringComparison.OrdinalIgnoreCase) ||
                    piece.Equals("Rook", StringComparison.OrdinalIgnoreCase) ||
                    piece.Equals("Bishop", StringComparison.OrdinalIgnoreCase) ||
                    piece.Equals("Queen", StringComparison.OrdinalIgnoreCase) ||
                    piece.Equals("King", StringComparison.OrdinalIgnoreCase))
                {
                    // If the input is valid, set the validPiece flag to true to exit the loop
                    validPiece = true;
                }
                else
                {
                    // If the input is invalid, prompt the user to enter valid input
                    Console.WriteLine("Invalid piece type. Please enter Knight, Rook, Bishop, Queen, or King.");
                }
            }
        }
        // Return the valid piece type
        return piece;
    }

    /// <summary>
    /// Get the row and column for the piece
    /// </summary>
    /// <param name="boardSize"></param>
    /// <returns></returns>
    internal static Tuple<int, int> GetRowAndCol(int boardSize)
    {
        // Declare variables to hold the row and column values
        int row = 0;
        int col = 0;

        // Declare a boolean variable to track if the input is valid
        bool validInput = false;

        // Loop until the user provides valid input
        while (!validInput)
        {
            // Try/catch to stop the user from entering invalid input
            try
            {
                // Get the row from the user
                Console.Write("Enter the row number of the piece: ");
                row = int.Parse(Console.ReadLine());
                // Get the column from the user
                Console.Write("Enter the column number of the piece: ");
                col = int.Parse(Console.ReadLine());

                // Validate the input to ensure it's within the bounds of the board
                if (row >= 0 && row < boardSize && col >= 0 && col < boardSize)
                {
                    validInput = true;
                }
                else
                {
                    // Otherwise prompt the user to enter valid input
                    Console.WriteLine($"Invalid position. Please enter values between 0 and {boardSize - 1}.");
                }
            }
            catch (FormatException)
            {
                // If an exception is caught, prompt the user to enter valid input
                Console.WriteLine("Invalid input. Please enter numeric values for row and column.");
            }
        }

        // Return the data
        return Tuple.Create(row, col);
    }
}