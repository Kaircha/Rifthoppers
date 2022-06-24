using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSegment : Entity {
  public bool IsPowered;

  // Base Entity FixedUpdate doesn't work with the worm
  public override void FixedUpdate() { }
}