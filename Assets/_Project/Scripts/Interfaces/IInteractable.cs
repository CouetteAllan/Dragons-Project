using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void DisplayInteraction();
    public void HideInteraction();
    public void Interact(PlayerController player);
}
