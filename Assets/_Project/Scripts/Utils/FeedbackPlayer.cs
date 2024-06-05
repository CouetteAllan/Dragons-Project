using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{

    [SerializeField] private MMF_Player _feedbackTantrum;
    public void PlayFeedback()
    {
        _feedbackTantrum.PlayFeedbacks();
    }
}
