using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;

namespace ChessBoardGUIApp
{
    public partial class FrmChessBoard : Form
    {
        // Class level variables
        private BoardModel _board;
        private BoardLogic _boardLogic;
        // 2D array of buttons for the chess board
        private Button[,] _buttons;

        public FrmChessBoard()
        {
            InitializeComponent();

            // Initialize class level variables
            _board = new BoardModel(8);
            _boardLogic = new BoardLogic();
            _buttons = new Button[8, 8];

            // Set up the panel with buttons
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
    }
}
