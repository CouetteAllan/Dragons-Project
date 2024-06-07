using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChangeGlobalLight : MonoBehaviour
{
    public Light2D Light;
    [SerializeField] private Color _targetColor;

    public void TurnLightToRed()
    {
        Light.intensity = 1.2f;
        Light.color = _targetColor;
    }
}
