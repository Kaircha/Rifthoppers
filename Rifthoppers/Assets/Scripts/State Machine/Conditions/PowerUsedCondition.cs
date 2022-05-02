using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Could be expanded into Co-op
public class PowerUsedCondition : Condition {
  public Power Power;
  public bool PowerUsed;

  public PowerUsedCondition(Power power) {
    Power = power;
  }

  public override bool Satisfied => PowerUsed;
  public override void Initialize() => Power.OnPowerUsed += SetPowerUsed;
  public override void Reset() => PowerUsed = false;
  public override void Terminate() => Power.OnPowerUsed -= SetPowerUsed;
  
  private void SetPowerUsed() => PowerUsed = true;
}