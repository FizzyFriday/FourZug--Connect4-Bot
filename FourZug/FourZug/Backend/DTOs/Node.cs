namespace FourZug.Backend.DTOs
{
    // Represents a node in the parent-pointer "tree"
    public class Node
    {
        // Represents grid AFTER a move
        public string[] stringBits;

        // Represents who makes the next move (who's turn it is)
        public string nextMoveBy;

        // Represents the last column move that made this board
        public byte lastColMove;

        public Node(string[] stringBits, string currentTurn, byte lastColMove)
        {
            this.stringBits = stringBits;
            this.nextMoveBy = currentTurn;
            this.lastColMove = lastColMove;
        }
    }
}