using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGraphics : MonoBehaviour
{
    public List<TankSlotGraphics> TankSlotsGraphics;
    //public TankSlotGraphics tankSlotPrefab;
    private TankController tankController;

    private void Start()
    {
        tankController = new TankController(this);
        LoadTankSlots();
    }

    private void LoadTankSlots()
    {
        for (int i = 0; i < tankController.TankSlots.Count; i++)
        {
            //TankSlotGraphics slot = Instantiate(tankSlotPrefab);
            TankSlotsGraphics[i].SetupTankSlot(tankController.TankSlots[i]);
        }
    }

    public void UpdateTankSlots()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == GameConstants.TANK_PIECE_TAG)
        {
            var piece = other.GetComponent<TankPiece>();
            //if(Input.GetButtonDown("Grab1"))

            for (int i = 0; i < tankController.TankSlots.Count; i++)
            {
                if(piece!=null)
                {
                    if(tankController.TankSlots[i].Id == piece.Id)
                    TankSlotsGraphics[i].AddSlotPiece(piece);

                    piece.cancelGravity();
                }
            }
        }
    }
}
