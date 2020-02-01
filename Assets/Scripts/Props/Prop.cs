using UnityEngine;

public abstract class Prop: MonoBehaviour
{
    protected bool canStun;
    protected abstract void onCollide();
    protected abstract void onCollide(Prop p);
}
