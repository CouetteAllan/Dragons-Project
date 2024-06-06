using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    private void Start()
    {
        PlayButton.onClick.AddListener(() => SceneManager.LoadScene(1));
    }

}
