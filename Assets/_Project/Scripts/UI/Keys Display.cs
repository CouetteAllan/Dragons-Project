using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysDisplay : MonoBehaviour
{
    [SerializeField] private Image[] _keys = new Image[3];

    public void Awake()
    {
        PlayerController.OnPlayerUpdateKeyNumber += UpdateKeyDisplay;
    }

    public void OnDisable()
    {
        PlayerController.OnPlayerUpdateKeyNumber -= UpdateKeyDisplay;
    }

    public void UpdateKeyDisplay(int currentKeys)
    {
        foreach (var key in _keys)
        {
            key.gameObject.SetActive(false);
        }

        for (int i = 0; i < currentKeys; i++)
        {
            _keys[i].gameObject.SetActive(true);
            _keys[i].transform.DOPunchScale(Vector3.one, 1.5f, vibrato: 8, elasticity: .1f);
        }
    }
}
