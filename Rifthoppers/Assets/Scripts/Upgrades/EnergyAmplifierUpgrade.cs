using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyAmplifierUpgrade : Upgrade {
  public override string Name => "Energy Amplifier";
  public override string Quote => "More Power!";
  public override List<Modifier> Modifiers => new() {
    new(ModifierType.Increase, "+4 Damage up"),
  };
  public override int Weight => throw new System.NotImplementedException();

  public EnergyAmplifierUpgrade(int id, Sprite sprite) {
    ID = id;
    Sprite = sprite;
  }

  public override void OnAdd(Entity entity) => entity.Stats.ProjectileDamage += 4;
  public override void OnRemove(Entity entity) => entity.Stats.ProjectileDamage -= 4;
}