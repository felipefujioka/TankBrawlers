
public class Bullet : IndestructibleProp
{
    public void Destroy()
    {
        //vfx
        Destroy(gameObject);
    }
}