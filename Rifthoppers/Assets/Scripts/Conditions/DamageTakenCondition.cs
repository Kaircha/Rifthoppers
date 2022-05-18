using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenCondition : Condition {
  public Entity Entity;
  public float Amount;
  public float Required;
  public bool IsSummed;

  public DamageTakenCondition(Entity entity, float required, bool isSummed) {
    Entity = entity;
    Required = required;
    IsSummed = isSummed;
  }

  public override bool Satisfied => Amount >= Required;
  public override void Initialize() => RiftManager.Instance.Energy.OnDamageTaken += CountDamage;
  public override void Reset() => Amount = 0;
  public override void Terminate() => RiftManager.Instance.Energy.OnDamageTaken -= CountDamage;

  private void CountDamage(Entity dealer, Entity receiver, float amount, bool isDoT) {
    if (IsSummed) Amount += amount;
    else Amount = amount;
  }
}