using FourZug.Backend.DTOs;
using FourZug.Backend.HeuristicsEngine.HeuristicsEngineProcessors;


namespace FourZug.Backend.HeuristicsEngine.HeuristicsEngineAccess
{
    // The implemented interface of the component

    internal class HeuristicsEngine : IHeuristicsEngine
    {
        // Calls component scripts to load their references
        public void InitComponentReferences()
        {
            BoardEvaluator.LoadReferences();
        }

        public short GetNodeEval(Node node)
        {
            // Gets the evaluation of board state. If != 0, then the game ended
            short sHeuristic = BoardEvaluator.BoardStateAsEval(node);
            if (sHeuristic != 0) return sHeuristic;

            // Return the value based on piece positioning
            short pHeuristic = BoardEvaluator.PositionEval(node.grid);
            return pHeuristic;
        }

        // Return the board state as an evaluation
        public short GetNodeStateEval(Node node)
        {
            return BoardEvaluator.BoardStateAsEval(node);
        }

        // Return the board state as a string
        public string GetBoardStateAsString(string[,] grid, string lastMoveBy)
        {
            return BoardEvaluator.GridStateAsString(grid, lastMoveBy);
        }

        // Return the node state as a string
        public string GetNodeStateAsString(Node node)
        {
            // If next move is by X, then last was by O. Same for O to X
            string nodeLastMoveBy = node.nextMoveBy == "X" ? "O" : "X";

            return BoardEvaluator.GridStateAsString(node.grid, nodeLastMoveBy);
        }
    }
}
