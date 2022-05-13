using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperchargedUpgrade : Upgrade{
  public override string Name => "Supercharged";
  public override string Quote => "Charge your blasts!";
  public override List<Modifier> Modifiers => new() {
    new(ModifierType.Default, "Blaster charges up until limit or release"),
  };
  public override int Weight => throw new System.NotImplementedException();

  public SuperchargedUpgrade(int id, Sprite sprite) {
    ID = id;
    Sprite = sprite;
  }

  public override void OnAdd(Entity entity) {
    entity.Stats.MaxCharges = 10;
  }

  public override void OnRemove(Entity entity) {
    entity.Stats.MaxCharges = 1;
  }
}
