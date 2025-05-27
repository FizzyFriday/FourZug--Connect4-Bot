namespace FourZug.Backend.HeuristicsEngine
{
    // The interface blueprint of the component

    static class IHeuristicsEngine
    {
        // Gets the heuristis of a game board / node
        public static short GetNodeEval(Node node)
        {
            int sHeuristic = HeuristicsEngine.GetStateHeuristic(node);
            if (sHeuristic != 0) return (short)sHeuristic;

            int pHeuristic = PositionHeuristic(node.grid);
            return (short)pHeuristic;
        }

        // Provides simple scoring return from GameState method
        public static short GetNodeStateEval(Node node)
        {
            string lastMoveBy = "O";
            if (node.nextMoveBy == "O") lastMoveBy = "X";

            string result = GameState(node.grid, lastMoveBy);

            // Returns points if the game ends
            // If the game is a draw, this is bad for either side, hence the large loss
            if (lastMoveBy == "X")
            {
                if (result == "Win") return 1000;
                if (result == "Draw") return -500;
            }
            if (lastMoveBy == "O")
            {
                if (result == "Win") return -1000;
                if (result == "Draw") return 500;
            }

            // Return 0 if the game hasnt ended
            return 0;
        }

        // Returns Win, Draw or StillInPlay. Used by API
        public static string GetGridState(string[,] grid, string lastMoveBy)
        {
            return GameState(grid, lastMoveBy);
        }
    }
}
