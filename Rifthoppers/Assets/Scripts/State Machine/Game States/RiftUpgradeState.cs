using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftUpgradeState : State {
  public override void Enter() {
    foreach (Player player in LobbyManager.Instance.Players) {
      player.Entity.EnterLabState();
      player.Entity.transform.position = Vector3.zero;
    }
    RiftManager.Instance.Energy.Heal();
    RiftManager.Instance.RiftScaler.Resize(10f);
    RiftManager.Instance.RiftLoader.LoadUpgrade();
  }
}