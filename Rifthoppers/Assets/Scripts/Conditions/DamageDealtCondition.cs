using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealtCondition : Condition {
  public Entity Entity;
  public float Amount;
  public float Required;
  public bool IsSummed;

  public DamageDealtCondition(Entity entity, float required, bool isSummed) {
    Entity = entity;
    Required = required;
    IsSummed = isSummed;
  }

  public override bool Satisfied => Amount >= Required;
  public override void Initialize() => Entity.OnDamageDealt += CountDamage;
  public override void Reset() => Amount = 0;
  public override void Terminate() => Entity.OnDamageDealt -= CountDamage;
  
  private void CountDamage(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (IsSummed) Amount += amount;
    else Amount = amount;
  }
}