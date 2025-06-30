using FourZug.Backend.DTOs;
using FourZug.Backend.UtilityEngine.UtilityEngineAccess;


namespace FourZug.Backend.HeuristicsEngine.HeuristicsEngineAccess
{
    // The interface blueprint of the component

    public interface IHeuristicsEngine
    {
        // Creates and saves interface references of component
        void InitComponentReferences(IUtilityEngine utilityEngine);

        // Provides evaluation based on game state of board
        short NodeEval(Node node);

        // Returns if the game ends or not, and its evaluation
        (bool endsGame, short nodeEval) NodeSummary(Node node);

        // Return the board state as a string (Used by API)
        char BoardWinner(string[,] grid, string lastMoveBy, int lastColMove);

        short EvaluateBoardPositions(string[,] grid);
    }
}
