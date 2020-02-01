using System.Collections.Generic;
using UnityEngine;
public class TankController
{
    public Team TankTeam;
    public int CurrentLife;
    public List<TankSlot> TankSlots;
    private int filledSlots => TankSlots.FindAll(s => s.IsFilled).Count;
    public bool isRepaired => TankSlots.Count == filledSlots;
    private TankGraphics tankGraphics;
    public Bullet bullet;
    public TankController(TankGraphics graphics, Team tankTeam)
    {
        TankSlots = new List<TankSlot>();
        TankTeam = tankTeam;

        for (int i = 0; i < GameConstants.TANK_SLOTS; i++)
        {
            TankSlot slot = new TankSlot{Id= "slot" + i};
            TankSlots.Add(slot);
        }

        tankGraphics = graphics;
    }

    public void AddBullet(Bullet bulletInside)
    {
        bullet = bulletInside;

        bullet.transform.SetParent(tankGraphics.transform);
        bullet.transform.localPosition = Vector3.zero;
    }

    public void RemoveBullet()
    {
        bullet = null;
    }
}
