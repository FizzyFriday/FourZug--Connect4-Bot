using FourZug.Backend.HeuristicsEngineAccess;
using FourZug.Backend.TreeManagerAccess;
using FourZug.Backend.UtilityEngineAccess;

/*
 * Has access permission for assemblies:
 *     UtilityEngineAccess
 *     HeuristicsEngineAccess
 *     TreeManagerAccess
 *     DTOs - if imported
 */


namespace FourZug.APIAccess
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
            this.treeManager = new TreeManager();
            this.utilityEngine = new UtilityEngine();

            // Inject contract dependencies into the components
            this.heuristicsEngine.InitComponentReferences(utilityEngine);
            this.treeManager.InitComponentReferences(heuristicsEngine, utilityEngine);         
        }


        /*
         * Returns the best move given a game grid/board
         * @pre:
         *      @param - grid, represents the game board
         *      @param - turn, turn of current player
         * @post:
         *      @return - Returns column of best move
         */
        public int BestMove(char[,] grid, char turn)
        {
            return this.treeManager.BestMove(grid, turn);
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
        public char[,] MakeMove(char[,] grid, char turn, int col)
        {
            int[] availableRows = this.utilityEngine.GetAvailableRows(grid);
            return this.utilityEngine.MakeMove(grid, turn, col, availableRows[col]);
        }


        /*
         * Returns valid columns based on a game grid/board
         * @pre:
         *      @param - grid, represents the gameboard
         * @post:
         *      @return - Returns an int list of valid column moves
         */
        public List<int> GetValidMoves(char[,] grid)
        {
            List<byte> byteValidColumns = this.utilityEngine.GetValidMoves(grid);

            // Creates an List<int> copy of the byte list
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
        public char GetGameWinner(char[,] grid, char turn, int lastColMove)
        {
            return this.heuristicsEngine.BoardWinner(grid, turn, lastColMove);
        }
    }
}
