using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private MMProgressBar _lifeBar;
    [SerializeField] private MMF_Player _feedback;

    private void Awake()
    {
        PlayerController.OnPlayerUpdateHealth += UpdateHealth;
        PlayerController.OnPlayerLowHealth += PlayerController_OnPlayerLowHealth;
        PlayerController.OnPlayerNormalHealth += PlayerController_OnPlayerNormalHealth;
    }

    private void PlayerController_OnPlayerNormalHealth()
    {
        _feedback.StopFeedbacks();
        _feedback.RestoreInitialValues();
        _feedback.ResetFeedbacks();
    }

    private void PlayerController_OnPlayerLowHealth()
    {
        _feedback.PlayFeedbacks();
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerUpdateHealth -= UpdateHealth;
        PlayerController.OnPlayerLowHealth += PlayerController_OnPlayerLowHealth;
        PlayerController.OnPlayerNormalHealth += PlayerController_OnPlayerNormalHealth;
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        _lifeBar.UpdateBar(currentHealth, 0, maxHealth);
    }
}
