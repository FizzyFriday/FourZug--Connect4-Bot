namespace FourZug.Backend
{
    // Handles tree searching
    // Handles tree results
    internal static class Bot
    {
        // - PARAMETERS -
        private static int maxDepth = 2;
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
            int bestReward = int.MinValue;
            if (!maximizing) bestReward = int.MaxValue;

            // Evaluate each move
            int bestCol = -1;
            // Headers of the debugging display
            Console.WriteLine("R   X   O");
            foreach (Node directMove in root)
            {
                int reward = Minimax(directMove, 1, maximizing);
                // Display the new bestmove value (root header)
                Console.WriteLine(reward);

                // If the move result is better than already seen
                if (reward > bestReward && maximizing)
                {
                    bestReward = reward;
                    bestCol = directMove.col;
                }
                if (reward < bestReward && !maximizing)
                {
                    bestReward = reward;
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
                int reward = HeuristicsManager.GetHeuristics(node);
                // Displays heuristic value in debug display
                string padding = new string(' ', currentDepth*4);
                Console.WriteLine($"{padding}{reward}");

                return reward;
            }

            // Non leaf - deepen and send back results
            List<int> childCols = GameUtility.ValidColumns(node.grid);

            int bestReward = int.MinValue;
            if (node.turn == "O") bestReward = int.MaxValue;

            foreach (int childCol in childCols)
            {
                // Lazy expands child onto tree
                Node child = node.CreateNode(childCol);
                node.children.Add(child);

                int reward = Minimax(child, currentDepth + 1, maximizing);

                // Displays the nodes value after child nodes
                string padding = new string(' ', currentDepth*4);
                Console.WriteLine($"{padding}{reward}");

                // If the reward is better than already seen
                if (maximizing) bestReward = Math.Max(reward, bestReward);
                if (!maximizing) bestReward = Math.Min(reward, bestReward);
            }

            return bestReward;

        }
    }
}
