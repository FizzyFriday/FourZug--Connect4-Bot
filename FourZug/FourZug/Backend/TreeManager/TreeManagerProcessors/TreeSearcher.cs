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
            if (heuristicsEngine == null) throw new MissingFieldException();
       
            turnNum += 2;

            Node root = new Node(grid, currentTurn, byte.MinValue);

            // Set desired points by turn and set worst possible reward to bestReward
            bool isMaximizing = currentTurn == "X" ? true : false;
            short bestReward = isMaximizing ? short.MinValue : short.MaxValue;



            byte bestCol = 0;
            List<byte>? validColumns = utilityEngine?.GetValidBoardColumns(grid);
            if (validColumns == null) throw new Exception("Board is full. No 'best move'");

            foreach (byte validCol in validColumns)
            {
                // Get current board move option
                Node moveOption = CreateChild(root, validCol);

                // Do a quick depth 1 check for a clear best move. Depth 1 doesnt detect losses
                var nodeSummary = heuristicsEngine.NodeSummary(moveOption);
                if (nodeSummary.endsGame) return validCol;

                // Start search
                short reward = Minimax(moveOption, 1, !isMaximizing);

                // Don't save reward if it isn't a new PB
                if (isMaximizing && reward <= bestReward) continue;
                else if (!isMaximizing && reward >= bestReward) continue;

                bestReward = reward;
                bestCol = validCol;
            }

            return bestCol;
        }


        // Runs the minimax tree searching logic
        private static short Minimax(Node node, int currentDepth, bool isMaximizing)
        {
            if (utilityEngine == null || heuristicsEngine == null) throw new MissingFieldException();

            if (currentDepth > 0)
            {
                // Return value of node ends game or is a leaf
                var nodeSummary = heuristicsEngine.NodeSummary(node);
                if (nodeSummary.endsGame || currentDepth == maxDepth) return nodeSummary.nodeEval;
            }
            
            short bestReward = isMaximizing ? short.MinValue : short.MaxValue;
            List<byte> childCols = utilityEngine.GetValidBoardColumns(node.grid);

            foreach (byte childCol in childCols)
            {
                Node childMoveOption = CreateChild(node, childCol);

                // Get best reward from deeper searches
                short reward = Minimax(childMoveOption, currentDepth + 1, !isMaximizing);

                // If reward is better than already seen
                if (isMaximizing) bestReward = Math.Max(reward, bestReward);
                if (!isMaximizing) bestReward = Math.Min(reward, bestReward);
            }

            return bestReward;
        }

        // Make sure col is valid before calling
        // Returns a created child node given a column
        private static Node CreateChild(Node node, byte col)
        {
            if (utilityEngine == null) throw new MissingFieldException();
            
            // This game board is an option for the node / nextMoveBy player
            string[,] childGrid = utilityEngine.MakeMove(node.grid, node.nextMoveBy, col);

            // If node's next move by X, then for child it would be O. Vise versa for O to X
            string childNextMoveBy = node.nextMoveBy == "X" ? "O" : "X";

            nodesMade++;

            return new Node(childGrid, childNextMoveBy, col);
        }
    }
}
