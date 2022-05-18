using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCondition : Condition {
  public Energy Energy;
  public float Required;
  public ComparisonType Type;

  public EnergyCondition(float required, ComparisonType type) {
    Energy = RiftManager.Instance.Energy;
    Required = Mathf.Clamp01(required);
    Type = type;
  }

  public override bool Satisfied => CheckPercentage();
  public override void Initialize() { }
  public override void Reset() { }
  public override void Terminate() { }

  private bool CheckPercentage() {
    float energyPercent = Energy.Percentage;
    return Type switch {
      ComparisonType.Less => energyPercent < Required,
      ComparisonType.LessEqual => energyPercent <= Required,
      ComparisonType.Equal => energyPercent == Required,
      ComparisonType.GreaterEqual => energyPercent >= Required,
      ComparisonType.Greater => energyPercent > Required,
      _ => false,
    };
  }
}