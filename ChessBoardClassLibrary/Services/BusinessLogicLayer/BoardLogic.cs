using ChessBoardClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Elijah Hodge
 * CST - 250
 * 07/04/2026
 * Activity 3
 */

namespace ChessBoardClassLibrary.Services.BusinessLogicLayer
{
    internal class BoardLogic
    {
        /// <summary>
        /// Reset the board by setting the
        /// cell properties back to default
        /// Encapsulate this method so it can only be
        ///   called from this class.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private BoardModel ResetBoard(BoardModel board)
        {
            // Use a foreach loop to iterate over every cell
            foreach (CellModel cell in board.Grid)
            {
                // Set the properties to their defaults
                cell.IsLegalNextMove = false;
                cell.PieceOccupyingCell = "";
            }
            // Return the board back to the presentation layer
            return board;
        }

        /// <summary>
        /// Check if a row/column location is on the board
        /// Encapsulate this method so it can only be
        ///   called from this class.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool IsOnBoard(BoardModel board, int row, int col)
        {
            // Get the size of the board
            int size = board.Size;
            // Check if the row is on the baord
            bool isRowSafe = row >= 0 && row < size;
            // Check if the column is on the board
            bool isColSafe = col >= 0 && col < size;
            // Return true if both row and column are safe
            return isRowSafe && isColSafe;
        }

        /// <summary>
        /// Mark the legal moves for the given piece and location
        /// </summary>
        /// <param name="board"></param>
        /// <param name="currentCell"></param>
        /// <param name="chessPiece"></param>
        /// <returns></returns>
        public BoardModel MarkLegalMoves(BoardModel board, CellModel currentCell, string chessPiece)
        {
            // Reset the board
            board = ResetBoard(board);

            // Use a switch statement to determine the behavior of the piece
            switch (chessPiece.ToLower())
            {
                case "Knight":
                    // Set the occupying property of the current cell
                    board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "N";
                    // Mark the valid moves for the knight
                    board = MarkValidKnightMoves(board, currentCell);
                    break;

                case "Rook":
                    // Set the occupying property of the current cell
                    board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "R";
                    // Mark the valid moves for the rook
                    board = MarkValidRookMoves(board, currentCell);
                    break;

                case "Bishop":
                    // Set the occupying property of the current cell
                    board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "B";
                    // Mark the valid moves for the bishop
                    board = MarkValidBishopMoves(board, currentCell);
                    break;

                case "Queen":
                    // Set the occupying property of the current cell
                    board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "Q";
                    // Mark the valid moves for the queen
                    board = MarkValidQueenMoves(board, currentCell);
                    break;

                case "King":
                    // Set the occupying property of the current cell
                    board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "K";
                    // Mark the valid moves for the king
                    board = MarkValidKingMoves(board, currentCell);
                    break;

                default:
                    // If the piece is not valid, return the board as is
                    return board;
            }
            // Return the updated board
            return board;
        } // End of MarkLegalMoves method

        /// <summary>
        /// Mark the valid moves for the knight
        /// Access modifier is private - meaning this method is encapsulated within the
        ///     BoardLogic class and cannot be accessed directly outside the class.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="currentCell"></param>
        /// <returns></returns>
        private BoardModel MarkValidKnightMoves(BoardModel board, CellModel currentCell)
        {
            // Possible move for the knight row
            int[] knightRowMoves = { 2, 1, -1, -2, -2, -1, 1, 2 };
            // Possible move for the knight column
            int[] knightColMoves = { 1, 2, 2, 1, -1, -2, -2, -1 };
            // Loop through the knights moves
            for (int i = 0; i < knightRowMoves.Length; i++)
            {
                // Check if each move is on the board
                if (IsOnBoard(board, currentCell.Row + knightRowMoves[i], currentCell.Column + knightColMoves[i]))
                {
                    // If the cell is on the board, label it as a possible move for the knight
                    board.Grid[currentCell.Row + knightRowMoves[i], currentCell.Column + knightColMoves
                        [i]].IsLegalNextMove = true;
                }
            }
            return board;
        }

        /// <summary>
        /// Mark the valid moves for the rook
        /// Access modifier is private - meaning this method is encapsulated within the
        ///     BoardLogic class and cannot be accessed directly outside the class.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="currentCell"></param>
        /// <returns></returns>
        private BoardModel MarkValidRookMoves(BoardModel board, CellModel currentCell)
        {
            for (int direction = 1; direction <= 4; direction++)
            {
                switch (direction)
                {
                    // Check the cells tabove the current cell
                    case 1:
                        //Get the current row and check the cells above it
                        for (int row = currentCell.Row + 1; row < board.Size; row++)
                        {
                            // Check if the position is a valid cell
                            if (IsOnBoard(board, row, currentCell.Column))
                            {
                                // If the cell is on the board, label it as a possible move for the rook
                                board.Grid[row, currentCell.Column].IsLegalNextMove = true;
                            }
                        }
                        break;
                    // Check the cells below the current cell
                    case 2:
                        // Get the current row and check the cells below it
                        for (int row = currentCell.Row - 1; row >= 0; row--)
                        {
                            // Check if the position is a valid cell
                            if (IsOnBoard(board, row, currentCell.Column))
                            {
                                // If the cell is on the board, label it as a possible move for the rook
                                board.Grid[row, currentCell.Column].IsLegalNextMove = true;
                            }
                        }
                        break;
                    // Check the cells to the left of the current cell
                    case 3:
                        // Get the current column and check the cells to the left of it
                        for (int col = currentCell.Column - 1; col >= 0; col--)
                        {
                            // Check if the position is a valid cell
                            if (IsOnBoard(board, currentCell.Row, col))
                            {
                                // If the cell is on the board, label it as a possible move for the rook
                                board.Grid[currentCell.Row, col].IsLegalNextMove = true;
                            }
                        }
                        break;
                    // Check the cells to the right of the current cell
                    case 4:
                        for (int col = currentCell.Column + 1; col < board.Size; col++)
                        {
                            // Check if the position is a valid cell
                            if (IsOnBoard(board, currentCell.Row, col))
                            {
                                // If the cell is on the board, label it as a possible move for the rook
                                board.Grid[currentCell.Row, col].IsLegalNextMove = true;
                            }
                        }
                        break;
                }
            }
            return board;
        }

