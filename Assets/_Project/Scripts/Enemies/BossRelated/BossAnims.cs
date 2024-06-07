using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnims : MonoBehaviour
{
    [SerializeField] private MMF_Player _feedback, _dash,_throw;
    public void PlaySoundTantrum()
    {
        _feedback.PlayFeedbacks();
    }
    public void PlaySoundDash()
    {
        _dash.PlayFeedbacks();
    }

    public void PlaySoundThrow()
    {
        _throw.PlayFeedbacks();
    }
}
