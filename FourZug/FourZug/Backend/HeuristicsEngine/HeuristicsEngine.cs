using FourZug.Backend.DTOs;
using FourZug.Backend.UtilityEngineAccess;
using System.Reflection.Metadata.Ecma335;

namespace FourZug.Backend.HeuristicsEngineAccess
{
    // The implemented interface of the component

    internal class HeuristicsEngine : IHeuristicsEngine
    {
        private IUtilityEngine? utilEngine;


        // INTERFACE CONTRACTS

        // Calls component scripts to load their references
        public void InitComponentReferences(IUtilityEngine utilEngine)
        {
            this.utilEngine = utilEngine;
        }

        // Returns if game ends and the evaluation of a node
        public (bool endsGame, short nodeEval) NodeSummary(Node node)
        {
            // If next move is by X, then last was by O. Same for O to X
            char nodeLastMoveBy = node.nextMoveBy == 'X' ? 'O' : 'X';

            char nodeWinner = BoardWinner(node.grid, nodeLastMoveBy, node.lastColMove);

            short nodeEval = EvalNodeUsingWinner(node, nodeWinner, nodeLastMoveBy);

            if (nodeWinner != '?') return (true, nodeEval);
            else return (false, nodeEval);
        }

        // Return the game winner
        public char BoardWinner(char[,] grid, char lastMoveBy, int lastColMove)
        {
            if (utilEngine == null) throw new MissingFieldException();


            // Get the row the last piece fell into
            int lastRowMove = -1;
            for (int row = grid.GetLength(1) - 1; row >= 0; row--)
            {
                if (grid[lastColMove, row] != ' ')
                {
                    lastRowMove = row;
                    break;
                }
            }

            int piecePositionID = utilEngine.RowColumnToID(lastRowMove, lastColMove);

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
                    int pointedID = piecePositionID + idDist * idChangeScales[direc];

                    if (isValidID(piecePositionID, pointedID))
                    {
                        char pieceAtPosition = utilEngine.PieceAtPositionID(grid, pointedID);
                        if (pieceAtPosition == lastMoveBy) connectedPieces++;
                        else connectedPieces = 0;

                        // Return that the last move owner is winner
                        if (connectedPieces == 4) return lastMoveBy;
                    }
                    else connectedPieces = 0;
                }
            }

            // If no player has won and no move left, game is a draw
            if (utilEngine.GetValidMoves(grid).Count == 0) return 'D';

            // If no one has won and it isnt a draw, the game must still be in play
            else return '?';
        }

        // Returns placement eval of entire baord
        public short EvalPiecePlacements(char[,] grid)
        {
            short pointBalance = 0;
            for (int col = 0; col < grid.GetLength(0); col++)
            {
                for (int row = 0; row < grid.GetLength(1); row++)
                {
                    char containedPiece = grid[col, row];
                    if (containedPiece == ' ') continue;
                    pointBalance += EvalPlacement(col, row, containedPiece);
                }
            }

            return pointBalance;
        }

        // Returns piece placement value gain of a slot
        public sbyte EvalPlacement(int col, int row, char containedPiece)
        {
            // Represents the points gained from positions taken
            // Viewing from side would correlate visually to game board and help understand array access
            sbyte[,] pointTable = new sbyte[7, 6]
            {
                { 3, 4, 5, 5, 4, 3},
                { 4, 6, 8, 8, 6, 4 },
                { 5, 8, 11, 11, 8, 5 },
                { 7, 10, 13, 13, 10, 7 },
                { 5, 8, 11, 11, 8, 5 },
                { 4, 6, 8, 8, 6, 4 },
                { 3, 4, 5, 5, 4, 3}
            };

            if (containedPiece == 'X') return pointTable[col, row];
            else return (sbyte)(pointTable[col, row] * -1);

        }


        // PRIVATE HELPER METHODS

        // Checks an ID of a grid is valid in a connect 4 chain
        private bool isValidID(int positionID, int pointedID)
        {
            // Handles invalid IDs
            if (pointedID < 0 || pointedID > 41) return false;

            int posCol = positionID / 6, posRow = positionID % 6;
            int pointCol = pointedID / 6, pointRow = pointedID % 6;
            int colDist = Math.Abs(posCol - pointCol), rowDist = Math.Abs(posRow - pointRow);

            // Checks for diagonal 1 to 1
            if (colDist != 0 && rowDist != 0)
            {
                if (colDist != rowDist) return false;
            }

            return true;
        }

        private short EvalNodeUsingWinner(Node node, char nodeWinner, char nodeLastMoveBy)
        {
            const short winGain = 1000, drawGain = -500;
            if (nodeWinner == 'X') return winGain;
            if (nodeWinner == 'O') return -1 * winGain;

            if (nodeWinner == 'D')
            {
                if (nodeLastMoveBy == 'X') return -1 * drawGain;
                else return drawGain;
            }

            // (If nodeWinner == '?')
            else return node.placementEval;
        }        
    }
}
