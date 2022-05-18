using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilledCondition : Condition {
  public Entity Entity;
  public int Amount;
  public int Required;
  public bool CountAllies;

  public KilledCondition(Entity entity, int required, bool countAllies) {
    Entity = entity;
    Required = required;
    CountAllies = countAllies;
  }

  public override bool Satisfied => Amount >= Required;
  public override void Initialize() => RiftManager.Instance.OnKilled += CountKills;
  public override void Reset() => Amount = 0;
  public override void Terminate() => RiftManager.Instance.OnKilled -= CountKills;
  
  private void CountKills(PlayerEntity entity) {
    if (CountAllies || Entity == entity) Amount++;
  }
}