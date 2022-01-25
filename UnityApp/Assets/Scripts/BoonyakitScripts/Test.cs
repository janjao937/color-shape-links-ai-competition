using System.Collections;
using System.Collections.Generic;
using ColorShapeLinks.Common.AI;
using ColorShapeLinks.Common;
using System.Threading;
using System;
public class Test : AbstractThinker
{
    private List<FutureMove> posibleMove;
    private List<FutureMove> nonLosingMove;
    private Random random;

    public override void Setup(string str)
    {
        posibleMove = new List<FutureMove>();
        nonLosingMove = new List<FutureMove>();
        random = new Random();
    }
    public override FutureMove Think(Board board, CancellationToken ct)
    {
        Winner winner;
        PColor colorOfAI = board.Turn;

        posibleMove.Clear();
        nonLosingMove.Clear();
        for(int col =0; col < Cols; col++)
        {
            if (board.IsColumnFull(col)) continue;

            for(int iShp = 0; iShp < 2; iShp++)
            {
                PShape shape = (PShape)iShp;
                if (board.PieceCount(colorOfAI,shape) == 0) continue;

                posibleMove.Add(new FutureMove(col, shape));

                board.DoMove(shape, col);
                winner = board.CheckWinner();

                board.UndoMove();

                if(winner.ToPColor() == colorOfAI)
                {
                    return new FutureMove(col, shape);
                }
                else if(winner.ToPColor() != colorOfAI.Other())
                {
                    nonLosingMove.Add(new FutureMove(col, shape));
                }
            }
        }
      
        if(nonLosingMove.Count > 0)
        {
            return nonLosingMove[random.Next(nonLosingMove.Count)];
        }
        return nonLosingMove[random.Next(posibleMove.Count)];
    }
    
   
    

   
}
