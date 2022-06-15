using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaboratoryState : State {
  public override void Enter() {
    foreach (Player player in LobbyManager.Instance.Players) {
      player.Brain.EnterInteractState();
    }
  }
}