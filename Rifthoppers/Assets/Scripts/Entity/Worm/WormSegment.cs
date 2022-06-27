using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSegment : Entity {
  [HideInInspector] public SimpleBlaster Blaster;
  public float Energy;

  public override void Awake() {
    base.Awake();
    Blaster = GetComponent<SimpleBlaster>();
  }

  // Base Entity FixedUpdate doesn't work with the worm
  public override void FixedUpdate() { }
}