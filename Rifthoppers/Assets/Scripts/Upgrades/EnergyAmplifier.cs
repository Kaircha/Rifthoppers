using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyAmplifier : Upgrade
{
  public override string Name => "Energy Amplifier";
  public override string Quote => "Amplified Energy, Increased Damage!";
  public override string Description => throw new System.NotImplementedException();
  public override int Weight => throw new System.NotImplementedException();

  public EnergyAmplifier(int id, Sprite sprite)
  {
    ID = id;
    Sprite = sprite;
  }

  public override void OnAdd(Entity entity) => entity.Stats.ProjectileDamage += 4;
  public override void OnRemove(Entity entity) => entity.Stats.ProjectileDamage -= 4;
}
