using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence {
  MonoBehaviour Host;
  public List<IEnumerator> Routines = new List<IEnumerator>();

  public void StartSequence(MonoBehaviour host) {
    Host = host;
    Host.StartCoroutine(SequenceRoutine());
  }

  public void StopSequence() {
    if (Host != null) {
      Host.StopCoroutine(SequenceRoutine());
      Host = null;
    }
  }

  private IEnumerator SequenceRoutine() {
    foreach (IEnumerator routine in Routines) {
      yield return Host.StartCoroutine(routine);
    }
  }
}