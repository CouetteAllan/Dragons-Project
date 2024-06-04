using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave",menuName = "EnemyWave")]
public class EnemyWaveDatas : ScriptableObject
{
    public WaveComponent[] waveComponents;
    public float TimeToSpawnInSeconds = 0.0f;
}

[Serializable]
public struct WaveComponent
{
    public float DelayWave;
    public EnemyConfig EnemyToSpawn;
}
