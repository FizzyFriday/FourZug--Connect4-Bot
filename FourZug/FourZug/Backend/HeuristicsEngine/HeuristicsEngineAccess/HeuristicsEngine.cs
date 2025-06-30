using FourZug.Backend.DTOs;
using FourZug.Backend.HeuristicsEngine.HeuristicsEngineProcessors;
using FourZug.Backend.UtilityEngine.UtilityEngineAccess;


namespace FourZug.Backend.HeuristicsEngine.HeuristicsEngineAccess
{
    // The implemented interface of the component

    internal class HeuristicsEngine : IHeuristicsEngine
    {
        // Calls component scripts to load their references
        public void InitComponentReferences(IUtilityEngine utilityEngine)
        {
            BoardEvaluator.LoadReferences(utilityEngine);
        }

        // Returns the evaluation of a node
        public short NodeEval(Node node)
        {
            return BoardEvaluator.EvaluateNode(node);
        }

        // Returns if game ends and the evaluation of a node
        public (bool endsGame, short nodeEval) NodeSummary(Node node)
        {
            // If next move is by X, then last was by O. Same for O to X
            string nodeLastMoveBy = node.nextMoveBy == "X" ? "O" : "X";

            char nodeWinner = BoardEvaluator.GetBoardWinner(node.grid, nodeLastMoveBy, node.lastColMove);

            short nodeEval = BoardEvaluator.EvaluateNodeUsingWinner(node, nodeWinner, nodeLastMoveBy);

            if (nodeWinner != '?') return (true, nodeEval);
            else return (false, nodeEval);
        }

        // Return the game winner (Used by API)
        public char BoardWinner(string[,] grid, string lastMoveBy, int lastColMove)
        {
            return BoardEvaluator.GetBoardWinner(grid, lastMoveBy, lastColMove);
        }

        // Gets total placement eval using all slots
        public static short BoardPlacementEval(string[,] grid)
        {
            short posEval = 0;

            for (int col = 0; col < grid.GetLength(0); col++)
            {
                for (int row = 0; row < grid.GetLength(1); row++)
                {
                    string containedPiece = grid[col, row];
                    if (containedPiece == " ") continue;
                    posEval += BoardEvaluator.PlacementHeuristic(col, row, containedPiece);
                }
            }

            return posEval;
        }
    }
}
