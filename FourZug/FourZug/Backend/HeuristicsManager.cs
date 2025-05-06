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

            // Run through whole grid and save position of each piece node owns
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


            Func<string, int[], int[], bool> CheckIfConnect4 = (nodeTurn, basePos, grad) =>
            {
                // Gets the column and row of the end of the potential connect 4
                int endCol = basePos[0] + (3 * grad[0]);
                int endRow = basePos[1] + (3 * grad[1]);

                // Check the 4 spots using provided direction gradient
                for (int colInc = 0; colInc <= endCol; colInc++)
                {
                    for (int rowInc = 0; rowInc <= endRow; rowInc++)
                    {
                        // Get coordinates of new position
                        int newCol = basePos[0] + colInc;
                        int newRow = basePos[1] + rowInc;

                        // If piece doesnt match, no connect 4 made
                        if (grid[newCol, newRow] != nodeTurn)
                        {
                            return false;
                        }
                    }
                }

                return true;
            };

            // Check each 4D direction for a connect 4
            foreach (int[] piecePos in ownedPieces)
            {
                int topIndex = grid.GetLength(1) - 1;
                int rightmostIndex = grid.GetLength(0) - 1;

                // Check vertical (up)
                // Check for if out of bounds error would occur
                if (piecePos[1] + 3 <= topIndex)
                {
                    int[] grad = [0, 1];
                    bool connect4made = CheckIfConnect4(nodeTurn, piecePos, grad);
                    if (connect4made) return "Win";
                }

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
