using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitals : MonoBehaviour {
  public List<Orbital> List = new();
  public float Speed = 2f;
  public float Radius = 4f;

  public void Add(Orbital orbital) {
    List.Add(orbital);
    StopAllCoroutines();
    StartCoroutine(OrbitalRoutine());
  }

  public void Remove(Orbital orbital) {
    if (List.Contains(orbital)) {
      List.Remove(orbital);
      (orbital as IPoolable).Release(orbital.gameObject);
      StopAllCoroutines();
      StartCoroutine(OrbitalRoutine());
    }
  }

  public void Remove() {
    foreach (Orbital orbital in List) {
      (orbital as IPoolable).Release(orbital.gameObject);
    }
    List = new();
  }

  public IEnumerator OrbitalRoutine() {
    List<float> oldAngles = new();
    List<float> newAngles = new();
    for (int i = 0; i < List.Count; i++) {
      oldAngles.Add(List[i].Angle);
      if (List.Count == 0) newAngles.Add(0f);
      else newAngles.Add((float)i / List.Count * 2f * Mathf.PI);
    }

    List.ForEach(orbital => orbital.DoBehavior = false);
    float timer = 0f;
    while (timer < 1) {
      for (int i = 0; i < List.Count; i++) {
        float angle = Mathf.Lerp(oldAngles[i], newAngles[i], timer);
        Vector3 position = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));
        List[i].Angle = angle;
        List[i].transform.localPosition = Radius * position;
        if (List[i].DoRotation) List[i].transform.right = position;
      }
      timer += Time.deltaTime;
      yield return null;
    }
    
    List.ForEach(orbital => orbital.DoBehavior = true);
    while (true) {
      foreach (Orbital orbital in List) {
        orbital.Angle = (orbital.Angle + -Speed * Time.deltaTime) % (2f * Mathf.PI);
        Vector3 position = new Vector3(Mathf.Sin(orbital.Angle), Mathf.Cos(orbital.Angle));
        orbital.transform.localPosition = Radius * position;
        if (orbital.DoRotation) orbital.transform.right = position;
      }
      yield return null;
    }
  }
}