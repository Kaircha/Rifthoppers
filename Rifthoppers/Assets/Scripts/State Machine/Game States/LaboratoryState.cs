using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaboratoryState : State {
  public override void Enter() {

    foreach (Player player in LobbyManager.Instance.Players) {
      player.Entity.Upgrades.ForEach(x => x.Remove(player.Entity));
      player.Entity.Upgrades = new();
      player.Entity.EnterLabState();
    }
  }

  public override IEnumerator Execute() {
    while (true) {
      yield return null;
    }
  }
}