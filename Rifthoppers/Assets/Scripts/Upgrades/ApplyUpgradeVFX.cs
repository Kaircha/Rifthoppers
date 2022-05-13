using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyUpgradeVFX : MonoBehaviour {
  public List<GameObject> Prefabs;
  public List<GameObject> Clones;

  public GameObject ApplyVFX(int index, Transform origin = null) {
    GameObject clone = Instantiate(Prefabs[index], origin ?? transform);
    Clones.Add(clone);
    return clone;
  }

  public void DeleteVFX(GameObject clone) {
    GameObject vfx = Clones.Find(x => x == clone);
    if (vfx != null) {
      Clones.Remove(vfx);
      Destroy(vfx);
    }
  }
}