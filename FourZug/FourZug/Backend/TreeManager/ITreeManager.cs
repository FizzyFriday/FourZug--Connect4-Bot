namespace FourZug.Backend.TreeManager
{
    // The interface blueprint of the component

    public interface ITreeManager
    {
        // Starts the Bot and returns best move results
        int StartBot(string[,] grid, string currentTurn);
    }
}
