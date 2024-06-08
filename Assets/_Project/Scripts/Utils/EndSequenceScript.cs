using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndSequenceScript : Singleton<EndSequenceScript>
{
    [SerializeField] private GameObject _virtualCamera,_mom;
    [SerializeField] private PlayableDirector _sequence;

    public void PlayEndSequence()
    {
        _mom.SetActive(true);
        _virtualCamera.SetActive(true);
        _sequence.Play();
    }

    public void SendEndSequence()
    {
        GameManager.Instance.FadeOut();
    }
}
