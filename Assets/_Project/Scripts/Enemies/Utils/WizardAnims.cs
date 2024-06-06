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
        float randomDistance = Random.Range(4.0f, 8.0f);
        int randomDirection = Random.Range(0, 2);


        Vector3 finalPos = randomDirection == 0 ? (Vector3)perpendicular : -(Vector3)perpendicular;


        this.transform.position += finalPos * randomDistance;
    }

    public void PlayWizardParticles()
    {
        _wizardSpell.Play();
    }
}
