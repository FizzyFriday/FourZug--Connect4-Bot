using FourZug.Backend.DTOs;


namespace FourZug.Backend.HeuristicsEngine.HeuristicsEngineAccess
{
    // The interface blueprint of the component

    public interface IHeuristicsEngine
    {
        // Creates and saves interface references of component
        void InitComponentReferences();

        // Gets the evaluation of a game board / node
        short GetNodeEval(Node node);

        // Provides evaluation based on game state of board
        short GetNodeStateEval(Node node);

        // Returns string summary of game board
        string GetBoardStateAsString(string[,] grid, string lastMoveBy);

        // Returns string summary of node
        string GetNodeStateAsString(Node node);
    }
}
