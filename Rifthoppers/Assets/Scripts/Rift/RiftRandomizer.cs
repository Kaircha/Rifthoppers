using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftRandomizer : MonoBehaviour{

  public List<Transform> spots;

  public void RandomizeSpots(){
    foreach (Transform spot in spots)
      spot.GetChild(Random.Range(0, spot.childCount)).gameObject.SetActive(true);
  }
}
