namespace FourZug.Backend.DTOs
{
    // Represents a node in the parent-pointer "tree"
    public class Node
    {
        // Represents grid AFTER a move
        public string[] stringBits;

        // Represents who makes the next move (who's turn it is) - as 2 string bits
        public string nextBitsMove;

        // Represents the last column move that made this board
        public byte lastIDMove;

        public Node(string[] stringBits, string nextBitsMove, byte lastIDMove)
        {
            this.stringBits = stringBits;
            this.nextBitsMove = nextBitsMove;
            this.lastIDMove = lastIDMove;
        }
    }
}