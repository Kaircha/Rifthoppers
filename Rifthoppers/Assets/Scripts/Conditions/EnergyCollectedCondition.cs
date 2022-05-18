using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCollectedCondition : Condition {
  public int Amount;
  public int Required;

  public EnergyCollectedCondition(int required) {
    Required = required;
  }

  public override bool Satisfied => Amount >= Required;
  public override void Initialize() => RiftManager.Instance.OnEnergyCollected += CountCollects;
  public override void Reset() => Amount = 0;
  public override void Terminate() => RiftManager.Instance.OnEnergyCollected -= CountCollects;

  private void CountCollects() => Amount++;
}