using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Energy Amplifier", menuName = "Upgrades/Energy Amplifier")]
public class EnergyAmplifierUpgrade : Upgrade {
  public override void OnAdd() => Entity.Stats.ProjectileDamage += 4;
  public override void OnRemove() => Entity.Stats.ProjectileDamage -= 4;
}