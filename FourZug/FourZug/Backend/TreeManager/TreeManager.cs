using FourZug.Backend.HeuristicsEngine;
using FourZug.Backend.UtilityEngine;

namespace FourZug.Backend.TreeManager
{
    // The implemented interface of the component

    public class TreeManager : ITreeManager
    {
        // Call component scripts to create their references
        public void InitComponentReferences()
        {
            TreeSearcher.LoadReferences();
        }

        // Starts the tree search for the best move, and returns result
        public int GetBotBestMove(string[,] grid, string currentTurn)
        {
            int BestCol = TreeSearcher.BestMove(grid, currentTurn);
            return BestCol;
        }
    }
}
