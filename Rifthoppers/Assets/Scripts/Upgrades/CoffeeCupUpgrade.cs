using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coffee Cup", menuName = "Upgrades/Coffee Cup")]
public class CoffeeCupUpgrade : Upgrade {
  public override void OnAdd() => Entity.Stats.PlayerFirerate += 3;
  public override void OnRemove() => Entity.Stats.PlayerFirerate -= 3;
}