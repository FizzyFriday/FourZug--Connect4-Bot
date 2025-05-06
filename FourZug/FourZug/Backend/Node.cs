namespace FourZug.Backend
{
    // Contains necessary information for nodes
    // Contains helpful methods concerning tree usage
    internal class Node
    {
        // - PARAMETERS -
        public string[,] grid { get; }
        public string turn { get; }
        public int col { get; }

        public List<Node> children { get; set; }


        // - PUBLIC METHODS -

        // Main constructor
        public Node(string[,] grid, string turn, int col)
        {
            this.grid = grid;
            this.turn = turn;
            this.col = col;
            this.children = new();
        }

        // Returns a created child node given a column
        public Node CreateNode(int col)
        {
            List<int> validCols = GameUtility.ValidColumns(grid);

            // Checks if column is a valid child
            if (validCols.IndexOf(col) != -1)
            {
                // Get data of child node
                string[,] postMoveGrid = GameUtility.MakeMove(this.grid, this.turn, this.col);
                string childTurn = "";
                if (this.turn == "X") childTurn = "O";
                else if (this.turn == "O") childTurn = "X";
                Node child = new Node(postMoveGrid, childTurn, col);

                // Checks if this node already contains the child
                if (this.children.Contains(child))
                {
                    Console.WriteLine("Node already exists");
                    return null;
                }

                return child;
            }

            return null;
        }

        // - PRIVATE METHODS -

    }
}
