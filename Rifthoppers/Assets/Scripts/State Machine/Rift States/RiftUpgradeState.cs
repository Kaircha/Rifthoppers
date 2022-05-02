using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftUpgradeState : State {
  public override void Enter() {
    foreach (Player player in LobbyManager.Instance.Players) {
      player.Entity.EnterHubState();
      player.Entity.transform.position = Vector3.zero;
    }
    RiftManager.Instance.Energy.Heal();
    RiftManager.Instance.RiftScaler.Resize(10f);
    RiftManager.Instance.RiftLoader.LoadUpgrade();
    Time.timeScale = 1;
  }

  public override IEnumerator Execute() {
    // Replace this later; Somehow setting time in Enter/Exit doesn't work
    while (true) {
      Time.timeScale = 1;
      yield return null;
    }
  }
}