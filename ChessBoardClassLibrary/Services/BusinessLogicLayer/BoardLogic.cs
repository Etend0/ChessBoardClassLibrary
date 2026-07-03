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
                cell.IsLegelNextMove = false;
                cell.PieceOccypyingCell = "";
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
    }
}
