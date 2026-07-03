using ChessBoardClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Elijah Hodge
 * CST - 250
 * 07/02/2026
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
                case "knight":
                    // Set the occupying property of the current cell
                    board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "N";
                    // Mark the valid moves for the knight
                    board = MarkValidKnightMoves(board, currentCell);
                    break;

                case "rook":
                    break;

                case "bishop":
                    break;

                case "queen":
                    break;

                case "king":
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
        ///   BoardLogic class and cannot be accessed directly outside the class.
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
    }
}
