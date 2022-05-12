using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftLoader : MonoBehaviour {
  // Temporary until a second rift is needed
  public List<GameObject> Waves = new();
  public GameObject Upgrade;
  public Material FadeInMaterial;
  public Material FadeOutMaterial;
  public Material WaterPoolMaterial_In;
  public Material WaterPoolMaterial_Out;

  public GameObject CurrentArea;
  private GameObject TempArea;

  public List<Material> FadeInMaterials;
  public List<Material> FadeOutMaterials;
  public List<LayerMask> MaterialLayers;

  public void LoadWave() {
    StopAllCoroutines();
    StartCoroutine(Load(Waves[Random.Range(0, Waves.Count)]));
  }

  public void LoadUpgrade() {
    StopAllCoroutines();
    TempArea = Instantiate(Upgrade, CurrentArea.transform);
  }

  public IEnumerator Load(GameObject ToSpawn) {
    if (TempArea != null) Destroy(TempArea);
    GameObject newArea = Instantiate(ToSpawn, transform);

    newArea.GetComponent<RiftRandomizer>().RandomizeSpots();

    SetFadeOutFloat("_Alpha", 0f);

    for (int i = 0; i < MaterialLayers.Count; ++i){
      SetAreaLayerMaterial(CurrentArea, MaterialLayers[i], FadeOutMaterials[i]);
      SetAreaLayerMaterial(newArea, MaterialLayers[i], FadeInMaterials[i]);
    }

    float timer = 0f;
    while (timer < 1f) {

      SetFadeInFloat("_Alpha", timer);
      SetFadeOutFloat("_Alpha", timer);

      timer += Time.deltaTime;
      yield return null;
    }

    timer = 1f;

    SetFadeInFloat("_Alpha", timer);
    SetFadeOutFloat("_Alpha", timer);

    Destroy(CurrentArea);
    CurrentArea = newArea;
  }

  public void SetFadeInFloat(string name, float value)
  {
    foreach (Material mat in FadeInMaterials)
      mat.SetFloat(name, value);
  }
  public void SetFadeOutFloat(string name, float value)
  {
    foreach (Material mat in FadeOutMaterials)
      mat.SetFloat(name, value);
  }

  public void SetAreaLayerMaterial(GameObject area, LayerMask layermask, Material material) {

    if (area.TryGetComponent(out SpriteRenderer mainRenderer) && layermask == (layermask | (1 << area.layer)))
        mainRenderer.material = material;
    
    foreach (Transform child in area.transform) 
        SetAreaLayerMaterial(child.gameObject, layermask, material);
  }
}