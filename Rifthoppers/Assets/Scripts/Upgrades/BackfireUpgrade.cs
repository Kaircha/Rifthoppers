using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackfireUpgrade : Upgrade {
  public override string Name => "Backfire";
  public override string Quote => "Backwards Combustion";
  public override List<Modifier> Modifiers => new() { 
    new(ModifierType.Default, "Smaller Blaster firing backwards"),
    new(ModifierType.Default, "30% - 10% x Luck Self-Ignite on Shoot")
  };
  public override int Weight => throw new System.NotImplementedException();


  public BackfireUpgrade(int id, Sprite sprite) {
    ID = id;
    Sprite = sprite;
  }

  private GameObject Clone;
  private Weapon Weapon;
  public override void OnAdd() {
    // GetChild really sucks; This needs to be better
    Clone = Entity.UpgradeVFX.ApplyVFX(1, Entity.transform.GetChild(2)); 
    Weapon = new DefaultWeapon(Entity, Clone.transform.GetChild(1));
    Blaster blaster = Entity.GetComponentInChildren<Blaster>(true);
    blaster.AddWeapon(Weapon);
    
    blaster.OnShoot += SelfIgnite;
  }

  public override void OnRemove() {
    Entity.UpgradeVFX.DeleteVFX(Clone);
    Blaster blaster = Entity.GetComponentInChildren<Blaster>(true);
    blaster.RemoveWeapon(Weapon);
    
    blaster.OnShoot -= SelfIgnite;
  }

  private void SelfIgnite() {
    if (Random.Range(0f, 100f) < 30f - 10f * Entity.Stats.PlayerLuck)
    Entity.AddEffect(new IgniteEffect(1, 5));
  }
}