using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsManager : Singleton<AssetsManager>
{
    [SerializeField] private Projectile _mageProjectilePrefab;
    [SerializeField] private List<AssetObjectConfig> _assets = new List<AssetObjectConfig>();
    [SerializeField] private List<AssetSOConfig> _assetsSO = new List<AssetSOConfig>();

    public void Start()
    {
        
    }

    public GameObject GetAssetObject(string assetName)
    {
        GameObject asset = _assets.Find((a) => a.assetName == assetName).assetObject;
        return asset;
    }
    
    public ScriptableObject GetAssetSO(string assetName)
    {
        ScriptableObject asset = _assetsSO.Find((a) => a.assetName == assetName).assetObject;
        return asset;
    }
}

[Serializable]
public struct AssetObjectConfig
{
    public string assetName;
    public GameObject assetObject;
}

[Serializable]
public struct AssetSOConfig
{
    public string assetName;
    public ScriptableObject assetObject;
}
