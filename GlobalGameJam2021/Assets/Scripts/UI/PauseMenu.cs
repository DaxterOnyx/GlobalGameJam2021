using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    private VisualElement pauseMenu;

    private bool isStoped = false;

    // Start is called before the first frame update
    void Start()
    {
        var visualElement = GetComponent<UIDocument>().rootVisualElement;
        pauseMenu = visualElement.Q<VisualElement>("PausePanel");
        visualElement.Q<Button>("Continue").clicked += ContinueGame;
        visualElement.Q<Button>("Quit").clicked +=()=> SceneManager.LoadScene(0);
        
        pauseMenu.style.display = DisplayStyle.None;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isStoped)
                ContinueGame();
            else
                PauseGame();
        }
    }

    void ContinueGame()
    {
        Time.timeScale = 1;
        pauseMenu.style.display = DisplayStyle.None;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.style.display = DisplayStyle.Flex;
    }
}