using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Really needs a better name
public class Barrel : MonoBehaviour {
  public AudioSource AudioSource;
  public CinemachineImpulseSource ImpulseSource;
  public Rigidbody2D Rigidbody;
  public Transform Origin;

  // Kind of shouldn't be here? Not sure where else to put it
  public GameObject LaserPrefab;
  [HideInInspector] public LaserAmmo Laser;

  public void Awake() {
    //GameObject laser = Instantiate(LaserPrefab, transform);
    //Laser = laser.GetComponent<LaserAmmo>();
    //laser.SetActive(false);
  }
}