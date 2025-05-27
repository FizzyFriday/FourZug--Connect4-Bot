using FourZug.Backend.DTOs;
using FourZug.Backend.UtilityEngine;
using System.Xml.Linq;


namespace FourZug.Backend.HeuristicsEngine
{
    // The actual processor of the component

    internal static class BoardEvaluator
    {

        // Returns Win (for node not current player), Draw or StillInPlay
        // Losses would have returned a Win for the parent node already (this node wouldnt exist then)

        /*
         * Since the function currently runs through the whole board every time this is ran,
         * a lot of processing would be repeated/wasted. Why check a piece that hasn't had any new pieces
         * connected to it and didnt end the game previously? Instead, when a move is made, 
         * only the newly placed piece would have any impact on the game.
         * 
         * To rewrite the method and massively improve performance, just 1 piece should be checked (the newest one), 
         * and in all 8 directions. (The node.lastColMove field already in the node can help)
         * 
         * This will cut direction checks from 4n, where n is pieces owned, to 8. It also will reduce loop iterations
         * from 42 to 1. This will in particular boost the performance in higher depths, and high depths in early game
         * 
         * Estimated boost from 550k runs per second to 1.2M excl bottleneck
         */

        public static string BoardStateAsString(string[,] grid, string lastMoveBy)
        {
            // Checks a given direction and position if a connect 4 is made
            Func<string, int[], int[], bool> CheckIfConnect4 = (nodeTurn, basePos, grad) =>
            {
                // Checks if the end of the potential connect 4 will go out of array
                int endCol = basePos[0] + (3 * grad[0]);
                int endRow = basePos[1] + (3 * grad[1]);

                if (endCol >= grid.GetLength(0) || endCol < 0) return false;
                if (endRow >= grid.GetLength(1) || endRow < 0) return false;

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
                bool connect4made = CheckIfConnect4(lastMoveBy, piecePos, grad);
                if (connect4made) return true;

                // Check diagonal (NE)
                grad = [1, 1];
                connect4made = CheckIfConnect4(lastMoveBy, piecePos, grad);
                if (connect4made) return true;

                // Check horizontal (right)
                grad = [1, 0];
                connect4made = CheckIfConnect4(lastMoveBy, piecePos, grad);
                if (connect4made) return true;

                // Check diagonal (SE)
                grad = [1, -1];
                connect4made = CheckIfConnect4(lastMoveBy, piecePos, grad);
                if (connect4made) return true;

                return false;
            };

            // This for loop and checking the lamba functions for EVERY piece is intense
            // Check each piece node owns in grid for a connect 4
            for (int col = 0; col < grid.GetLength(0); col++)
            {
                for (int row = 0; row < grid.GetLength(1); row++)
                {
                    if (grid[col, row] == lastMoveBy)
                    {
                        bool isInConnect4 = CheckDirections(col, row);
                        if (isInConnect4) return "Win";
                    }
                }
            }

            // If no player has won and no move left, game is a draw
            if (UtilityEngine.ValidColumns(grid).Count == 0) return "Draw";

            // If no one has won and it isnt a draw, the game must still be in play
            else return "StillInPlay";
        }

        // Converts the board state as a string into an evaluation
        public static short BoardStateAsEval(Node node)
        {
            string lastMoveBy = "O";
            if (node.nextMoveBy == "O") lastMoveBy = "X";

            string result = BoardStateAsString(node.grid, lastMoveBy);

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

        // Returns the position score of the 2 players
        // Returns points of maximizer take points of minimizer
        public static short PositionEval(string[,] grid)
        {
            // Represents the points gained from positions taken
            // Viewing from side would correlate visually to game board and help understand array access
            int[,] pointTable = new int[7, 6]
            {
                { 3, 4, 5, 5, 4, 3},
                { 4, 6, 8, 8, 6, 4 },
                { 5, 8, 11, 11, 8, 5 },
                { 7, 10, 13, 13, 10, 7 },
                { 5, 8, 11, 11, 8, 5 },
                { 4, 6, 8, 8, 6, 4 },
                { 3, 4, 5, 5, 4, 3}
            };

            // Get the points gained for each player on each position
            int pointBalance = 0;
            for (int col = 0; col < grid.GetLength(0); col++)
            {
                for (int row = 0; row < grid.GetLength(1); row++)
                {
                    // Add on points for the position owning player
                    string containedPiece = grid[col, row];
                    int positionPoints = pointTable[col, row];

                    if (containedPiece == "X") pointBalance += positionPoints;
                    else if (containedPiece == "O")
                    {
                        pointBalance -= positionPoints;
                    }
                }
            }
            return pointBalance;
        }
    }
}
