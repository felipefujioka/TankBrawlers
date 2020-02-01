public class DestructiveProp : Prop
{
    private void Destroy()
    {
        
    }
    
    protected override void onCollide()
    {
        throw new System.NotImplementedException();
    }

    protected override void onCollide(Prop p)
    {
        throw new System.NotImplementedException();
    }
}
