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

        // Main constructor
        public Node(string[,] grid, string turn, int col)
        {
            this.gameGrid = grid;
            this.turn = turn;
            this.col = col;

            // Gets the game grid after the node's move
            string[,] postMoveGrid = GameUtility.MakeMove(this.gameGrid, this.turn, this.col);

            // Gets all possible column moves
            List<int> validChildCols = GameUtility.ValidColumns(postMoveGrid);
            this.unaddedChildCols = validChildCols;
        }

        // Adds all children to node at once (Remove later with lazy expansion)
        public void AddBranches()
        {
            // Runs through all possible children
            foreach (int childCol in unaddedChildCols)
            {
                // Get the game grid and move of any child node
                string[,] postMoveGrid = GameUtility.MakeMove(this.gameGrid, this.turn, this.col);
                string childTurn = "O";
                if (childTurn == "O") childTurn = "X";

                // Add child to tree
                Node child = new Node(postMoveGrid, childTurn, childCol);
                children.Add(child);
            }

            unaddedChildCols.Clear();
        }

        // - PRIVATE METHODS -

    }
}