        /// <summary>
        /// Mark the valid moves for the bishop
        /// Access modifier is private - meaning this method is encapsulated within the
        ///     BoardLogic class and cannot be accessed directly outside the class.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="currentCell"></param>
        /// <returns></returns>
        private BoardModel MarkValidBishopMoves(BoardModel board, CellModel currentCell)
        {
            // Enter a for loop for each direction
            for (int direction = 1; direction <= 4; direction++)
            {
                // Set a bool to track our valid moves
                bool isValidMove = true;
                // Set a integer for our current direction offset
                int curMove = 1;
                switch (direction)
                {
                    // Check the cells diagonally up and to the right of the current cell
                    case 1:
                        // While the move is valid, keep checking
                        while (isValidMove)
                        {
                            // Check if the position is a valid cell
                            if (IsOnBoard(board, currentCell.Row + curMove, currentCell.Column + curMove))
                            {
                                // If the cell is on the board, label it as a possible move for the bishop
                                board.Grid[currentCell.Row + curMove, currentCell.Column + curMove].IsLegalNextMove = true;
                                // Increment the current move offset
                                curMove++;
                            }
                            else
                            {   // If not, the move is no longer valid
                                isValidMove = false;
                            }

                        }
                        break;

                    // Check the cells diagonally up and to the left of the current cell
                    case 2:
                        // While the move is valid, keep checking
                        while (isValidMove)
                        {
                            if (IsOnBoard(board, currentCell.Row + curMove, currentCell.Column - curMove))
                            {
                                // If the cell is on the board, label it as a possible move for the bishop
                                board.Grid[currentCell.Row + curMove, currentCell.Column - curMove].IsLegalNextMove = true;
                                // Increment the current move offset
                                curMove++;
                            }
                            else
                            {   // If not, the move is no longer valid
                                isValidMove = false;
                            }
                        }
                        break;
                    // Check the cells diagonally down and to the right of the current cell
                    case 3:
                        // While the move is valid, keep checking
                        while (isValidMove)
                        {
                            // Check if the position is a valid cell
                            if (IsOnBoard(board, currentCell.Row - curMove, currentCell.Column + curMove))
                            {
                                // If the cell is on the board, label it as a possible move for the bishop
                                board.Grid[currentCell.Row - curMove, currentCell.Column + curMove].IsLegalNextMove = true;
                                // Increment the current move offset
                                curMove++;
                            }
                            else
                            {   // If not, the move is no longer valid
                                isValidMove = false;
                            }
                        }
                        break;

                    case 4:
                        // Check the cells diagonally down and to the left of the current cell
                        while (isValidMove)
                        {
                            // Check if the position is a valid cell
                            if (IsOnBoard(board, currentCell.Row - curMove, currentCell.Column - curMove))
                            {
                                // If the cell is on the board, label it as a possible move for the bishop
                                board.Grid[currentCell.Row - curMove, currentCell.Column - curMove].IsLegalNextMove = true;
                                // Increment the current move offset
                                curMove++;
                            }
                            else
                            {   // If not, the move is no longer valid
                                isValidMove = false;
                            }
                        }
                        break;
                }
            }
            return board;
        }


        /// <summary>
        /// Mark the valid moves for the queen
        /// Access modifier is private - meaning this method is encapsulated within the
        ///     BoardLogic class and cannot be accessed directly outside the class.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="currentCell"></param>
        /// <returns></returns>
        private BoardModel MarkValidQueenMoves(BoardModel board, CellModel currentCell)
        {
            // The queen's moves are a combination of the rook and bishop moves
            board = MarkValidRookMoves(board, currentCell);
            board = MarkValidBishopMoves(board, currentCell);
            return board;
        }

        /// <summary>
        /// Mark the valid moves for the king
        /// Access modifier is private - meaning this method is encapsulated within the
        ///     BoardLogic class and cannot be accessed directly outside the class.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="currentCell"></param>
        /// <returns></returns>
        private BoardModel MarkValidKingMoves(BoardModel board, CellModel currentCell)
        {
            // Possible move for the king row
            int[] kingRowMoves = { 1, 1, 0, -1, -1, -1, 0, 1 };
            // Possible move for the king column
            int[] kingColMoves = { 0, 1, 1, 1, 0, -1, -1, -1 };
            // Loop through the kings moves
            for (int i = 0; i < kingRowMoves.Length; i++)
            {
                // Check if each move is on the board
                if (IsOnBoard(board, currentCell.Row + kingRowMoves[i], currentCell.Column + kingColMoves[i]))
                {
                    // If the cell is on the board, label it as a possible move for the king
                    board.Grid[currentCell.Row + kingRowMoves[i], currentCell.Column + kingColMoves
                        [i]].IsLegalNextMove = true;
                }
            }
            return board;
        }
    }
}
