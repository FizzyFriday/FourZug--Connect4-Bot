namespace FourZug.Backend.DTOs
{
    // Represents a node in the parent-pointer "tree"
    public struct Node
    {
        // Represents grid AFTER the {lastColMove} move
        public string[,] grid;

        // Represents who makes the next move (who's turn it is)
        public string nextMoveBy;

        // Represents the last column move that made this board
        public byte lastColMove;

        public Node(string[,] grid, string currentTurn, byte lastColMove)
        {
            this.grid = grid;
            this.nextMoveBy = currentTurn;
            this.lastColMove = lastColMove;
        }
    }
}