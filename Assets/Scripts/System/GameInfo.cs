using UnityEngine;

public enum Team
{
    Red,
    Blue
}

public class GameInfo : MonoBehaviour
{
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
