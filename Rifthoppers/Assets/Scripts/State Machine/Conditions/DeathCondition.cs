using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCondition : Condition {
  public Entity Entity;
  public DeathCondition(Entity entity) {
    Entity = entity;
  }

  public override bool Satisfied {
    get {
      if (Entity != null && Entity.Health != null) return Entity.Health.IsDead;
      else return false;
    }
  }
  public override void Initialize() { }
  public override void Reset() { }
  public override void Terminate() { }
}