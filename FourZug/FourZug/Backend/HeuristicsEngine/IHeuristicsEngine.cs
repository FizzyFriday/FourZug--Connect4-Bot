using FourZug.Backend.DTOs;
using FourZug.Backend.UtilityEngineAccess;

namespace FourZug.Backend.HeuristicsEngineAccess
{
    // The interface blueprint of the component

    public interface IHeuristicsEngine
    {
        // Creates and saves interface references of component
        void InitComponentReferences(IUtilityEngine utilityEngine);

        // Returns if the game ends or not, and its evaluation
        (bool endsGame, short nodeEval) NodeSummary(Node node);

        // Return the board state as a string (Used by API)
        char BoardWinner(char[,] grid, char lastMoveBy, int lastColMove);

        // Returns eval of entire board
        short EvalPiecePlacements(char[,] grid);

        // Returns eval to adjust by for new move
        sbyte EvalPlacement(int col, int row, char containedPiece);
    }
}
