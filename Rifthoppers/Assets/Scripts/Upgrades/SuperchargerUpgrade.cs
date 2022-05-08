using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperchargerUpgrade : Upgrade{
  public override string Name => "SuperCharger";
  public override string Quote => "charge your bullets";
  public override string Description => throw new System.NotImplementedException();
  public override int Weight => throw new System.NotImplementedException();

  public SuperchargerUpgrade(int id, Sprite sprite)
  {
    ID = id;
    Sprite = sprite;
  }
  public override void OnAdd(Entity entity)
  {
    entity.Stats.MaxCharges = 10;
  }

  public override void OnRemove(Entity entity)
  {
    //entity.OnDamageDealt -= PoisonOnDamageDealt;
    //entity.Health.OnDamageTaken -= PoisonOnDamageTaken;
    //entity.Stats.ProjectileForks -= 1;
  }

}
