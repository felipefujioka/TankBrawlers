using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class MainMenuScreen : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayBGM("bgm_menu");
    }

    public void LoadJunScene()
    {
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX("sfx_button_press", false);
        SceneManager.LoadScene(1);
    }
}
