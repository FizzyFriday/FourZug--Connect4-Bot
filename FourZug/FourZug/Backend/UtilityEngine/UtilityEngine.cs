namespace FourZug.Backend.UtilityEngine
{
    // The implemented interface of the component

    public class UtilityEngine : IUtilityEngine
    {
        public string[,] MakeMove(string[,] grid, string turn, int col)
        {
            // Return grid result from engine

            return new string[1,1];
        }

        public List<byte> GetValidBoardColumns(string[,] grid)
        {
            // Return result from engine

            return new List<byte>();
        }
    }
}
