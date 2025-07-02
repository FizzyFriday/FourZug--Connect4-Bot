using FourZug.Backend.HeuristicsEngineAccess;
using FourZug.Backend.UtilityEngineAccess;

namespace FourZug.Backend.TreeManagerAccess
{
    // The interface blueprint of the component

    public interface ITreeManager
    {
        // Calls component scripts to create their references
        void InitComponentReferences(IHeuristicsEngine heuEngine, IUtilityEngine utilEngine);

        // Starts the Bot and returns best move results
        int BestMove(char[,] grid, char currentTurn);
    }
}
