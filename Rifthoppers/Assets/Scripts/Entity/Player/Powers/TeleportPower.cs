using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportPower : Power {
  public ParticleSystem TeleportSystem;
  public AudioSource TeleportSource;

  public override void Press() {
    Entity.transform.position = Entity.Target.position;
  }

  public override void Hold() { }

  public override void Release() { }
}