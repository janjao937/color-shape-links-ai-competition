using ColorShapeLinks.Common;
using ColorShapeLinks.Common.AI;
using System.Threading;



public class Boonyakit_AiThinker : AbstractThinker
{
    FutureMove move;

    int col = 0;
    bool check = true;
    
    public override FutureMove Think(Board board, CancellationToken ct)
    {
        if(check)
        {
            col = board.cols / 2;
            check = false;
        }

        if (col != board.cols / 2) col++;  //IF col != col/2  ==  col++

        while (board.IsColumnFull(col))
        {
            col++;                             //If This col full == col++
           
        }

        if (board.PieceCount(board.Turn, board.Turn.Shape()) > 0)
        {
            move = new FutureMove(col, board.Turn.Shape());
            
        }
        else if (board.PieceCount(board.Turn, board.Turn.Other().Shape()) > 0)
        {
            move = new FutureMove(col, board.Turn.Other().Shape());
        }
        else
        {
            move = FutureMove.NoMove;          
        }

        return move;
    }



}
