using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public enum Team
    {
        Red,
        Blue
    }
    
    private static GameInfo instance;
    public static GameInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Instantiate(Resources.Load<GameInfo>("System/GameInfo"));
                
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }
}
