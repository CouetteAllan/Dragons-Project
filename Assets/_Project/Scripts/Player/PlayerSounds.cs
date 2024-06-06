using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip _fireLaunchClip;
    [SerializeField] AudioClip _powerUp;
    public void PlaySound(string soundName)
    {
        MMSoundManagerSoundPlayEvent.Trigger(_fireLaunchClip, MMSoundManager.MMSoundManagerTracks.Sfx, this.transform.position, false, .6f);
    }


}
