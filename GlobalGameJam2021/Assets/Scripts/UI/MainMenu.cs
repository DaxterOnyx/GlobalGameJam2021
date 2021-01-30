using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class MainMenu : MonoBehaviour
{
    private VisualElement credits;
    private VisualElement menu;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var playButton = root.Q<Button>("Play");
        var aboutButton = root.Q<Button>("About");
        var quitButton = root.Q<Button>("Quit");
        credits = root.Q<VisualElement>("Credits");
        menu = root.Q<VisualElement>("Menu");
        var backButton = root.Q<Button>("Back");

        playButton.clicked += OnPlay;
        aboutButton.clicked += OnAbout;
        quitButton.clicked += OnQuit;
        backButton.clicked += OnBack;
        
        OnBack();
    }

    private void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    private void OnAbout()
    {
        menu.style.display = DisplayStyle.None;
        credits.style.display = DisplayStyle.Flex;
    }
    
    private void OnQuit()
    {
        Application.Quit(0);
    }

    private void OnBack()
    {
        menu.style.display = DisplayStyle.Flex;
        credits.style.display = DisplayStyle.None;
    }
}