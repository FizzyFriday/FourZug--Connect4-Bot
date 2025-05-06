namespace FourZug.Backend
{
    // Handles heuristics of a move
    internal static class HeuristicsManager
    {
        // - PARAMETERS -


        // - PUBLIC METHODS -
        public static int GetHeuristics(Node node)
        {
            string result = GameState(node.grid);

            // Returns points if the game ends
            // If the game is a draw, this is bad for either side, hence the large loss
            if (node.turn == "X")
            {
                if (result == "Win") return 1000;
                if (result == "Draw") return -500;
            }
            if (node.turn == "O")
            {
                if (result == "Win") return -1000;
                if (result == "Draw") return 500;
            }

            int pHeuristic = PositionHeuristic();

            return pHeuristic;
        }


        // - PRIVATE METHODS -
        // Returns Win (for node not current player), Draw or StillInPlay
        private static string GameState(string[,] grid)
        {
            return string.Empty;
        }

        // Returns the position score of the 2 players
        // Adds the points of maximizer and takes points of minimizer
        private static int PositionHeuristic()
        {
            return -1;
        }

    }
}
