using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ParticleSystem particle;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Debug.Log("Dapat coin");
            particle.transform.SetParent(null);
            particle.Play();
            Destroy(gameObject);
        }
    }
}
