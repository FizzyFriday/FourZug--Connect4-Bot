namespace FourZug.Backend
{
    // Contains necessary information for nodes
    // Contains helpful methods concerning tree usage
    internal class Node
    {
        // - PARAMETERS -
        // Instead of a node representing board and turn before move
        // and requiring code to get board after, node now represents after the move

        // Represents grid AFTER the move
        public string[,] grid { get; }


        // Represents who makes the next move (who's turn it is)
        public string nextMoveBy { get;  }

        // Represents the last column move that made this board
        public int lastMove { get; }


        public List<Node> children { get; set; }


        // - PUBLIC METHODS -

        // Main constructor
        public Node(string[,] grid, string currentTurn, int lastMove)
        {
            this.grid = grid;
            this.children = new();
            this.nextMoveBy = currentTurn;
            this.lastMove = lastMove;
        }

        // Returns a created child node given a column
        public Node? AddChildToTree(int col)
        {
            List<int> validCols = GameUtility.ValidColumns(grid);

            // Checks if column is a valid child
            if (validCols.IndexOf(col) != -1)
            {
                // Get the grid of child node
                // This game board is an option for the node / nextMoveBy player
                string[,] childGrid = GameUtility.MakeMove(this.grid, this.nextMoveBy, col);

                string childNextMoveBy = "O";
                if (this.nextMoveBy == "O") childNextMoveBy = "X";

                Node child = new Node(childGrid, childNextMoveBy, col);

                // Checks if this node already contains the child
                if (this.children.Contains(child))
                {
                    Console.WriteLine("Node already exists in tree");
                    return null;
                }

                this.children.Add(child);
                return child;
            }

            return null;
        }

        // - PRIVATE METHODS -

    }
}
