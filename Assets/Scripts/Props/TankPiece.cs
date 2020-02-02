using System.Collections;
using UnityEngine;
public class TankPiece : IndestructibleProp
{
    public Team team;
    public string Id;
    
    public Team color;

    IEnumerator Start()
    {
        yield return null;
        if(color == Team.Blue)
            gameObject.GetComponent<SpriteRenderer>().material.SetColor(GameConstants.OUTLINE_FILL_COLOR, Color.blue);
        else
            gameObject.GetComponent<SpriteRenderer>().material.SetColor(GameConstants.OUTLINE_FILL_COLOR, Color.red);
    }
}
