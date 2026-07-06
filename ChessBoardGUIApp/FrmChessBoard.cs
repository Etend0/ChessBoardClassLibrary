using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;

/*
 * Elijah Hodge
 * CST - 250
 * 07/05/2026
 * Chess Board Project
 * Activity 2
 */

namespace ChessBoardGUIApp
{
    public partial class FrmChessBoard : Form
    {
        // Class level variables
        private BoardModel _board;
        private BoardLogic _boardLogic;
        // 2D array of buttons for the chess board
        private Button[,] _buttons;

        // Arrays of colors
        private Color[] _basicColors = new Color[] { Color.White, Color.Gray, Color.Red, Color.Purple, Color.Aqua, Color.Green, Color.Blue };

        private Color[] _coolColors = new Color[] { Color.Cyan, Color.Magenta, Color.Orange, Color.Brown, Color.DarkGreen, Color.LightGreen, Color.Red };

        private Color[] _warmColors = new Color[] { Color.Red, Color.Orange, Color.Yellow, Color.Brown, Color.Purple, Color.Magenta, Color.LightPink };

        private Color[] _coldColors = new Color[] { Color.Blue, Color.Cyan, Color.LightBlue, Color.DarkBlue, Color.LightSkyBlue, Color.LightSteelBlue, Color.DarkSlateBlue };

        private Color[] _neutralColors = new Color[] { Color.White, Color.DarkGray, Color.LightGray, Color.DarkGray, Color.Silver, Color.DimGray, Color.Gainsboro };

        private Color[] _neonColors = new Color[] { Color.Lime, Color.Magenta, Color.Cyan, Color.Yellow, Color.Orange, Color.Purple, Color.Red };

        // Array to hold all color arrays
        private Color[][] _colorSchemes;

        // Set the current theme
        string currentTheme = "Basic Colors";

        // Set the current index
        int currentThemeIndex = 0;

        /// <summary>
        /// Default constructor for FrmChessBoard
        /// </summary>
        public FrmChessBoard()
        {
            InitializeComponent();

            // Initialize class level variables
            _board = new BoardModel(8);
            _boardLogic = new BoardLogic();
            _buttons = new Button[8, 8];

            // Initialize the color schemes array
            _colorSchemes = new Color[][]
            {
                _basicColors,
                _coolColors,
                _warmColors,
                _coldColors,
                _neutralColors,
                _neonColors
            };

            // Setup the panel with buttons
            SetUpButtons();
        }

        /// <summary>
        /// Populate the panel control with buttons
        /// </summary>
        private void SetUpButtons()
        {
            // Declaire and initialize
            // Calculate the size of each button based on
            // the panel width and the number of buttons needs
            int buttonSize = pnlChessBoard.Width / _board.Size;
            // Set the panel to be square
            pnlChessBoard.Height = pnlChessBoard.Width;
            // Use nested for loops to loop through the boards Grid
            for (int row = 0; row < _board.Size; row++)
            {
                for (int col = 0; col < _board.Size; col++)
                {
                    // Set up each individual button
                    // Create a new button in the 2D array
                    _buttons[row, col] = new Button();
                    // Get the current button
                    Button button = _buttons[row, col];
                    // Set the size for the button
                    button.Width = buttonSize;
                    button.Height = buttonSize;
                    // Set the location of the button
                    // using the left and top sides
                    button.Left = row * buttonSize;
                    button.Top = col * buttonSize;
                    // Set the checkorboard pattern colors
                    button.BackColor = (row + col) % 2 == 0 ? _colorSchemes[0][0] : _colorSchemes[0][1];
                    // Attach a click event handler to the button
                    button.Click += BtnSquareClickEH;
                    // Store the location of the button in
                    // the Tag property using a Point object
                    button.Tag = new Point(row, col);
                    // Set the text for the button
                    button.Text = $"{row}, {col}";
                    // Add the button to the panel's controls
                    pnlChessBoard.Controls.Add(_buttons[row, col]);
                }
            }

        } // End of SetUpButtons method

        /// <summary>
        /// Click even handler for the chess board buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSquareClickEH(object? sender, EventArgs e)
        {
            // Declare and initialize
            Button button = (Button)sender;
            Point point = (Point)button.Tag;
            int row = point.X;
            int col = point.Y;
            string piece = cmbChessPieces.Text;

            // Show the user their choice
            MessageBox.Show($"You clicked on row {row} and column {col}");
            // Send the board, current cell, and piece to the business logic layer
            _board = _boardLogic.MarkLegalMoves(_board, _board.Grid[row, col], piece);
            // Update the buttons
            UpdateButtons();
        }

        /// <summary>
        /// Update the text for each button based on the board
        /// </summary>
        private void UpdateButtons()
        {
            // Declare and initialize
            string piece;

            int theme = 0;
            // Set up a directory to get the names of the chess pieces
            Dictionary<string, string> pieceMap = new Dictionary<string, string>()
            {
                { "N", "Knight" },
                { "R", "Rook" },
                { "B", "Bishop" },
                { "Q", "Queen" },
                { "K", "King" }
            };

            // Loop through each cell in the grid to update the corresponding button
            for (int row = 0; row < _board.Size; row++)
            {
                for (int col = 0; col < _board.Size; col++)
                {
                    if (_board.Grid[row, col].PieceOccupyingCell != "")
                    {
                        // Use the dictionary to get the name of the chess piece
                        piece = pieceMap[_board.Grid[row, col].PieceOccupyingCell];

                        switch (piece)
                        {
                            case "King":
                                theme = 2;
                                break;
                            case "Queen":
                                theme = 3;
                                break;
                            case "Bishop":
                                theme = 4;
                                break;
                            case "Knight":
                                theme = 5;
                                break;
                            case "Rook":
                                theme = 6;
                                break;
                        }

                        // Set the text color
                        _buttons[row, col].ForeColor = _colorSchemes[currentThemeIndex][theme];
                        // Update the text for the button
                        _buttons[row, col].Text = piece;
                    }
                    else if (_board.Grid[row, col].IsLegalNextMove)
                    {
                        // Set the text to show a legal move
                        _buttons[row, col].ForeColor = Color.Black;
                        _buttons[row, col].Text = "Legal Move";
                    }
                    else
                    {
                        _buttons[row, col].ForeColor = Color.White;
                        // Clear the text for any other buttons
                        _buttons[row, col].Text = "";
                    }
                }
            }
        } // End of UpdateButtons method

        /// <summary>
        /// Update color scheme based on the user's selection from the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbThemeSelected(object sender, EventArgs e)
        {
            // Grab the name of the current color theme
            string theme = cmbTheme.Text;

            // Grab the index of the selected theme
            int themeIndex = cmbTheme.SelectedIndex;

            // Check if our theme has changed
            if (currentTheme != theme)
            {
                // If it has, update the current theme and get the selected colors
                currentTheme = theme;
                // Get the selected colors based on the index of the combo box
                Color[] selectedColors = _colorSchemes[cmbTheme.SelectedIndex];

                // Update the current theme index
                currentThemeIndex = themeIndex;

                // Update the button colors based on the selected theme through each row
                for (int row = 0; row < _board.Size; row++)
                {
                    // Update the button colors based on the selected theme through each column
                    for (int col = 0; col < _board.Size; col++)
                    {
                        // Set the button colors based on the selected theme and the checkered pattern
                        _buttons[row, col].BackColor = (row + col) % 2 == 0 ? selectedColors[0] : selectedColors[1];
                    }
                }
            }

            // Update the rest of the text on the board to use the current theme
            UpdateButtons();
        }
    }
}
