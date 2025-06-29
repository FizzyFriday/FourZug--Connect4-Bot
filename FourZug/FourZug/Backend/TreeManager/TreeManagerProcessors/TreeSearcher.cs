using FourZug.Backend.DTOs;
using FourZug.Backend.HeuristicsEngine.HeuristicsEngineAccess;
using FourZug.Backend.UtilityEngine.UtilityEngineAccess;
using System.Windows.Forms.VisualStyles;

/*
 * Has access permission for assemblies:
 *     HeuristicsEngineAccess
 *     UtilityEngineAccess
 */


namespace FourZug.Backend.TreeManager.TreeManagerProcessors
{
    internal static class TreeSearcher
    {
        // - PARAMETERS -
        private static byte maxDepth = 7;
        private static byte turnNum = 0;

        private static int nodesMade = 1; // 1 because of root
        
        private static IHeuristicsEngine? heuristicsEngine;
        private static IUtilityEngine? utilityEngine;


        public static void LoadReferences(IHeuristicsEngine heuEngine, IUtilityEngine utilEngine)
        {
            heuristicsEngine = heuEngine;
            utilityEngine = utilEngine;
        }


        // Manages the Minimax searching, returning best move for grid
        public static byte BestMove(string[,] grid, string currentTurn)
        {
            if (heuristicsEngine == null) throw new Exception();

            // This should be based on pieces in grid, not a set increment
            turnNum += 2;

            // Increases the depth getting later in the game
            if (turnNum >= 12) maxDepth = 8; // 8
            else if (turnNum >= 14) maxDepth = 10; // 10
            else if (turnNum >= 16) maxDepth = 16; // 16
            else if (turnNum >= 18) maxDepth = 30;

            Node root = new Node(grid, currentTurn, byte.MinValue);

            // Set desired points by turn and set worst possible reward to bestReward
            bool isMaximizing = currentTurn == "X" ? true : false;
            short bestReward = isMaximizing ? short.MinValue : short.MaxValue;

            byte bestCol = 0;
            List<byte>? validColumns = utilityEngine?.GetValidBoardColumns(grid);
            if (validColumns.Count() == 0) return 0;

            foreach (byte validCol in validColumns)
            {
                // Get node after move
                Node moveOption = CreateChild(root, validCol);

                // Only allow deepening if node doesnt end game
                var nodeSummary = heuristicsEngine.NodeSummary(moveOption);
                short reward = 0;
                if (nodeSummary.endsGame) reward = nodeSummary.nodeEval;

                // Allow deepening
                else reward = Minimax(moveOption, 1, !isMaximizing);

                // If the move result is better than already seen
                if (reward > bestReward && isMaximizing)
                {
                    bestReward = reward;
                    bestCol = validCol;
                }
                if (reward < bestReward && !isMaximizing)
                {
                    bestReward = reward;
                    bestCol = validCol;
                }
            }

            return bestCol;
        }


        // Runs the minimax tree searching logic
        private static short Minimax(Node node, int currentDepth, bool isMaximizing)
        {
            // Would never actually be true due to API loading all references on load
            if (utilityEngine == null || heuristicsEngine == null) return -1;

            // Leaf node - run heuristics
            if (currentDepth == maxDepth)
            {
                return heuristicsEngine.NodeEval(node);
            }

            // Set best reward to worst possible for player
            short bestReward = isMaximizing ? short.MinValue : short.MaxValue;

            List<byte> childCols = utilityEngine.GetValidBoardColumns(node.grid);
            foreach (byte childCol in childCols)
            {
                // Get node after move
                Node child = CreateChild(node, childCol);

                // If the game ends from this node, return its eval
                var nodeSummary = heuristicsEngine.NodeSummary(child);
                if (nodeSummary.endsGame) return nodeSummary.nodeEval;

                // Get best reward from deeper searches
                short reward = Minimax(child, currentDepth + 1, !isMaximizing);

                // If the reward is better than already seen
                if (isMaximizing) bestReward = Math.Max(reward, bestReward);
                if (!isMaximizing) bestReward = Math.Min(reward, bestReward);
            }

            return bestReward;
        }

        // Make sure col is valid before calling
        // Returns a created child node given a column
        private static Node CreateChild(Node node, byte col)
        {
            if (utilityEngine == null) return new Node();
            
            // This game board is an option for the node / nextMoveBy player
            string[,] childGrid = utilityEngine.MakeMove(node.grid, node.nextMoveBy, col);

            // If node's next move by X, then for child it would be O. Vise versa for O to X
            string childNextMoveBy = node.nextMoveBy == "X" ? "O" : "X";

            nodesMade++;

            return new Node(childGrid, childNextMoveBy, col);
        }
    }
}
