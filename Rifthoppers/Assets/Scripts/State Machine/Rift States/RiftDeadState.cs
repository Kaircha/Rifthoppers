using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftDeadState : State {
  public override void Enter() {
    foreach (Player player in LobbyManager.Instance.Players) {
      player.Entity.EnterHubState();
      player.Entity.transform.position = Vector3.zero;
    }
    RiftManager.Instance.RiftScaler.Resize(5f);
    RiftManager.Instance.RiftLoader.LoadDead();
    Time.timeScale = 1;
  }

  public override IEnumerator Execute() {
    // Replace this later; Somehow setting time in Enter/Exit doesn't work
    while (true) {
      Time.timeScale = 1;
      yield return null;
    }
  }

  public override void Exit() {
    foreach (Player player in LobbyManager.Instance.Players) {
      player.Entity.Upgrades.ForEach(x => x.Remove(player.Entity));
      player.Entity.Upgrades = new();
    }
  }
}