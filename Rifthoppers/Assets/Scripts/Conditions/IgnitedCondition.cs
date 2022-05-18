using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnitedCondition : Condition {
  public Entity Entity;
  public float Amount;
  public float RequiredMagnitude;
  public float RequiredDuration;

  public IgnitedCondition(Entity entity, float requiredMagnitude = 0f, float requiredDuration = 0f) {
    Entity = entity;
    RequiredMagnitude = Mathf.Min(requiredMagnitude, 0f);
    RequiredDuration = Mathf.Min(requiredDuration, 0f);
  }

  public override bool Satisfied => CheckIgnite();

  public override void Initialize() { }
  public override void Reset() { }
  public override void Terminate() { }

  public bool CheckIgnite() {
    if (Entity.Effects.FirstOrDefault(x => x.GetType() == typeof(IgniteEffect)) is IgniteEffect igniteEffect) {
      if (igniteEffect.Damage >= RequiredMagnitude && igniteEffect.Duration >= RequiredDuration) return true;
    }
    return false;
  }
}