﻿using System;

namespace SolarWinds.MSP.Chess
{
    public class Pawn : ChessPiece
    {
        public Pawn(PieceColor pieceColor)
        {
            this.PieceColor = pieceColor;
            this.StrType = "Pawn";
            this.StrColor = pieceColor == PieceColor.Black ? "Black" : "White";
        }

        public override void Move(MovementType movementType, int newX, int newY)
        {
            throw new NotImplementedException("Need to implement Pawn.Move()");
        }

    }
}
