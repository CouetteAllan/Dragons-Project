using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFeedBackSound : MonoBehaviour
{
    [SerializeField] private MMF_Player[] _feedbacks;

    public void PlayFeedback(int index)
    {
        _feedbacks[index].PlayFeedbacks();
    }
}
