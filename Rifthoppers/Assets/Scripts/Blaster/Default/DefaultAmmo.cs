using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAmmo : Ammo {
  public override void Homing() {
    throw new System.NotImplementedException();
  }

  public override void Pierce(Collider2D collider) {
    throw new System.NotImplementedException();
  }

  public override void Reflect(Collision2D collision) {
    throw new System.NotImplementedException();
  }

  public override void Split(Collider2D collider) {
    throw new System.NotImplementedException();
  }
}