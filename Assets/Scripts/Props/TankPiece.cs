using UnityEngine;
public class TankPiece : IndestructibleProp
{
    private Team team;
    public string Id;
    
    public Team color;

    private void Start()
    {        
        if(color == Team.Blue)
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        else
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
