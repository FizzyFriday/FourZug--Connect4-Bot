using FourZug.Backend.TreeManager;
using FourZug.Backend.UtilityEngine;
using FourZug.Backend.HeuristicsEngine;


namespace FourZug.API
{
    // Provides a Backend interface for the frontend
    public class API : IAPI
    {
        private readonly HeuristicsEngine heuristicsEngine;
        private readonly TreeManager treeManager;
        private readonly UtilityEngine utilityEngine;


        /*
         * Does all referencing between component processors and
         * interfaces of other components
         */
        public API()
        {
            this.heuristicsEngine = new HeuristicsEngine();
            this.heuristicsEngine.InitComponentReferences();

            this.treeManager = new TreeManager();
            this.treeManager.InitComponentReferences();

            // UtilityEngine doesnt use other components
            // So no referencing needed
        }


        /*
         * Returns the best move given a game grid/board
         * @pre:
         *      @param - grid, represents the game board
         *      @param - turn, turn of current player
         * @post:
         *      @return - Returns column of best move
         */
        public int BestMove(string[,] grid, string turn)
        {
            return this.treeManager.GetBotBestMove(grid, turn);
        }


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
        public string[,] MakeMove(string[,] grid, string turn, int col)
        {
            return this.utilityEngine.MakeMove(grid, turn, col);
        }


        /*
         * Returns valid columns based on a game grid/board
         * @pre:
         *      @param - grid, represents the gameboard
         * @post:
         *      @return - Returns an int list of valid column moves
         */
        public List<int> ValidBoardColumns(string[,] grid)
        {
            List<byte> byteValidColumns = this.utilityEngine.GetValidBoardColumns(grid);

            // Creates an int list copy of the byte list
            List<int> intValidColumns = new();
            foreach (byte b in byteValidColumns)
            {
                intValidColumns.Add(b);
            }

            return intValidColumns;
        }


        /*
         * Returns state of the game given a grid/board
         * @pre:
         *      @param - grid, represents the game board
         *      @param - turn, turn of current player
         * @post:
         *      @return - String, representing state of game 
         *          String can be: Win (for current player), Draw or StillInPlay
         */
        public string BoardState(string[,] grid, string turn)
        {
            return this.heuristicsEngine.GetBoardStateAsString(grid, turn);
        }
    }
}
