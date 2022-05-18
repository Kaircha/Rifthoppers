using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonedCondition : Condition {
  public Entity Entity;
  public float Amount;
  public float RequiredMagnitude;
  public float RequiredStacks;

  public PoisonedCondition(Entity entity, float requiredMagnitude = 0f, float requiredStacks = 0f) {
    Entity = entity;
    RequiredMagnitude = Mathf.Min(requiredMagnitude, 0f);
    RequiredStacks = Mathf.Min(requiredStacks, 0f);
  }

  public override bool Satisfied => CheckPoison();

  public override void Initialize() { }
  public override void Reset() { }
  public override void Terminate() { }

  public bool CheckPoison() {
    if (Entity.Effects.FirstOrDefault(x => x.GetType() == typeof(PoisonEffect)) is PoisonEffect poisonEffect) {
      if (poisonEffect.Stacks.Sum(x => x.Damage) >= RequiredMagnitude && poisonEffect.Stacks.Count >= RequiredStacks) return true;
    }
    return false;
  }
}