using UnityEngine;

public abstract class Prop: MonoBehaviour
{
    public bool CanStun;
    protected abstract void onCollide(Prop collidedProp);
}
