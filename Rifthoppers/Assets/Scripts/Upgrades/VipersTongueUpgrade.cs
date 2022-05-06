using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VipersTongueUpgrade : Upgrade {
  public override string Name => "Viper's Tongue";
  public override int ID => 0;
  public override Sprite Sprite => throw new System.NotImplementedException();
  public override string Quote => throw new System.NotImplementedException();
  public override string Description => throw new System.NotImplementedException();
  public override int Weight => throw new System.NotImplementedException();

  public override void OnAdd(PlayerEntity entity) {
    entity.AddCallback(VipersTongueEffect);
  }

  public override void OnRemove(PlayerEntity entity) {
    entity.RemoveCallback(VipersTongueEffect);
  }

  public void VipersTongueEffect() {
  
  }
}