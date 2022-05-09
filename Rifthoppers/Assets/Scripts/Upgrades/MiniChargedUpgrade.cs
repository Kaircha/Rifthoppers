using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinichargedUpgrade : Upgrade{
  public override string Name => "Minicharged";
  public override string Quote => "Charge your blasts, a bit!";
  public override string Description => throw new System.NotImplementedException();
  public override int Weight => throw new System.NotImplementedException();

  public MinichargedUpgrade(int id, Sprite sprite) {
    ID = id;
    Sprite = sprite;
  }

  public override void OnAdd(Entity entity) {
    entity.Stats.MaxCharges = Mathf.Max(5, entity.Stats.MaxCharges);
  }

  public override void OnRemove(Entity entity) {
    entity.Stats.MaxCharges = 1;
  }
}
