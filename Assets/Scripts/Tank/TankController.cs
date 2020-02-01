using System.Collections.Generic;

public class TankController
{
    public Team TankTeam;
    public int CurrentLife;
    public List<TankSlot> TankSlots;
    private int filledSlots => TankSlots.FindAll(s => s.IsFilled).Count;
}
