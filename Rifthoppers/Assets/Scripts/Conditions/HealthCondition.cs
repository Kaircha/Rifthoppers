using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCondition : Condition {
  public Entity Entity;
  public IHealth Health;
  public float Required;
  public ComparisonType Type;

  public HealthCondition(Entity entity, float required, ComparisonType type) {
    Entity = entity;
    Health = entity.GetComponent<Health>();
    Required = Mathf.Clamp01(required);
    Type = type;
  }

  public override bool Satisfied => CheckPercentage();
  public override void Initialize() { }
  public override void Reset() { }
  public override void Terminate() { }

  private bool CheckPercentage() {
    float healthPercent = Health.Percentage;
    return Type switch {
      ComparisonType.Less => healthPercent < Required,
      ComparisonType.LessEqual => healthPercent <= Required,
      ComparisonType.Equal => healthPercent == Required,
      ComparisonType.GreaterEqual => healthPercent >= Required,
      ComparisonType.Greater => healthPercent > Required,
      _ => false,
    };
  }
}