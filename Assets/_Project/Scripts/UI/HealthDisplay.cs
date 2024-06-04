using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private MMProgressBar _lifeBar;

    private void Awake()
    {
        PlayerController.OnPlayerUpdateHealth += UpdateHealth;
    }
    private void OnDisable()
    {
        PlayerController.OnPlayerUpdateHealth -= UpdateHealth;
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        _lifeBar.UpdateBar(currentHealth, 0, maxHealth);
    }
}
