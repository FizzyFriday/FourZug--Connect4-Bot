using Accessibility;
using FourZug.Backend.UtilityEngine.UtilityEngineProcessors;

namespace FourZug.Backend.UtilityEngine.UtilityEngineAccess
{
    // The implemented interface of the component

    public class UtilityEngine : IUtilityEngine
    {
        // Makes a move using string bits
        string[] MakeMove(string[] stringBits, string turn, int col)
        {
            return null;
        }

        // Make a move and return result
        public string[,] MakeMove(string[,] grid, string turn, int col)
        {
            string[,] boardResult = UtilityHelper.MakeMove(grid, turn, col);
            return boardResult;
        }

        //  Returns the valid column moves of a board
        public List<byte> GetValidBoardColumns(string[,] grid)
        {
            List<byte> validColumns = UtilityHelper.ValidColumns(grid);
            return validColumns;
        }

        // Converts a grid row and columns into an unique "id"
        public int RowColumnToID(int row, int col)
        {
            const byte idGainFromCol = 6;
            int id = (idGainFromCol * col) + (row);

            return id;
        }

        public bool isValidID(int id)
        { 
        
        }

        // Converts 2D string grid to 1D byte grid
        public string[] Flatten2DGrid(string[,] grid)
        {
            string[] stringBits = new string[grid.GetLength(0) + grid.GetLength(1)];

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 6; col++)
                {
                    int posID = RowColumnToID(row, col);
                    // Do string to byte conversion
                    stringBits[posID] = UtilityHelper.PieceStringBitConvert(grid[col, row]);
                }
            }
            return stringBits;
        }

        // Converts 1D byte grid to 2D string grid
        public string[,] Unflatten1DGrid(string[] stringBits)
        {
            string[,] grid = new string[7, 6];

            for (int id = 0; id < stringBits.Length; id++)
            {
                int idCol = id / 6, idRow = id % 6;
                grid[idCol, idRow] = UtilityHelper.PieceStringBitConvert(stringBits[id]);
            }
            return grid;
        }

        public string StringToStringBits(string str)
        {
            return UtilityHelper.PieceStringBitConvert(str);
        }
    }
}
