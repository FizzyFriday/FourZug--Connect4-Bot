namespace FourZug.Backend.TreeManager
{
    // The interface blueprint of the component

    public interface ITreeManager
    {
        // Creates and saves interface references of component
        void InitComponentReferences();

        // Starts the Bot and returns best move results
        int GetBotBestMove(string[,] grid, string currentTurn);
    }
}
