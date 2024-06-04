using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField]
    private List<EnemySpawner> _spawners= new List<EnemySpawner>();
    private List<EnemyController> _enemies = new List<EnemyController>();

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newState)
    {
        if(newState == GameState.StartGame)
        {
            Initialize();
        }
    }

    private void Start()
    {
        
    }

    private void Initialize()
    {
        

    }
}
