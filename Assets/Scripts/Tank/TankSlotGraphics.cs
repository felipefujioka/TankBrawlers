﻿using System.Collections;
using UnityEngine;

public class TankSlotGraphics : MonoBehaviour
{
    public TankSlot tankSlot;

    public void AddSlotPiece(TankPiece piece)
    {
        SoundManager.Instance.PlaySFX("sfx_tank_repair", false);

        tankSlot.Piece = piece;
        
        piece.CancelGravity();
        piece.colliderInProps.enabled = false;
        
        piece.transform.SetParent(transform);
        piece.transform.localPosition = Vector3.zero;
        piece.transform.localRotation = Quaternion.identity;
        tankSlot.IsFilled = true;
    }

    public void DestroySlot()
    {
        if (tankSlot.Piece != null)
        {
            TankPiece piece = tankSlot.Piece;
            piece.EnableGravity();
            piece.colliderInProps.enabled = true;

            var rndX = Random.Range(0.8f, 1f);
            var rndY = Random.Range(0.8f, 1f);
            Vector3 variatingDirection = new Vector3(piece.team == Team.Blue ? rndX * -1 : rndX, rndY);
        
            piece.ThrowProp(variatingDirection, null);
            
            tankSlot.Piece = null;
        }
    }
}
