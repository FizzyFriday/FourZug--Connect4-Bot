using FourZug.Backend.HeuristicsEngine.HeuristicsEngineAccess;
using FourZug.Backend.TreeManager.TreeManagerProcessors;
using FourZug.Backend.UtilityEngine.UtilityEngineAccess;


namespace FourZug.Backend.TreeManager.TreeManagerAccess
{
    // The implemented interface of the component

    public class TreeManager : ITreeManager
    {
        // Call component scripts to create their references
        public void InitComponentReferences(IHeuristicsEngine heuEngine, IUtilityEngine utilEngine)
        {
            TreeSearcher.LoadReferences(heuEngine, utilEngine);
        }

        // Starts the tree search for the best move, and returns result
        public int GetBotBestMove(string[,] grid, string currentTurn)
        {
            int BestCol = TreeSearcher.BestMove(grid, currentTurn);
            return BestCol;
        }
    }
}
