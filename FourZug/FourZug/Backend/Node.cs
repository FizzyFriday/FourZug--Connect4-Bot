namespace FourZug.Backend
{
    // Contains necessary information for nodes
    // Contains helpful methods concerning tree usage
    internal class Node
    {
        // - PARAMETERS -
        private string[,] gameGrid;
        private string turn;
        private int[]? move;

        private List<Node> children;
        private List<int[]> unaddedChildMoves;

        // - PUBLIC METHODS -

        // Root constructor
        public Node(string[,] grid, string turn)
        {
            this.gameGrid = grid;
            this.turn = turn;
            // Set value of unaddedChildMove
        }

        // Main constructor
        public Node(string[,] grid, string turn, int[] move)
        {
            this.gameGrid = grid;
            this.turn = turn;
            this.move = move;
            // Set value of unaddedChildMove
        }

        // - PRIVATE METHODS -


    }
}
