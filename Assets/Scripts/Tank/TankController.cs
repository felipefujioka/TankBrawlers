using System.Collections;
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
    private bool canTakeDamage = true;
    private int life = GameConstants.TANK_MAX_LIFE;

    //TODO remove
    private float timer = 0; 
    public bool IsAlive()
    {
        return life > 0;
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            tankGraphics.StartCoroutine(ShotReset());
        }
    }
    
    IEnumerator ShotReset()
    {
        yield return null;
        canTakeDamage = true;
        life--;
        tankGraphics.lifeFill.fillAmount = (float)life / GameConstants.TANK_MAX_LIFE;
    }
    
    public TankController(TankGraphics graphics, Team tankTeam)
    {
        TankSlots = new List<TankSlot>();
        TankTeam = tankTeam;

        tankGraphics = graphics;
    }
}
