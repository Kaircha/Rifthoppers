using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyUpgradeVFX : MonoBehaviour
{
  public List<GameObject> Prefabs;

  public void ApplyVFX(int index) => Instantiate(Prefabs[index], transform);
  public void DeleteVFX(int index) => Destroy(transform.Find(Prefabs[index].name).gameObject);
}
