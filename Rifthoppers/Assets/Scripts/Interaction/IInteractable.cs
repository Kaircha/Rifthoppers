using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {
  public void ShowHighlight();
  public void Interact(PlayerEntity interactor);
  public void HideHighlight();
}