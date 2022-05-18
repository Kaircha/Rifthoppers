using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaboratoryState : State {
  public override void Enter() {
    foreach (Player player in LobbyManager.Instance.Players) {
      player.Entity.RemoveUpgrades();
      player.Entity.RemoveEffects();
      player.Entity.EnterLabState();
    }
  }

  public override IEnumerator Execute() {
    while (true) {
      yield return null;
    }
  }
}