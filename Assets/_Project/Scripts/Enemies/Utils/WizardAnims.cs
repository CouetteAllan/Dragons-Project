using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnims : MonoBehaviour
{
    [SerializeField] private ParticleSystem _wizardSpell;

    public void WizardTP()
    {
        var playerPos = (GameManager.Instance.Player.transform.position - this.transform.position).normalized;
        Vector2 perpendicular = new Vector2(-playerPos.y,playerPos.x);
        this.transform.position += (Vector3)perpendicular * 5.0f;
    }

    public void PlayWizardParticles()
    {
        _wizardSpell.Play();
    }
}
