using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyWings : Upgrade
{
  public override string Name => "Energy Wings";
  public override string Quote => "Fly Over Stuff!";
  public override string Description => throw new System.NotImplementedException();
  public override int Weight => throw new System.NotImplementedException();

  public EnergyWings(int id, Sprite sprite)
  {
    ID = id;
    Sprite = sprite;
  }

  public override void OnAdd(Entity entity)
  {
    entity.UpgradeVFX.ApplyVFX(0);
  }

  public override void OnRemove(Entity entity)
  {
    entity.Stats.MaxCharges = 1;
  }
}
