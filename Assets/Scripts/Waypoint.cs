using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint nextWaypoint;

    private SpriteRenderer sprite;
    private IEnumerator arriveCoroutine = null;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Arrive(){
        arriveCoroutine = Arriving();
        animator.SetTrigger("Arrived");
        // StartCoroutine(arriveCoroutine);
    }

    public void Leave(){
        if(arriveCoroutine != null) StopCoroutine(arriveCoroutine);
        animator.SetTrigger("Leave");
        // StartCoroutine(Leaving());
    }

    IEnumerator Arriving(){
        float _timer = 0f;
        while(_timer <1f){
            sprite.color = Color.Lerp(sprite.color,Color.white, _timer);
            _timer += Time.deltaTime * 0.5f;
            yield return null;
        }

        sprite.color = Color.white;

        yield return null;
    }

    IEnumerator Leaving(){
        float _timer = 0f;
        while(_timer <1f){
            sprite.color = Color.Lerp(sprite.color,Color.clear, _timer);
            _timer += Time.deltaTime * 0.5f;
            yield return null;
        }

        sprite.color = Color.clear;

        yield return null;
    }

    public Vector3 Pos(){
        return transform.position;
    }
}
