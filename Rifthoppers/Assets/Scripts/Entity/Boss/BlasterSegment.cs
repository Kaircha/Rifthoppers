using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterSegment : MonoBehaviour {
  public Transform LeftOrigin;
  public Transform RightOrigin;

  public void ShootLeft() => Shoot(LeftOrigin);
  public void ShootRight() => Shoot(RightOrigin);

  public void Shoot() { ShootLeft(); ShootRight(); }
  public void Shoot(Transform origin) {
    
  }
}