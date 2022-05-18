using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedCondition : Condition {
  public Entity Entity;
  public int Amount;
  public int Required;

  public AttackedCondition(Entity entity, int required) {
    Entity = entity;
    Required = required;
  }

  public override bool Satisfied => Amount >= Required;
  public override void Initialize() => Entity.OnAttacked += CountAttacks;
  public override void Reset() => Amount = 0;
  public override void Terminate() => Entity.OnAttacked -= CountAttacks;
  
  private void CountAttacks() => Amount++;
}