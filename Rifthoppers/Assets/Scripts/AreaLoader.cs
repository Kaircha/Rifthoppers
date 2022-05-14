using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLoader : MonoBehaviour
{

  public List<Material> FadeMaterials; // first one needs to be the default one 
  public List<GameObject> Waves = new(); // temporary until we add wave class
  public Transform CurrentArea;
  private Transform TemporaryArea;
  public GameObject Upgrade;

  Coroutine loadRoutine = null;

  public void LoadWave(){
    if (loadRoutine != null) StopCoroutine(loadRoutine);
    loadRoutine = StartCoroutine(Load(Waves[Random.Range(0, Waves.Count)]));
  }
  public void LoadUpgrade(){
    if (loadRoutine != null) StopCoroutine(loadRoutine);
    TemporaryArea = Instantiate(Upgrade, CurrentArea).transform;
  }

  public IEnumerator Load(GameObject Area)
  {
    if (TemporaryArea != null) Destroy(TemporaryArea.gameObject);

    Transform newArea = Instantiate(Area, transform).transform;
    if (newArea.TryGetComponent<RiftRandomizer>(out RiftRandomizer randomizer))
      randomizer.RandomizeSpots();

    SetDefaultMaterial(newArea);
    SetAreaMaterialFloat(CurrentArea, "_Size", 0.1f);
    SetAreaMaterialFloat(CurrentArea, "_Alpha", 0f);

    float timer = 0f;
    while (timer < 1f)
    {
      SetAreaMaterialFloat(CurrentArea, "_Alpha", timer);
      SetAreaMaterialFloat(newArea, "_Alpha", timer);
      timer += Time.deltaTime;
      yield return null;
    }

    SetAreaMaterialFloat(CurrentArea, "_Alpha", 1);
    SetAreaMaterialFloat(newArea, "_Alpha", 1);

    Destroy(CurrentArea.gameObject);
    CurrentArea = newArea;
  }

  public void SetFadeFloat(string name, float val) {
    foreach (Material mat in FadeMaterials)
      mat.SetFloat(name, val);
  }

  public void SetDefaultMaterial(Transform Area){
    if (Area.gameObject.layer == 0 && Area.TryGetComponent(out SpriteRenderer rend))
      rend.material = FadeMaterials[0];
    foreach (Transform child in Area) SetDefaultMaterial(child);
  }
  public void SetAreaMaterialFloat(Transform Area, string name, float value){
    if (Area.TryGetComponent(out SpriteRenderer rend))
      rend.material.SetFloat(name, value);
    foreach (Transform child in Area) SetAreaMaterialFloat(child, name, value);
  }

  public float ScaleSpeed;
  public float ScaleRadius = 20f;
  public Transform RiftCollider;
  public Transform Mask;
  public ParticleSystem RiftParticleSystem;

  private Coroutine scaleRoutine = null;

  public void Resize(float target){
    if (scaleRoutine != null) StopCoroutine(scaleRoutine);
    scaleRoutine = StartCoroutine(ResizeRoutine(target));
  }
  public IEnumerator ResizeRoutine(float target){
    var shapemodule = RiftParticleSystem.shape;
    float radius = ScaleRadius;

    float time = 0;
    while (time < 1){
      time += ScaleSpeed * Time.deltaTime;
      ScaleRadius = Mathf.SmoothStep(radius, target, time);
      RiftCollider.localScale = Mask.localScale = 0.05f * ScaleRadius * Vector3.one;
      shapemodule.radius = ScaleRadius;
      yield return null;
    }
    ScaleRadius = shapemodule.radius = target;
    RiftCollider.localScale = Mask.localScale = 0.05f * target * Vector3.one;
  }
}
