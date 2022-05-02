using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftScaler : MonoBehaviour {
  public float Speed;
  public float Radius = 20f;
  public Transform Collider;
  public Transform Mask;
  public ParticleSystem ParticleSystem;

  public void Resize(float target) {
    StopAllCoroutines();
    StartCoroutine(ResizeRoutine(target));
  }
  public IEnumerator ResizeRoutine(float target) {
    var Shapemodule = ParticleSystem.shape;
    float radius = Radius;
    float time = 0;

    while (time < 1) {
      Radius = Mathf.SmoothStep(radius, target, time);
      Collider.localScale = 0.05f * Radius * Vector3.one;
      Mask.localScale = 0.05f * Radius * Vector3.one;
      Shapemodule.radius = Radius;
      time += Speed * Time.deltaTime;
      yield return null;
    }

    Radius = target;
    Collider.localScale = 1.1f * 0.05f * Radius * Vector3.one;
    Mask.localScale = 0.05f * Radius * Vector3.one;
    Shapemodule.radius = Radius;
  }
}