namespace FourZug.Backend
{
    // Handles tree searching
    // Handles tree results

   
    // Bot sometimes misses a Win in 1
    // Bot sometimes will sacrifice multiple traps, although this may be sometimes strategically good?
    // Add a connection heuristic (connect 2, connect 3) that contributes a small amount?


    internal static class Bot
    {
        // - PARAMETERS -
        public static int maxDepth = 7;


        // - PUBLIC METHODS -
        // Manages the Minimax searching and returns final best move results
        public static int BestMove(string[,] grid, string currentTurn)
        {
            Node root = new Node(grid, currentTurn, -1);

            // If turn == "X", maximizing = true. Else, maximizing = false
            bool maximizing = (currentTurn == "X") ? true : false;

            // Set best reward to worst possible for player
            // If maximizing = true, bestReward = -infinity. Else, bestReward = +infinity;
            int bestReward = (maximizing) ? int.MinValue : int.MaxValue;

            // Evaluate each move
            int bestCol = -1;
            List<int> validColumns = GameUtility.ValidColumns(grid);

            foreach (int col in validColumns)
            {
                // Lazy expand child onto tree
                Node? child = root.AddChildToTree(col);
                if (child == null) continue;

                // Begin the search
                int reward = Minimax(child, 1, !maximizing);

                // If the move result is better than already seen
                if (reward > bestReward && maximizing)
                {
                    bestReward = reward;
                    bestCol = child.lastMove;
                }
                if (reward < bestReward && !maximizing)
                {
                    bestReward = reward;
                    bestCol = child.lastMove;
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
                return reward;
            }

            // Non leaf - deepen and send back results
            List<int> childCols = GameUtility.ValidColumns(node.grid);

            // Set best reward to worst possible for player
            int bestReward = (maximizing) ? int.MinValue : int.MaxValue;

            foreach (int childCol in childCols)
            {
                // Lazy expands child onto tree
                Node? child = node.AddChildToTree(childCol);
                if (child == null) continue;

                // The player to last play move doesn't matter if its just checking if the game ended or not
                int statePoints = HeuristicsManager.GetStateHeuristic(child.grid, child.nextMoveBy);

                // If true, game has ended
                if (statePoints != 0) return statePoints;

                // Get best reward from deeper searches
                int reward = Minimax(child, currentDepth + 1, !maximizing);

                // If the reward is better than already seen
                if (maximizing) bestReward = Math.Max(reward, bestReward);
                if (!maximizing) bestReward = Math.Min(reward, bestReward);
            }

            return bestReward;
        }
    }
}
