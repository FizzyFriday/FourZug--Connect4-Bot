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

        // Returns each eval of each move option
        public Dictionary<int, int> EvaluateMoves(string[,] grid, string currentTurn)
        {
            return TreeSearcher.EvalMoves(grid, currentTurn);
        }


        public int GetBestMove(string[,] grid, string currentTurn)
        {
            Dictionary<int, int> evals = TreeSearcher.EvalMoves(grid, currentTurn);
            return evals.OrderByDescending(x => x.Value).First().Key;
        }
    }
}
