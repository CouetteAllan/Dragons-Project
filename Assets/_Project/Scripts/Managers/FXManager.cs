using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : Singleton<FXManager>
{
    [SerializeField] private ParticleSystem _fireExplosion;

    public void CreateFX(string fxName, Vector2 worldPosition)
    {
        switch (fxName)
        {
            case "fireExplosion":
                var fx = Instantiate(_fireExplosion, worldPosition, Quaternion.identity);
                fx.Play();
                break;
        }
    }

}
