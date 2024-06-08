using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button PlayButton,QuitButton,CreditsButton;
    private void Start()
    {
        PlayButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        CreditsButton.onClick.AddListener(() => SceneManager.LoadScene(2));
        QuitButton.onClick.AddListener(() => Application.Quit());
        
    }

}
