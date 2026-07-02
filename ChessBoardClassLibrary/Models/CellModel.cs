using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Elijah Hodge
 * CST - 250
 * 07/01/2026
 * Activity 3
 */

namespace ChessBoardClassLibrary.Models
{
    internal class CellModel
    {
        // Class level properties with public getters and private setters.
        // This is an example of encapsulation: external code can read the values,
        // but only this class can modify them.
        public int Row { get; private set; }
        public int Column { get; private set; }

        // These properties need to be both readable and writable from outside the model,
        // so we use public getters and setters. This is appropriate for properties
        // where external components (e.g., the board logic) are responsible for updating them.
        public string PieceOccypyingCell { get; private set; }
        public bool IsLegelNextMove { get; private set; }

        /// <summary>
        /// Parameterized Constructor for cell model class
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public CellModel(int row, int column)
        {
            Row = row;
            Column = column;
            // Set default values for the other properties
            PieceOccypyingCell = "";
            IsLegelNextMove = false;
        }
    }
}
