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
        public static string GameState(string[,] grid, string nodeTurn)
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
                if (piecePos[0] + 3 <= rightmostIndex && piecePos[1] + 3 <= topIndex)
                {
                    Console.WriteLine($"{piecePos[0]}, {piecePos[1]}");

                    int[] grad = [1, 1];
                    bool connect4made = CheckIfConnect4(nodeTurn, piecePos, grad);
                    if (connect4made) return "Win";
                }

                // Check horizontal (right)
                if (piecePos[0] + 3 <= rightmostIndex)
                {
                    if (piecePos[0] == 1 && piecePos[1] == 0) Console.WriteLine("A");
                    int[] grad = [1, 0];
                    bool connect4made = CheckIfConnect4(nodeTurn, piecePos, grad);
                    if (connect4made) return "Win";
                }

                // Check diagonal (SE)
                if (piecePos[0] + 3 <= rightmostIndex && piecePos[1] - 3 >= 0)
                {
                    int[] grad = [1, -1];
                    bool connect4made = CheckIfConnect4(nodeTurn, piecePos, grad);
                    if (connect4made) return "Win";
                }
            }

            // If no player has won and no move left, game is a draw
            if (GameUtility.ValidColumns(grid).Count == 0) return "Draw";

            // If no one has won and it isnt a draw, the game must still be in play
            else return "StillInPlay";

        }

        // Returns the position score of the 2 players
        // Returns points of maximizer take points of minimizer
        private static int PositionHeuristic()
        {
            return -1;
        }

    }
}
