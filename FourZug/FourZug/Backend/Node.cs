namespace FourZug.Backend
{
    // Contains necessary information for nodes
    // Contains helpful methods concerning tree usage
    internal class Node
    {
        // - PARAMETERS -
        private string[,] gameGrid;
        private string turn;
        private int col;

        private List<Node> children;
        private List<int> unaddedChildCols;


        // - PUBLIC METHODS -

        // Root constructor
        public Node(string[,] grid, string turn)
        {
            this.gameGrid = grid;
            this.turn = turn;
            SetValidCols();
        }

        // Main constructor
        public Node(string[,] grid, string turn, int col)
        {
            this.gameGrid = grid;
            this.turn = turn;
            this.col = col;
            SetValidCols();
        }

        // - PRIVATE METHODS -
        private void SetValidCols()
        {
            // Gets the game grid after the node's move
            string[,] postMoveGrid = GameUtility.MakeMove(this.gameGrid, this.turn, this.col);

            // Gets all possible column moves
            List<int> validChildCols = GameUtility.ValidColumns(postMoveGrid);
            this.unaddedChildCols = validChildCols;
        }

    }
}
