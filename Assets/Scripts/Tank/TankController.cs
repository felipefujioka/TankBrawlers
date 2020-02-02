using System.Collections.Generic;
using System.Timers;
using DG.Tweening;
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

    private int life = 1;

    //TODO remove
    private float timer = 0; 
    public bool IsAlive()
    {
        return life > 0;
    }

    public void TakeDamage()
    {
        life--;
        if (life <= 0)
        {
            SoundManager.Instance.PlaySFX("sfx_tank_explode", false);
        }
    }
    
    public TankController(TankGraphics graphics, Team tankTeam)
    {
        TankSlots = new List<TankSlot>();
        TankTeam = tankTeam;

        for (int i = 0; i < GameConstants.TANK_SLOTS; i++)
        {
            TankSlot slot = new TankSlot {Id = "slot" + i};
            TankSlots.Add(slot);
        }

        tankGraphics = graphics;
    }

    public void AddBullet(Bullet bulletInside)
    {
        SoundManager.Instance.PlaySFX("sfx_tank_repair", false);
        
        bullet = bulletInside;

        bullet.transform.SetParent(tankGraphics.transform);
        bullet.gameObject.SetActive(false);
    }

    public void RemoveBullet()
    {
        MonoBehaviour.Destroy(bullet.gameObject);
        bullet = null;
    }
}
