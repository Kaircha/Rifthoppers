using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrbSpawnedCondition : Condition {
  public int Amount;
  public int Required;

  public EnergyOrbSpawnedCondition(int required) {
    Required = required;
  }

  public override bool Satisfied => Amount >= Required;

  public override void Initialize() => RiftManager.Instance.OnEnergyOrbSpawned += CountSpawns;

  public override void Reset() => Amount = 0;

  public override void Terminate() => RiftManager.Instance.OnEnergyOrbSpawned -= CountSpawns;

  private void CountSpawns() => Amount++;
}