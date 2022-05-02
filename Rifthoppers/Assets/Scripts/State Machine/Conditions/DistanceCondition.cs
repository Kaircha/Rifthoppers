using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCondition : Condition {
  public Entity Entity;
  public TargetType Target;
  public float Required;
  public ComparisonType Type;

  public DistanceCondition(Entity entity, TargetType target, float required, ComparisonType type) {
    Entity = entity;
    Target = target;
    Required = Mathf.Max(0, required);
    Type = type;
  }

  public override bool Satisfied => CheckDistance();
  public override void Initialize() { }
  public override void Reset() { }
  public override void Terminate() { }
  
  private bool CheckDistance() {
    float distance = Target switch {
      TargetType.ClosestPlayer => LobbyManager.Instance.GetDistanceToClosest(Entity.transform.position),
      _ => 0,
    };

    return Type switch {
      ComparisonType.Less => distance < Required,
      ComparisonType.LessEqual => distance <= Required,
      ComparisonType.Equal => distance == Required,
      ComparisonType.GreaterEqual => distance >= Required,
      ComparisonType.Greater => distance > Required,
      _ => false,
    };
  }
}

public enum ComparisonType {
  Less,
  LessEqual,
  Equal,
  GreaterEqual,
  Greater
}

public enum TargetType { 
  ClosestPlayer
}