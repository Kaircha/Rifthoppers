using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftLoader : MonoBehaviour {
  // Temporary until a second rift is needed
  public List<GameObject> Waves = new List<GameObject>();
  public GameObject Upgrade;
  public GameObject Dead;
  public Material FadeInMaterial;
  public Material FadeOutMaterial;
  public Material WaterPoolMaterial_In;
  public Material WaterPoolMaterial_Out;

  public GameObject CurrentArea;
  private GameObject TempArea;

  // These should play an animation for the load
  public void LoadDead() {
    StopAllCoroutines();
    TempArea = Instantiate(Dead, CurrentArea.transform);
  }

  public void LoadWave() {
    StopAllCoroutines();
    StartCoroutine(Load(Waves[Random.Range(0, Waves.Count)]));
  }

  public void LoadUpgrade() {
    StopAllCoroutines();
    TempArea = Instantiate(Upgrade, CurrentArea.transform);
  }

  // Need to find beter solution for transitioning objects that don't use the FadeOut/In materials

  public IEnumerator Load(GameObject ToSpawn) {
    if (TempArea != null) Destroy(TempArea);
    GameObject newArea = Instantiate(ToSpawn, transform);

    newArea.GetComponent<RiftRandomizer>().RandomizeSpots();

    FadeOutMaterial.SetFloat("_Alpha", 0f);
    WaterPoolMaterial_Out.SetFloat("_Alpha", 0f);

    SetAreaMaterial(newArea, FadeInMaterial, WaterPoolMaterial_In);
    SetAreaMaterial(CurrentArea, FadeOutMaterial, WaterPoolMaterial_Out);

    float timer = 0f;
    while (timer < 1f) {
      FadeInMaterial.SetFloat("_Alpha", timer);
      FadeOutMaterial.SetFloat("_Alpha", timer);
      WaterPoolMaterial_In.SetFloat("_Alpha", timer);
      WaterPoolMaterial_Out.SetFloat("_Alpha", timer);
      timer += Time.deltaTime;
      yield return null;
    }
    timer = 1f;
    FadeInMaterial.SetFloat("_Alpha", timer);
    FadeOutMaterial.SetFloat("_Alpha", timer);
    WaterPoolMaterial_In.SetFloat("_Alpha", timer);
    WaterPoolMaterial_Out.SetFloat("_Alpha", timer);

    Destroy(CurrentArea);
    CurrentArea = newArea;
  }

  public void SetAreaMaterial(GameObject area, Material material, Material forWater) {

    if (area.TryGetComponent(out SpriteRenderer mainRenderer)){

      if (area.layer != 14)
        mainRenderer.material = material;
      else
        mainRenderer.material = forWater;
    }
    
    foreach (Transform child in area.transform) 
        SetAreaMaterial(child.gameObject, material, forWater);
  }
}