using System.Collections.Generic;

public class TankController
{
    public Team TankTeam;
    public int CurrentLife;
    public List<TankSlot> TankSlots;
    private int filledSlots => TankSlots.FindAll(s => s.IsFilled).Count;
    private TankGraphics tankGraphics;

    public TankController(TankGraphics graphics)
    {
        for (int i = 0; i < GameConstants.TANK_SLOTS; i++)
        {
            TankSlot slot = new TankSlot();
            TankSlots.Add(slot);
        }
        tankGraphics = graphics;
    }
}
