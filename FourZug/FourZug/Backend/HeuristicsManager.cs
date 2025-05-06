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

        // Returns Win (for node not current player), Draw or StillInPlay
        // Losses would have returned a Win for the parent node already (this node wouldnt exist then)
        public static string GameState(string[,] grid, string nodeTurn)
        {
            // Checks a given direction and position if a connect 4 is made
            Func<string, int[], int[], bool> CheckIfConnect4 = (nodeTurn, basePos, grad) =>
            {
                // Checks if the end of the potential connect 4 will go out of array
                if (basePos[0] + (3 * grad[0]) >= grid.GetLength(0)) return false;
                if (basePos[1] + (3 * grad[1]) >= grid.GetLength(1)) return false;

                // Check the 3 other spots using provided direction gradient
                for (int dist = 1; dist <= 3; dist++)
                {
                    // Gets new position using provided direction gradient
                    int newCol = basePos[0] + (dist * grad[0]);
                    int newRow = basePos[1] + (dist * grad[1]);

                    // If piece doesnt match, no connect 4 made
                    if (grid[newCol, newRow] != nodeTurn)
                    {
                        return false;
                    }
                }

                return true;
            };

            // Check each 4D direction for a connect 4, provided a positon
            Func<int, int, bool> CheckDirections = (col, row) =>
            {
                int[] piecePos = [col, row];

                // Check vertical (up)
                int[] grad = [0, 1];
                bool connect4made = CheckIfConnect4(nodeTurn, piecePos, grad);
                if (connect4made) return true;

                // Check diagonal (NE)
                grad = [1, 1];
                connect4made = CheckIfConnect4(nodeTurn, piecePos, grad);
                if (connect4made) return true;

                // Check horizontal (right)
                grad = [1, 0];
                connect4made = CheckIfConnect4(nodeTurn, piecePos, grad);
                if (connect4made) return true;

                // Check diagonal (SE)
                grad = [1, -1];
                connect4made = CheckIfConnect4(nodeTurn, piecePos, grad);
                if (connect4made) return true;

                return false;
            };

            // Check each piece node owns in grid for a connect 4
            for (int col = 0; col < grid.GetLength(0); col++)
            {
                for (int row = 0; row < grid.GetLength(1); row++)
                {
                    if (grid[col, row] == nodeTurn)
                    {
                        bool isInConnect4 = CheckDirections(col, row);
                        if (isInConnect4) return "Win";
                    }
                }
            }

            // If no player has won and no move left, game is a draw
            if (GameUtility.ValidColumns(grid).Count == 0) return "Draw";

            // If no one has won and it isnt a draw, the game must still be in play
            else return "StillInPlay";

        }


        // - PRIVATE METHODS -


        // Returns the position score of the 2 players
        // Returns points of maximizer take points of minimizer
        private static int PositionHeuristic()
        {
            return -1;
        }

    }
}
