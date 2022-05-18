using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoisonEffect : Effect {
  public List<(float Damage, float Duration)> Stacks = new();

  public PoisonEffect(float damage, float duration) {
    Stacks = new();
    Stacks.Add((damage, duration));
  }

  public override void Add(Effect effect) {
    if (effect is not PoisonEffect) return;
    PoisonEffect poison = effect as PoisonEffect;

    if (poison.Stacks.Count == 0) return;

    Stacks.AddRange(poison.Stacks);
  }

  public override IEnumerator EffectRoutine() {
    //GameObject P = Instantiate(PoisonParticles, Sprite.gameObject.transform);
    //Sprite.material = PoisonMat;

    while (Stacks.Count > 0) {
      Entity.Health.Hurt(null, Entity, Stacks.Sum(x => x.Damage) * Time.deltaTime, true);
      Stacks.ForEach(x => x.Duration -= Time.deltaTime);
      Stacks.RemoveAll(x => x.Duration <= 0);
      yield return null;
    }

    Entity.RemoveEffect(this);

    //Destroy(P);
    //Sprite.material = def;
  }
}