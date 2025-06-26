using FourZug.Backend.DTOs;
using FourZug.Backend.HeuristicsEngine.HeuristicsEngineProcessors;
using FourZug.Backend.UtilityEngine.UtilityEngineAccess;


namespace FourZug.Backend.HeuristicsEngine.HeuristicsEngineAccess
{
    // The implemented interface of the component

    internal class HeuristicsEngine : IHeuristicsEngine
    {
        private IUtilityEngine? utilityEngine;

        // Calls component scripts to load their references
        public void InitComponentReferences(IUtilityEngine utilityEngine)
        {
            this.utilityEngine = utilityEngine;
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
            string nodeLastMoveBy = node.nextBitsMove == "10" ? "01" : "10";

            char nodeWinner = BoardEvaluator.BoardWinnerAsChar(node.stringBits, nodeLastMoveBy, node.lastIDMove);

            short nodeEval = BoardEvaluator.EvaluateNodeUsingWinner(node, nodeWinner, nodeLastMoveBy);

            if (nodeWinner != '?') return (true, nodeEval);
            else return (false, nodeEval);
        }

        // Return the game winner (Used by API)
        public char BoardWinner(string[,] grid, string lastMoveBy, int lastColMove)
        {
            if (utilityEngine == null) return ' ';

            string[] stringBits = utilityEngine.Flatten2DGrid(grid);

            byte pieceID = (byte)(utilityEngine.NextEmptyIDInCol(stringBits, lastColMove) - 1);

            string lastBitsMove = utilityEngine.StringToStringBits(lastMoveBy);

            return BoardEvaluator.BoardWinnerAsChar(stringBits, lastBitsMove, pieceID);
        }
    }
}
