using System.Windows.Forms.VisualStyles;

namespace FourZug.Backend.UtilityEngine.UtilityEngineProcessors
{
    // The actual processor of the component

    // Contains helper methods such as getting valid moves for a grid
    internal static class UtilityHelper
    {

        public static string[] MakeMove(string[] stringBits, string bitsTurn, byte posID)
        {
            stringBits = (string[])stringBits.Clone();
            List<byte> validMoveIDs = MoveOptionIDs(stringBits);

            if (!validMoveIDs.Contains(posID))
            {
                throw new Exception("Invalid column move made on grid");
            }

            stringBits[posID] = bitsTurn;
            return stringBits;
        }

        public static string[,] MakeMove(string[,] grid, string turn, int col)
        {
            string[] stringBits = Flatten2DGrid(grid);
            string bitsTurn = (turn == "X") ? "10" : "01";
            byte posID = (byte)NextSpotInCol(stringBits, col);
            string[] newStringBits = MakeMove(stringBits, bitsTurn, posID);
            return Unflatten1DGrid(newStringBits);
        }

        // THIS RETURNS THE IDS OF THE MOVES, WHICH IS 5, 11, 14 etc
        public static List<byte> MoveOptionIDs(string[] stringBits)
        {
            List<byte> validIds = new();
       
            for (byte col = 0; col <= 6; col++)
            {
                sbyte id = NextSpotInCol(stringBits, col);
                if (id != -1) validIds.Add((byte)id);
            }
            return validIds;
        }

        public static (int col, int row) IDToColRow(byte id)
        {
            int col = (byte)(id / 6), row = (byte)(id % 6);
            return (col, row);
        }

        public static byte ColRowToID(int col, int row)
        {
            byte id = (byte)((col * 6) + row);
            return id;
        }

        // Needs improvement since what if there isnt an empty id in it?
        public static sbyte NextSpotInCol(string[] stringBits, int col)
        {
            // Has assumption column is valid
            byte id = ColRowToID(col, 0);
            while (stringBits[id] != "00")
            {
                if (IDToColRow(id).col != IDToColRow((byte)(id + 1)).col) return -1;
                id++;
            }
            return (sbyte)id;
        }

        public static string[] Flatten2DGrid(string[,] grid)
        {
            string[] stringBits = new string[grid.GetLength(0) * grid.GetLength(1)];

            for (int row = 0; row < grid.GetLength(1); row++)
            {
                for (int col = 0; col < grid.GetLength(0); col++)
                {
                    // String to string bits conversion
                    int posID = ColRowToID(row, col);

                    string bits = grid[col, row] switch
                    {
                        " " => "00",
                        "O" => "01",
                        "X" => "10",
                         _ => throw new NotImplementedException(),
                    };

                    stringBits[posID] = bits;
                }
            }
            return stringBits;
        }

        public static string[,] Unflatten1DGrid(string[] stringBits)
        {
            string[,] grid = new string[7, 6];

            for (int id = 0; id < stringBits.Length; id++)
            {
                int idCol = id / 6, idRow = id % 6;
                string bits = stringBits[id];
                grid[idCol, idRow] = (bits == "10") ? "X" : "O";
            }
            return grid;
        }
    }
}
