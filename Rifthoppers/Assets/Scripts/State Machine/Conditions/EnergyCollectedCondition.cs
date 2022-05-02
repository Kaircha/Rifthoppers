using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCollectedCondition : Condition {
  public Entity Entity;
  public int Amount;
  public int Required;
  public bool CountAllies;

  public EnergyCollectedCondition(Entity entity, int required, bool countAllies) {
    Entity = entity;
    Required = required;
    CountAllies = countAllies;
  }

  public override bool Satisfied => Amount >= Required;
  public override void Initialize() => RiftManager.Instance.OnEnergyCollected += CountCollects;
  public override void Reset() => Amount = 0;
  public override void Terminate() => RiftManager.Instance.OnEnergyCollected -= CountCollects;
  
  private void CountCollects(PlayerEntity entity) {
    if (CountAllies || Entity == entity) Amount++;
  }
}