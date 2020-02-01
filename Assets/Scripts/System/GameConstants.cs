using System;
using System.Collections;
using UnityEngine;

public static class GameConstants
{
    public static string BULLET_TAG = "Bullet";
    public static string PROP_TAG = "Prop";
    public static string TANK_PIECE_TAG = "TankPiece";

    public static float TIME_TO_END_STUN = 2F;

    public static IEnumerator WaitForTime(float time, Action callback)
    {
        yield return new WaitForSeconds(time);

        callback();
    }
}
