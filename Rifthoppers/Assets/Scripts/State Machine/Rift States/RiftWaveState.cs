using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftWaveState : State {
  public override void Enter() {
    foreach (Player player in LobbyManager.Instance.Players) {
      player.Entity.EnterRiftState();
    }
    RiftManager.Instance.Energy.CanTakeDamage = false;
    RiftManager.Instance.Energy.Heal();
    RiftManager.Instance.Checkpoint.SetActive(true);
    RiftManager.Instance.RiftSpawner.StartSpawning();
    RiftManager.Instance.RiftScaler.Resize(20f);
    RiftManager.Instance.RiftLoader.LoadWave();
    // Animate this?
    RiftManager.Instance.RiftWaveUIMaterial.color = Color.white;
  }

  public override IEnumerator Execute() {
    while (true) {
      RiftManager.Instance.Energy.Hurt(null, null, 5f * Time.deltaTime, true);
      // Time.timeScale = RiftManager.Instance.EnergyMultiplier;
      // Experimenting!
      Time.timeScale = RiftManager.Instance.EnergyMultiplier * RiftManager.Instance.SpeedMultiplier;
      yield return null;
    }
  }

  public override void Exit() {
    RiftManager.Instance.Energy.CanTakeDamage = true;
    RiftManager.Instance.Checkpoint.SetActive(false);
    RiftManager.Instance.RiftSpawner.StopSpawning();
    // Animate this?
    RiftManager.Instance.RiftWaveUIMaterial.color = Color.clear;

    Time.timeScale = 1;
  }
}