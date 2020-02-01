using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGraphics : MonoBehaviour
{
    public List<GameObject> TankSlotsGraphics;
    public TankSlotGraphics tankSlotPrefab;
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
            TankSlotGraphics slot = Instantiate(tankSlotPrefab);
            slot.SetupTankSlot(tankController.TankSlots[i]);
        }
    }

    public void UpdateTankSlots()
    {
        
    }
}
