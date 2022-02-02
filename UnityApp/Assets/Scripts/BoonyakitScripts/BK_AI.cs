using System.Collections;
using System.Collections.Generic;
using ColorShapeLinks.Common.AI;
using ColorShapeLinks.Common;
using System.Threading;
using System;

public class BK_AI : AbstractThinker
{
    
    private int lastCol = -1;

    
    public override FutureMove Think(Board board, CancellationToken ct)
    {
        //Perform Position
        FutureMove move;

        /*
          public FutureMove(int column, PShape shape)
        {
            this.column = column;
            this.shape = shape;
        }

        */
      
   
        do
        {
            lastCol++;
            if (lastCol >= board.cols) lastCol = 0;  // Get  column                        
            if (ct.IsCancellationRequested) return FutureMove.NoMove;
        }
        while (board.IsColumnFull(lastCol)); // Check Colum



        //Check shape   board.Turn.Shape()   Check Turn  board.Turn Color and Shape

        if (board.PieceCount(board.Turn, board.Turn.Shape()) > 0)
        {
            move = new FutureMove(lastCol, board.Turn.Shape());
        }
        else if (board.PieceCount(board.Turn, board.Turn.Other().Shape()) > 0)
        {
            move = new FutureMove(lastCol, board.Turn.Other().Shape());
        }
        else
        {
            move = FutureMove.NoMove;           //NomMove = -1,Shape = -1
        }

        return move;
    }


}
