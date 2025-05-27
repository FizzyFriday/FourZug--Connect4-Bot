using FourZug.Backend.DTOs;
using FourZug.Backend.HeuristicsEngine;
using FourZug.Backend.UtilityEngine;

namespace FourZug.Backend.TreeManager
{
    // The actual processor of the component


    // Bot sometimes misses a Win in 1
    // Bot sometimes will sacrifice multiple traps, although this may be sometimes strategically good?
    // Add a connection heuristic (connect 2, connect 3) that contributes a small amount?

    // Originally Bot.cs
    internal static class TreeSearcher
    {
        // - PARAMETERS -
        private static byte maxDepth = 7;
        private static byte turnNum = 0;


        // - PUBLIC METHODS -
        // Manages the Minimax searching and returns final best move results
        public static byte BestMove(string[,] grid, string currentTurn)
        {
            turnNum += 2;

            // Increases the depth getting later in the game
            if (turnNum >= 12) maxDepth = 8;
            else if (turnNum >= 14) maxDepth = 10;
            else if (turnNum >= 16) maxDepth = 16;
            else if (turnNum >= 18) maxDepth = byte.MaxValue;

            Node root = new Node(grid, currentTurn, byte.MinValue);

            // Set desired points by turn and set worst possible reward to bestReward
            bool maximizing = currentTurn == "X" ? true : false;
            short bestReward = maximizing ? short.MinValue : short.MaxValue;

            // Evaluate each move
            // bestCol will always be positive and 0-6, except for the -1 default case
            sbyte bestCol = -1;
            List<int> validColumns = IUtilityEngine.ValidColumns(grid);

            foreach (byte validCol in validColumns)
            {
                // Get node after move
                Node child = CreateChild(root, validCol);

                // Begin the search
                short reward = Minimax(child, 1, !maximizing);

                // If the move result is better than already seen
                if (reward > bestReward && maximizing)
                {
                    bestReward = reward;
                    bestCol = (sbyte)child.lastColMove;
                }
                if (reward < bestReward && !maximizing)
                {
                    bestReward = reward;
                    bestCol = (sbyte)child.lastColMove;
                }
            }

            return (byte)bestCol;
        }


        // - PRIVATE METHODS -

        // Runs the minimax tree searching logic
        private static short Minimax(Node node, int currentDepth, bool maximizing)
        {
            // Leaf node - run heuristics
            if (currentDepth == maxDepth)
            {
                return HeuristicsEngine.GetEvaluation(node);
            }

            // Set best reward to worst possible for player
            short bestReward = maximizing ? short.MinValue : short.MaxValue;

            List<int> childCols = UtilityEngine.ValidColumns(node.grid);
            foreach (byte childCol in childCols)
            {
                // Get node after move
                Node child = CreateChild(node, childCol);

                // The player to last play move doesn't matter if its just checking if the game ended or not
                short statePoints = HeuristicsEngine.GetStateHeuristic(child);

                // If true, game has ended, and this node has statePoints value
                if (statePoints != 0) return statePoints;

                // Get best reward from deeper searches
                short reward = Minimax(child, currentDepth + 1, !maximizing);

                // If the reward is better than already seen
                if (maximizing) bestReward = Math.Max(reward, bestReward);
                if (!maximizing) bestReward = Math.Min(reward, bestReward);
            }

            return bestReward;
        }

        // Make sure col is valid before calling
        // Returns a created child node given a column
        private static Node CreateChild(Node node, byte col)
        {
            // This game board is an option for the node / nextMoveBy player
            string[,] childGrid = UtilityEngine.MakeMove(node.grid, node.nextMoveBy, col);

            // If node's next move by X, then for child it would be O. Vise versa for O to X
            string childNextMoveBy = node.nextMoveBy == "X" ? "O" : "X";

            return new Node(childGrid, childNextMoveBy, col);
        }
    }
}
