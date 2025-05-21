using FourZug.Backend.DTOs;

namespace FourZug.Backend
{
    // Handles Tree related processing


    // Bot sometimes misses a Win in 1
    // Bot sometimes will sacrifice multiple traps, although this may be sometimes strategically good?
    // Add a connection heuristic (connect 2, connect 3) that contributes a small amount?

    // Originally Bot.cs
    internal static class TreeManager
    {
        // - PARAMETERS -
        private static int maxDepth = 7;


        // - PUBLIC METHODS -
        // Manages the Minimax searching and returns final best move results
        public static int BestMove(string[,] grid, string currentTurn)
        {
            Node root = new Node(grid, currentTurn, -1);

            // Set desired points by turn and set worst possible reward to bestReward
            bool maximizing = (currentTurn == "X") ? true : false;
            int bestReward = (maximizing) ? int.MinValue : int.MaxValue;

            // Evaluate each move
            int bestCol = -1;
            List<int> validColumns = UtilityEngine.ValidColumns(grid);

            foreach (int validCol in validColumns)
            {
                // Get node after move
                Node child = CreateChild(root, validCol);

                // Begin the search
                int reward = Minimax(child, 1, !maximizing);

                // If the move result is better than already seen
                if (reward > bestReward && maximizing)
                {
                    bestReward = reward;
                    bestCol = child.lastColMove;
                }
                if (reward < bestReward && !maximizing)
                {
                    bestReward = reward;
                    bestCol = child.lastColMove;
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
                return HeuristicsEngine.GetEvaluation(node);
            }

            // Set best reward to worst possible for player
            int bestReward = (maximizing) ? int.MinValue : int.MaxValue;

            List<int> childCols = UtilityEngine.ValidColumns(node.grid);
            foreach (int childCol in childCols)
            {
                // Get node after move
                Node child = CreateChild(node, childCol);

                // The player to last play move doesn't matter if its just checking if the game ended or not
                int statePoints = HeuristicsEngine.GetStateHeuristic(child);

                // If true, game has ended, and this node has statePoints value
                if (statePoints != 0) return statePoints;

                // Get best reward from deeper searches
                int reward = Minimax(child, currentDepth + 1, !maximizing);

                // If the reward is better than already seen
                if (maximizing) bestReward = Math.Max(reward, bestReward);
                if (!maximizing) bestReward = Math.Min(reward, bestReward);
            }

            return bestReward;
        }

        // Make sure col is valid before calling
        // Returns a created child node given a column
        private static Node CreateChild(Node node, int col)
        {
            // This game board is an option for the node / nextMoveBy player
            string[,] childGrid = UtilityEngine.MakeMove(node.grid, node.nextMoveBy, col);

            // If node's next move by X, then for child it would be O. Vise versa for O to X
            string childNextMoveBy = (node.nextMoveBy == "X") ? "O" : "X";

            return new Node(childGrid, childNextMoveBy, col);
        }
    }
}
