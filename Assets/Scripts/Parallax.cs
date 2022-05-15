using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
  public Camera camera;

  public Transform subject;

  public bool scrollYAxis;
  Vector2 startPosition;
  float startZ;
  float distanceFromSubject => transform.position.z - subject.position.z;

  float clippingPlane => camera.transform.position.z + (distanceFromSubject > 0 ? camera.farClipPlane : camera.nearClipPlane);
  float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;



  Vector2 travel => (Vector2)camera.transform.position - startPosition;
  void Start()
  {
    startPosition = transform.position;
    startZ = transform.position.z;
  }

  // Update is called once per frame
  void Update()
  {
    float newX = startPosition.x + travel.x * parallaxFactor;
    float newY = scrollYAxis ? startPosition.y + travel.y * parallaxFactor : startPosition.y + travel.y * 1;
    transform.position = new Vector3(newX, newY, startZ);
  }
}
