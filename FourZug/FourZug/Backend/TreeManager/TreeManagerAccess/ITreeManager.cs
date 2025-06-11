using FourZug.Backend.HeuristicsEngine.HeuristicsEngineAccess;
using FourZug.Backend.UtilityEngine.UtilityEngineAccess;

namespace FourZug.Backend.TreeManager.TreeManagerAccess
{
    // The interface blueprint of the component

    public interface ITreeManager
    {
        // Calls component scripts to create their references
        void InitComponentReferences(IHeuristicsEngine heuEngine, IUtilityEngine utilEngine);

        // Starts the Bot and returns best move results
        int GetBotBestMove(string[,] grid, string currentTurn);
    }
}
