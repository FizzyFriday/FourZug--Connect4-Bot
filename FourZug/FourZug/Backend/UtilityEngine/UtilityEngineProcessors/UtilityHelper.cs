namespace FourZug.Backend.UtilityEngine.UtilityEngineProcessors
{
    // The actual processor of the component

    // Contains helper methods such as getting valid moves for a grid
    internal static class UtilityHelper
    {

        public static string[] MakeMove(string[] stringBits, string bitsTurn, byte posID)
        {
            stringBits = (string[])stringBits.Clone();
            List<byte> validMoveIDs = ValidMoveIDs(stringBits);

            if (!validMoveIDs.Contains(posID))
            {
                throw new Exception("Invalid column move made on grid");
            }

            stringBits[posID] = bitsTurn;
            return stringBits;
        }

        public static List<byte> ValidMoveIDs(string[] stringBits)
        {
            List<byte> validIds = new();
            byte topRow = 5;

            for (byte col = 0; col <= 6; col++)
            {
                byte id = ColRowToID(col, topRow);
                if (stringBits[id] == "00") validIds.Add(id);
            }
            return validIds;
        }

        // This will make integrating bitwise require less changes
        public static string PieceStringBitConvert(string c)
        {
            // Convert piece to string bits
            if (c == "X") return "10";
            else if (c == "O") return "01";
            else if (c == " ") return "00";

            // Convert string bits to piece
            if (c == "10") return "X";
            else if (c == "01") return "O";
            else if (c == "00") return " ";

            return "";
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

        public static byte NextEmptyIDInCol(string[] stringBits, int col)
        {
            // Has assumption column is valid
            byte id = ColRowToID(col, 0);
            while (stringBits[id] == "00")
            {
                id++;
            }
            return id;
        }
    }
}
