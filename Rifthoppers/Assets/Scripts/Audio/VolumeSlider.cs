using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour {
  
  private void Start() {
    GetComponent<Slider>().value = 0.5f;
    //GetComponent<Slider>().value = DataManager.Instance.Has("MasterVolume") ? DataManager.Instance.Get<float>("MasterVolume") : 0.5f;
  }
}
