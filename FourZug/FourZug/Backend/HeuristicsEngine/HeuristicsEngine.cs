using FourZug.Backend.DTOs;


namespace FourZug.Backend.HeuristicsEngine
{
    // The implemented interface of the component

    public class HeuristicsEngine : IHeuristicsEngine
    {
        public HeuristicsEngine()
        { 
            // Create the interface references for BoardEvaluator
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
            return BoardEvaluator.BoardStateAsString(grid, lastMoveBy);
        }
    }
}
