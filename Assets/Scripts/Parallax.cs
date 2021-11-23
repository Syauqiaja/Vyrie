using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos, height;
    public GameObject cam;
    public float parallaxEffect;
private void Awake() {
    if(cam == null) cam = Camera.main.gameObject;
}
    private void Start() {
        startPos = transform.position.x;
        
        // length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = cam.transform.position.y - transform.position.y;
    }

    private void Update() {
        float dist=(cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + dist, cam.transform.position.y - height, transform.position.z);
    }
}