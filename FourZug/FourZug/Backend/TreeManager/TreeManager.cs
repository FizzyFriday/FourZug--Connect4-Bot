﻿using FourZug.Backend.DTOs;
using FourZug.Backend.HeuristicsEngineAccess;
using FourZug.Backend.UtilityEngineAccess;

namespace FourZug.Backend.TreeManagerAccess
{
    // The implemented interface of the component

    public class TreeManager : ITreeManager
    {
        private IHeuristicsEngine? heuristicsEngine;
        private IUtilityEngine? utilityEngine;
        private const byte maxDepth = 7;
        private int nodesMade = 1; // 1 because of root


        // INTERFACE CONTRACTS

        // Call component scripts to create their references
        public void InitComponentReferences(IHeuristicsEngine heuEngine, IUtilityEngine utilEngine)
        {
            heuristicsEngine = heuEngine;
            utilityEngine = utilEngine;
        }

        // Manages the Minimax searching, returning best move for grid
        public int BestMove(char[,] grid, char currentTurn)
        {
            if (heuristicsEngine == null) throw new MissingFieldException();

            Node root = CreateRoot(grid, currentTurn);

            // Set desired points by turn and set worst possible reward to bestReward
            bool isMaximizing = currentTurn == 'X' ? true : false;
            short bestReward = isMaximizing ? short.MinValue : short.MaxValue;

            byte bestCol = 0;
            List<byte>? validColumns = utilityEngine?.GetValidMoves(grid);
            if (validColumns == null) throw new Exception("Board is full. No 'best move'");

            foreach (byte validCol in validColumns)
            {
                // Get current board move option
                Node moveOption = CreateChild(root, validCol);

                // Do a quick depth 1 check for a clear best move. Depth 1 doesnt detect losses
                var nodeSummary = heuristicsEngine.NodeSummary(moveOption);
                if (nodeSummary.endsGame) return validCol;

                // Start search
                short reward = Minimax(moveOption, 1, !isMaximizing);

                // Don't save reward if it isn't a new PB
                if (isMaximizing && reward <= bestReward) continue;
                else if (!isMaximizing && reward >= bestReward) continue;

                bestReward = reward;
                bestCol = validCol;
            }

            return bestCol;
        }



        // PRIVATE HELPER METHODS

        // Runs the minimax tree searching logic
        private short Minimax(Node node, int currentDepth, bool isMaximizing)
        {
            if (utilityEngine == null || heuristicsEngine == null) throw new MissingFieldException();

            if (currentDepth > 0)
            {
                // Return value of node ends game or is a leaf
                var nodeSummary = heuristicsEngine.NodeSummary(node);
                if (nodeSummary.endsGame || currentDepth == maxDepth) return nodeSummary.nodeEval;
            }

            short bestReward = isMaximizing ? short.MinValue : short.MaxValue;
            List<byte> childCols = utilityEngine.GetValidMoves(node.grid);

            foreach (byte childCol in childCols)
            {
                Node childMoveOption = CreateChild(node, childCol);

                // Get best reward from deeper searches
                short reward = Minimax(childMoveOption, currentDepth + 1, !isMaximizing);

                // If reward is better than already seen
                if (isMaximizing) bestReward = Math.Max(reward, bestReward);
                if (!isMaximizing) bestReward = Math.Min(reward, bestReward);
            }

            return bestReward;
        }

        // Make sure col is valid before calling
        // Returns a created child node given a column
        private Node CreateChild(Node node, byte colMove)
        {
            if (utilityEngine == null || heuristicsEngine == null) throw new MissingFieldException();

            // Calculate new data for child
            int moveRow = node.availableColRows[colMove];
            short newPlacementEval = (short)(node.placementEval + heuristicsEngine.EvalPlacement(colMove, moveRow, node.nextMoveBy));
            char[,] childGrid = utilityEngine.MakeMove(node.grid, node.nextMoveBy, colMove, node.availableColRows[colMove]);
            char childNextMoveBy = node.nextMoveBy == 'X' ? 'O' : 'X';
            int[] newAvailability = AdjustAvailableRows(colMove, node.availableColRows);

            nodesMade++;

            Node child = new Node(childGrid, childNextMoveBy, newAvailability, (sbyte)colMove);
            child.placementEval = newPlacementEval;

            return child;
        }

        // Handles creation of a root node
        private Node CreateRoot(char[,] grid, char currentTurn)
        {
            if (utilityEngine == null || heuristicsEngine == null) throw new MissingFieldException();

            // Get the next height a piece falls into for each col
            int[] availableRows = utilityEngine.GetAvailableRows(grid);

            Node root = new Node(grid, currentTurn, availableRows, -1);
            root.placementEval = heuristicsEngine.EvalPiecePlacements(root.grid);
            return root;
        }

        private int[] AdjustAvailableRows(int col, int[] rowAvailability)
        {
            int[] newAvailability = (int[])rowAvailability.Clone();
            newAvailability[col]++;
            if (rowAvailability[col] == 6) newAvailability[col] = -1;
            return newAvailability;
        }
    }
}
