namespace FourZug.Backend.TreeManager
{
    // The implemented interface of the component

    public class TreeManager : ITreeManager
    {
        // Starts the tree search for the best move, and returns result
        public int GetBotBestMove(string[,] grid, string currentTurn)
        {
            byte BestCol = TreeSearcher.BestMove(grid, currentTurn);
            return (int)BestCol;
        }
    }
}
