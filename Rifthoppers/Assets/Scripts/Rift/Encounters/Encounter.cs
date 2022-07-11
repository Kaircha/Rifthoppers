using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Encounter : MonoBehaviour {
  public Area Area;
  public bool IsStarted;
  public abstract bool IsFinished { get; }
  public abstract float Progress { get; }
  public virtual void EncounterStart() {
    LobbyManager.Instance.Players.ForEach(player => player.Brain.EnterCombatState());
    RiftManager.Instance.Energy.Heal();
    RiftManager.Instance.Energy.CanTakeDamage = true;
    // RiftManager.Instance.RiftWaveUIMaterial.color = Color.white; // Animate this?
  }
  public abstract IEnumerator EncounterRoutine();
  public virtual void EncounterEnd() {
    RiftManager.Instance.Energy.Heal();
    RiftManager.Instance.Energy.CanTakeDamage = false;
    // RiftManager.Instance.RiftWaveUIMaterial.color = Color.clear; // Animate this?
  }
}