using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInteractable : MonoBehaviour, IInteractable {
  public PlayerBrain Brain;
  public Button UI;

  public void ShowHighlight() {
    UI.gameObject.SetActive(true);
  }

  public void Interact(PlayerBrain interactor) {
    LobbyManager.Instance.ChangeCharacter(interactor, Brain);
  }

  public void HideHighlight() {
    UI.gameObject.SetActive(false);
  }
}