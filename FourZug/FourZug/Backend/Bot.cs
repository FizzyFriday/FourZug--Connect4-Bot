namespace FourZug.Backend
{
    // Handles tree searching
    // Handles tree results
    internal static class Bot
    {
        // - PARAMETERS -


        // - PUBLIC METHODS -
        // Manages the Minimax searching and returns final best move results
        public static int BestMove(string[,] grid, string turn)
        {
            // Creates the root as a list of nodes as Root has no implementation
            List<Node> root = new();

            // Gets all valid columns and creates children
            List<int> validColumns = GameUtility.ValidColumns(grid);
            foreach (int validCol in validColumns)
            {
                Node directChild = new Node(grid, turn, validCol);
                root.Add(directChild);
            }

            // Run minimax on each child
            // X goes for max reward, so set to minimum at the start
            int highestReward = int.MinValue;
            if (turn == "O") highestReward = int.MaxValue;

            int bestCol = -1;
            foreach (Node directMove in root)
            {
                int reward = Minimax(directMove);
            }

      
            return -1;
        }


        // - PRIVATE METHODS -

        // Runs the minimax tree searching logic
        private static int Minimax(Node node)
        {
            return -1;
        }
    }
}
