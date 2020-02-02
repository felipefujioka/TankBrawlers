using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class MainMenuScreen : MonoBehaviour
{
    public void LoadJunScene()
    {
        SceneManager.LoadScene(1);
    }
}
