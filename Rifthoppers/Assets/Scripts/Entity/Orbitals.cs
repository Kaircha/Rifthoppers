using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitals : MonoBehaviour {
  private List<Orbital> OrbitalList = new();

  public void Update() {
    foreach (Orbital orbital in OrbitalList) {
      orbital.transform.RotateAround(transform.position, new Vector3(0, 0, 1), 90f * Time.deltaTime);
      if (orbital.KeepUpright) orbital.transform.rotation = Quaternion.identity;
    }
  }

  public void Add(Orbital orbital) {
    orbital.transform.position = 4f * Vector3.down;
    OrbitalList.Add(orbital);
    StopAllCoroutines();
    StartCoroutine(OrderOrbitalsRoutine());
  }

  public void Remove(Orbital orbital) {
    if (OrbitalList.Contains(orbital)) {
      OrbitalList.Remove(orbital);
      (orbital as IPoolable).Release(orbital.gameObject);
      StopAllCoroutines();
      StartCoroutine(OrderOrbitalsRoutine());
    }
  }

  public void Remove() {
    foreach (Orbital orbital in OrbitalList) {
      (orbital as IPoolable).Release(orbital.gameObject);
    }
    OrbitalList = new();
  }

  public IEnumerator OrderOrbitalsRoutine() {
    float timer = 0f;
    List<Vector3> origins = OrbitalList.Select(x => x.transform.localPosition).ToList();
    List<Vector3> targets = new();
    for (int i = 0; i < OrbitalList.Count; i++) {
      float angle = i * 360f / OrbitalList.Count;
      targets.Add(4f * new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)));
    }

    while (timer < 1) {
      for (int i = 0; i < OrbitalList.Count; i++) OrbitalList[i].transform.localPosition = Vector3.Lerp(origins[i], targets[i], timer);
      timer += Time.deltaTime;
      yield return null;
    }
  }
}