using FourZug.Backend.DTOs;
using FourZug.Backend.UtilityEngine.UtilityEngineAccess;
using System.Xml.Linq;

/*
 * Has access permission for assemblies:
 *     UtilityEngineAccess
 */

namespace FourZug.Backend.HeuristicsEngine.HeuristicsEngineProcessors
{
    // The actual processor of the component

    internal static class BoardEvaluator
    {
        private static IUtilityEngine? utilityEngine;

        public static void LoadReferences(IUtilityEngine utilEngine)
        {
            utilityEngine = utilEngine;
        }

        public static char BoardWinnerAsChar(string[] stringBits, string lastBitsMove, byte lastMoveID)
        {
            if (utilityEngine == null) return ' ';

            // Determines what to change ID by (+-) for checking each direction for connect 4
            // In order: Vertical, Diagonal (NE / SW), Horizontal, Diagonal (SE / NW)
            int[] idChangeScales = { 1, 7, 6, 5 };

            // Run through each direction, checking for connect 4
            for (int direc = 0; direc < idChangeScales.Length; direc++)
            {
                int connectedPieces = 0;
                // Run through chain
                for (int idDist = -3; idDist <= 3; idDist++)
                {
                    int pointedID = lastMoveID + (idDist * idChangeScales[direc]);

                    if (validConnectionID(lastMoveID, pointedID))
                    {
                        string pieceBits = stringBits[pointedID];
                        if (pieceBits == stringBits[lastMoveID]) connectedPieces++;
                        else connectedPieces = 0;

                        if (connectedPieces == 4)
                        {
                            if (lastBitsMove == "10") return 'X';
                            else return 'O';
                        }
                    }
                    else connectedPieces = 0;
                }
            }

            // If no player has won and no move left, game is a draw
            if (utilityEngine.ValidMoveIDs(stringBits).Count() == 0) return 'D';

            // If no one has won and it isnt a draw, the game must still be in play
            else return '?';
        }

        private static bool validConnectionID(byte positionID, int pointedID)
        {
            // Handles invalid IDs
            if (pointedID < 0 || pointedID > 41) return false;

            var posColRow = utilityEngine.ColRowFromID(positionID);
            var pointColRow = utilityEngine.ColRowFromID((byte)pointedID);

            int colDist = Math.Abs(posColRow.col - pointColRow.col), rowDist = Math.Abs(posColRow.row - pointColRow.row);

            // Checks for diagonal 1 to 1
            if (colDist != 0 && rowDist != 0)
            {
                if (colDist != rowDist) return false;
            }

            return true;
        }

        public static short EvaluateNode(Node node)
        {                   
            string lastBitsMove = node.nextBitsMove == "10" ? "01" : "10";

            char nodeState = BoardWinnerAsChar(node.stringBits, lastBitsMove, node.lastIDMove);

            return EvaluateNodeUsingWinner(node, nodeState, lastBitsMove);
        }

        public static short EvaluateNodeUsingWinner(Node node, char nodeWinner, string lastBitsMove)
        {
            const short winGain = 1000, drawGain = -500;
            if (nodeWinner == 'X') return winGain;
            if (nodeWinner == 'O') return (-1 * winGain);

            if (nodeWinner == 'D')
            {
                if (lastBitsMove == "01") return (-1 * drawGain);
                else return drawGain;
            }

            // (If nodeWinner == '?')
            else return PositionEval(node.stringBits);
        }


        // Returns the position score of the 2 players
        // Returns points of maximizer take points of minimizer
        private static short PositionEval(string[] stringBits)
        {
            if (utilityEngine == null) return 0;

            // Represents the points gained from positions taken
            // Viewing from side would correlate visually to game board and help understand array access
            byte[,] pointTable = new byte[7, 6]
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
            short pointBalance = 0;

            for (byte id = 0; id < 41; id++)
            {
                var colRow = utilityEngine.ColRowFromID(id);
                string pieceBits = stringBits[id];

                if (pieceBits == "10") pointBalance += (short)pointTable[colRow.col, colRow.row];
                else if (pieceBits == "01") pointBalance -= (short)pointTable[colRow.col, colRow.row];
            }

            return pointBalance;
        }
    }
}
