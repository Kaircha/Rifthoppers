using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Currently broken!
[CreateAssetMenu(fileName = "Backfire", menuName = "Upgrades/Backfire")]
public class BackfireUpgrade : Upgrade {
  private GameObject Clone;
  public override void Add() {
    // GetChild really sucks; This needs to be better
    Clone = Entity.UpgradeVFX.ApplyVFX(1, Entity.transform.GetChild(2)); 
    Blaster blaster = Entity.GetComponentInChildren<Blaster>(true);
    //blaster.AddWeapon(Weapon);
    
    blaster.OnShootingUpdated += SelfIgnite;
  }

  public override IEnumerator UpgradeRoutine() { yield return null; }

  public override void Remove() {
    Entity.UpgradeVFX.DeleteVFX(Clone);
    Blaster blaster = Entity.GetComponentInChildren<Blaster>(true);
    //blaster.RemoveWeapon(Weapon);
    
    blaster.OnShootingUpdated -= SelfIgnite;
  }

  private void SelfIgnite() {
    if (Random.Range(0f, 100f) < 30f - 10f * Entity.Stats.PlayerLuck)
    Entity.AddEffect(new IgniteEffect(1, 5));
  }
}