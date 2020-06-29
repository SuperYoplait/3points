﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                isPause = true;
                Time.timeScale = 0;
                this.gameObject.SetActive(true);
            }
            if (isPause)
            {
                isPause = false;
                Time.timeScale = 1;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void SetPause()
    {
        if (!isPause)
        {
            isPause = true;
            Time.timeScale = 0;
            this.gameObject.SetActive(true);
        }
    }

    public void Continue()
    {
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            this.gameObject.SetActive(false);
        }
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
