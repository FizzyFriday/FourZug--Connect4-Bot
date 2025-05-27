using FourZug.Backend.DTOs;


namespace FourZug.Backend.HeuristicsEngine
{
    // The implemented interface of the component

    public class HeuristicsEngine : IHeuristicsEngine
    {
        public short GetNodeEval(Node node)
        {
            // Reuse accidentally deleted code

            return -1;
        }

        public short GetNodeStateEval(Node node)
        {
            // Only gets the state evaluation and return this value

            return -1;
        }

        public string GetBoardStateAsString(string[,] grid, string lastMoveBy)
        {
            // Get the game state, and just return the string
            // Same as the GetNodeStateEval logic, but shorter

            return string.Empty;
        }
    }
}
