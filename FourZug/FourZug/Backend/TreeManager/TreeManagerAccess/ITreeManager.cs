using FourZug.Backend.HeuristicsEngine.HeuristicsEngineAccess;
using FourZug.Backend.UtilityEngine.UtilityEngineAccess;

namespace FourZug.Backend.TreeManager.TreeManagerAccess
{
    // The interface blueprint of the component

    public interface ITreeManager
    {
        // Calls component scripts to create their references
        void InitComponentReferences(IHeuristicsEngine heuEngine, IUtilityEngine utilEngine);

        // Gets eval of every move
        Dictionary<int, int> EvaluateMoves(string[,] grid, string currentTurn);

        // Returns best col move
        public int GetBestMove(string[,] grid, string currentTurn);
    }
}
