﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class MainMenuScreen : MonoBehaviour
{
    public void LoadJunScene()
    {
        SoundManager.Instance.PlaySFX("sfx_button_press", false);
        SceneManager.LoadScene(1);
    }
}