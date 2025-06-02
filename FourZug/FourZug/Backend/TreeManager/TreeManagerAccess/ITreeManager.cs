namespace FourZug.Backend.TreeManager.TreeManagerAccess
{
    // The interface blueprint of the component

    public interface ITreeManager
    {
        // Calls component scripts to create their references
        void InitComponentReferences();

        // Starts the Bot and returns best move results
        int GetBotBestMove(string[,] grid, string currentTurn);
    }
}
