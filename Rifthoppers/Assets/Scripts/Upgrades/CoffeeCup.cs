using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCup : Upgrade{
  public override string Name => "Cofee Cup";
  public override string Quote => "Fire Rate Up!";
  public override string Description => throw new System.NotImplementedException();
  public override int Weight => throw new System.NotImplementedException();

  public CoffeeCup(int id, Sprite sprite) {
    ID = id;
    Sprite = sprite;
  }

  public override void OnAdd(Entity entity) => entity.Stats.PlayerFirerate += 3;
  public override void OnRemove(Entity entity) => entity.Stats.PlayerFirerate -= 3;
  
}
