using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSlotGraphics : MonoBehaviour
{
    private TankSlot tankSlot;

    public void SetupTankSlot(TankSlot slot)
    {
        tankSlot = slot;
    }

    public void AddSlotPiece(TankPiece piece)
    {
        tankSlot.Piece = piece;
        piece.transform.SetParent(transform);
        piece.transform.localPosition = Vector3.zero;
        tankSlot.IsFilled = true;
    }
}
