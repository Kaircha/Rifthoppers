using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
  public LineRenderer LineRenderer;
  public EdgeCollider2D EdgeCollider;
  public ParticleSystem StartVFX;
  public ParticleSystem EndVFX;
  public Coroutine LaserRoutine;
}