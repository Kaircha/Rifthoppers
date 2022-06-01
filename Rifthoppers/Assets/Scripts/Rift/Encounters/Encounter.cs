using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Encounter : MonoBehaviour {
  public Area Area;
  public bool IsFinished => Progress >= 1f;
  public abstract float Progress { get; }
  public virtual void EncounterStart() {
    LobbyManager.Instance.Players.ForEach(player => player.Entity.EnterRiftState());
    RiftManager.Instance.Energy.Heal();
    RiftManager.Instance.Energy.CanTakeDamage = true;
    RiftManager.Instance.RiftWaveUIMaterial.color = Color.white; // Animate this?
  }
  public abstract IEnumerator EncounterRoutine();
  public virtual void EncounterEnd() {
    RiftManager.Instance.Energy.Heal();
    RiftManager.Instance.Energy.CanTakeDamage = false;
    RiftManager.Instance.RiftWaveUIMaterial.color = Color.clear; // Animate this?
    PoolManager.Instance.EnergyOrbs.Objects.Dispose();
    PoolManager.Instance.EnergyOrblets.Objects.Dispose();
  }
}