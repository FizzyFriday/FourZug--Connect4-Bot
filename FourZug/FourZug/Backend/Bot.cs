namespace FourZug.Backend
{
    // Handles tree searching
    // Handles tree results
    internal static class Bot
    {
        // - PARAMETERS -
        private static int maxDepth = 3;
        private static bool maximizing;


        // - PUBLIC METHODS -
        // Manages the Minimax searching and returns final best move results
        public static int BestMove(string[,] grid, string turn)
        {
            if (turn == "X") maximizing = true;
            else if (turn == "O") maximizing = false;

            // Creates the root as a list of nodes as Root has no implementation
            List<Node> root = new();

            // Gets all valid columns and creates children
            List<int> validColumns = GameUtility.ValidColumns(grid);
            foreach (int validCol in validColumns)
            {
                Node directChild = new Node(grid, turn, validCol);
                root.Add(directChild);
            }

            // Run minimax on each child
            // X goes for max reward, so set to minimum at the start
            int highestReward = int.MinValue;
            if (!maximizing) highestReward = int.MaxValue;

            // Evaluate each move
            int bestCol = -1;
            foreach (Node directMove in root)
            {
                int reward = Minimax(directMove, 1, !maximizing);

                // If the move result is better than already seen
                if (reward > highestReward)
                {
                    highestReward = reward;
                    bestCol = directMove.col;
                }
            }

            return bestCol;
        }


        // - PRIVATE METHODS -

        // Runs the minimax tree searching logic
        private static int Minimax(Node node, int currentDepth, bool maximizing)
        {
            // Leaf node - run heuristics
            if (currentDepth == maxDepth)
            {
                return -1;
            }

            // Non leaf - deepen and send back results
            // Make a dictionary?
            List<int> childCols = GameUtility.ValidColumns(node.grid);
            List<int> rewards = new();

            foreach (int childCol in childCols)
            {
                // Lazy expands child onto tree
                Node child = node.CreateNode(childCol);
                node.children.Add(child);

                // Deepens search and adds onto list
                int reward = Minimax(node, currentDepth + 1, !maximizing);
                rewards.Add(reward);
            }

            // return best result
            return -1;
        }
    }
}
