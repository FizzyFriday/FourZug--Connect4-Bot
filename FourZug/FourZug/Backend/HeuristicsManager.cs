namespace FourZug.Backend
{
    // Handles heuristics of a move
    internal static class HeuristicsManager
    {
        // - PARAMETERS -


        // - PUBLIC METHODS -
        public static int GetHeuristics(Node node)
        {
            string result = GameState(node.grid, node.turn);

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
        // Losses would have returned a Win for the parent node already (this node wouldnt exist then)
        private static string GameState(string[,] grid, string nodeTurn)
        {
            List<int[]> ownedPieces = new();

            // Check every piece player owns in grid
            for (int col = 0; col < grid.GetLength(0); col++)
            {
                for (int row = 0; row < grid.GetLength(1); row++)
                {
                    if (grid[col, row] == nodeTurn)
                    {
                        // Saves the owned piece
                        int[] pos = [col, row];
                        ownedPieces.Add(pos); 
                    }
                }
            }

            // Check each 4D direction for a connect 4
            foreach (int[] piecePos in ownedPieces)
            {
                int topIndex = grid.GetLength(1) - 1;
                int rightmostIndex = grid.GetLength(0) - 1;

                // Check vertical (up)

                // Check diagonal (NE)

                // Check horizontal (right)

                // Check diagonal (SE)
            }

            
;
            return string.Empty;
        }

        // Returns the position score of the 2 players
        // Returns points of maximizer take points of minimizer
        private static int PositionHeuristic()
        {
            return -1;
        }

    }
}
