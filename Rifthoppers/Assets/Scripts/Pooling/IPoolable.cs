using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable {
  public Pool Pool { get; set; }

  public void Release(GameObject gameObject) {
    if (Pool != null && gameObject.activeSelf) Pool.Objects.Release(gameObject);
    else gameObject.SetActive(false);
  }
}