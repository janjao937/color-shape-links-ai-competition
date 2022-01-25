using System.Collections;
using System.Collections.Generic;
using ColorShapeLinks.Common.AI;
using ColorShapeLinks.Common;
using System.Threading;
using System;

public class BK_AI : AbstractThinker
{
    // Last column played
    private int lastCol = -1;

    /// @copydoc IThinker.Think
    /// <seealso cref="IThinker.Think"/>
    public override FutureMove Think(Board board, CancellationToken ct)
    {
        // The move to perform
        FutureMove move;

        // Find next free column where to play
        do
        {
            // Get next column
            lastCol++;
            if (lastCol >= board.cols) lastCol = 0;
            // Is this task to be cancelled?
            if (ct.IsCancellationRequested) return FutureMove.NoMove;
        }
        while (board.IsColumnFull(lastCol));

        // Try to use the winning shape first
        if (board.PieceCount(board.Turn, board.Turn.Shape()) > 0)
        {
            move = new FutureMove(lastCol, board.Turn.Shape());
        }
        // If there's no pieces with the winning shape left, try and use
        // the other shape
        else if (board.PieceCount(board.Turn, board.Turn.Other().Shape()) > 0)
        {
            move = new FutureMove(lastCol, board.Turn.Other().Shape());
        }
        // Otherwise return a "no move" (this should never happen)
        else
        {
            move = FutureMove.NoMove;
        }

        // Return move
        return move;
    }


}
