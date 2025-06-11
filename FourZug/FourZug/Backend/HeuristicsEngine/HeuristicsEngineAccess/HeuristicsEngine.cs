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

        public bool isGameEnding(Node node)
        {
            string nodeLastMoveBy = node.nextMoveBy == "X" ? "O" : "X";

            string nodeState = BoardEvaluator.GridStateAsString(node.grid, nodeLastMoveBy);

            if (nodeState == "StillInPlay") return false;
            else return true;
        }

        // Return the board state as a string (Used by API)
        public string GetBoardStateAsString(string[,] grid, string lastMoveBy)
        {
            return BoardEvaluator.GridStateAsString(grid, lastMoveBy);
        }

        // Returns the state of the game and the evlaluation of a node
        public (string nodeGameState, short nodeEval) NodeSummary(Node node)
        {
            // If next move is by X, then last was by O. Same for O to X
            string nodeLastMoveBy = node.nextMoveBy == "X" ? "O" : "X";

            string nodeState = BoardEvaluator.GridStateAsString(node.grid, nodeLastMoveBy);

            short nodeEval = BoardEvaluator.EvaluateNodeUsingState(node, nodeState, nodeLastMoveBy);

            return (nodeState, nodeEval);
        }
    }
}
