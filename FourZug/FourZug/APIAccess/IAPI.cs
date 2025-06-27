namespace FourZug.APIAccess
{
    // Provides a Backend interface for the frontend
    public interface IAPI
    {
        // Implementations must have a constructor
        // This constructor does dependency injection 


        /*
         * Returns the best move given a game grid/board
         * @pre:
         *      @param - grid, represents the game board
         *      @param - turn, turn of current player
         * @post:
         *      @return - Returns column of best move
         */
        int BestMove(string[,] grid, string turn);


        /*
         * Returns the grid after making column move
         * @pre:
         *      @param - grid, represents the game board
         *      @param - turn, turn of current player
         *      @param - col, column user selected
         * @post:
         *      @modify - Makes move change to grid
         *      @return - Returns changed grid
         */
        string[,] MakeMove(string[,] grid, string turn, int col);


        /*
         * Returns the eval of each col move option given a board
         * @pre:
         *      @param - grid, represents the game board
         *      @param - turn, turn of current player
         * @post:
         *      @return - Dict where key is col, value is eval
        */
        public Dictionary<int, int> EvalMoves(string[,] grid, string turn);


        /*
         * Returns valid columns based on a game grid/board
         * @pre:
         *      @param - grid, represents the gameboard
         * @post:
         *      @return - Returns an int list of valid column moves
         */
        List<int> ValidBoardColumns(string[,] grid);


        /*
         * Returns state of the game given a grid/board
         * @pre:
         *      @param - grid, represents the game board
         *      @param - turn, turn of current player
         * @post:
         *      @return - Char, representing game winner (if applicable)
         *          Possiblities: 'X', 'O', 'D', '?'
         */
        char GetGameWinner(string[,] grid, string turn, int lastColMove);
    }
}
