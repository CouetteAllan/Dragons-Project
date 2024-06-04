using JetBrains.Annotations;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : Singleton<FXManager>
{
    [SerializeField] private ParticleSystem _fireExplosion;
    [SerializeField] private MMF_Player _thunderFeedback;

    public void CreateFX(string fxName, Vector2 worldPosition)
    {
        switch (fxName)
        {
            case "fireExplosion":
                var fx = Instantiate(_fireExplosion, worldPosition, Quaternion.identity);
                fx.Play();
                break;
            case "thunder":
                _thunderFeedback.PlayFeedbacks();
                break;
        }
    }

}