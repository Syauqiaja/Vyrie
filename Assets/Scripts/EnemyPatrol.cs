using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public int order = 0;
    public float radius = 1f;
    public float speed = 1f;
    public Transform centerPoint = null;

    private Collider2D collider;
    private bool isThrowed = false;

    private void Awake() {
        collider = GetComponent<Collider2D>();
    }
    private void Update() {
        if(isThrowed) Throwing();
        else Move();
    }

    public void Throw(){
        collider.enabled = false;
        isThrowed = true;
        Invoke("Die", 2f);
    }

    void Die(){
        Destroy(gameObject);
    }

     void Throwing(){
         radius += Time.deltaTime * 10f;
        Vector3 currentPosition;
        currentPosition.x = Mathf.Cos(Time.time * speed * 1.5f - (float)order * 1.1f/2f) * radius;
        currentPosition.y = Mathf.Sin(Time.time * speed * 1.5f - (float)order * 1.1f/2f) * radius;
        currentPosition.z = 0;

        transform.position = centerPoint.position + currentPosition;
    }

    void Move(){
        Vector3 currentPosition;
        currentPosition.x = Mathf.Cos(Time.time * speed * 1.5f - (float)order * 1.1f/2f/radius) * radius;
        currentPosition.y = Mathf.Sin(Time.time * speed * 1.5f - (float)order * 1.1f/2f/radius) * radius;
        currentPosition.z = 0;

        transform.position = centerPoint.position + currentPosition;
    }
}
