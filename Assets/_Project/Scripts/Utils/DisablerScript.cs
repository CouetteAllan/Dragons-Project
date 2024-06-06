using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablerScript : MonoBehaviour
{
    public enum DisableFunction
    {
        DoNothing,
        Destroy
    }
    [SerializeField] private DisableFunction _function;

    public void DisableObject(PickUpObject objectPick)
    {
        if(_function == DisableFunction.DoNothing)
        {
            return;
        }
        else
        {
            Destroy(objectPick.gameObject);
        }
    }
}
