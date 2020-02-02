using System;
using System.Collections;
using UnityEngine;

public static class GameConstants
{
    public static int PROPS_LAYER = 19;
    public static int TANK_LAYER = 20;
    public static string BULLET_TAG = "Bullet";
    public static string PROP_TAG = "Prop";
    public static string TANK_PIECE_TAG = "TankPiece";
    public static string PLAYER_TAG = "Player";
    public static string OUTLINE_BRIGHTNESS_TAG = "_Brightness";
    public static string OUTLINE_WIDTH = "_Width";
    public static string OUTLINE_COLOR = "_OutlineColor";
    public static string OUTLINE_FILL_COLOR = "_Color";

    public static int TANK_SLOTS = 4;

    public static float STUNNED_TIME = 2F;
    
    public static float PROP_SPAWN_DELAY = 2f;

    public static float TIME_TO_REPAIR = 2f;
    public static float HIGHTLIGHT_DELAY = 1f;
    
    public static IEnumerator WaitForTime(float time, Action callback)
    {
        yield return new WaitForSeconds(time);

        callback();
    }
}
