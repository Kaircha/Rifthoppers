using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Supercharged", menuName = "Upgrades/Supercharged")]
public class SuperchargedUpgrade : Upgrade {
  public WeaponType WeaponType;

  public override void OnAdd() {
    Entity.Stats.WeaponType |= WeaponType;
    Entity.Blaster.ReplaceWeapons(Entity.Stats.WeaponType);
  }

  public override void OnRemove() {
    Entity.Stats.WeaponType &= ~WeaponType;
    Entity.Blaster.ReplaceWeapons(Entity.Stats.WeaponType);
  }
}