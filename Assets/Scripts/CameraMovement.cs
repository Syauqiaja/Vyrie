using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    private IEnumerator lastCoroutine = null;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float damp = 2f;

    public static CameraMovement Instance;

    private void Awake() {
        if(Instance == null) Instance = this;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public void MoveVertical(Vector3 pos){
        if(lastCoroutine != null) StopCoroutine(lastCoroutine);
        Debug.Log(Mathf.Abs(player.transform.position.x - transform.position.x));
        if(Mathf.Abs(player.transform.position.x - transform.position.x) > 1.5f){
            pos -= (player.transform.position.x - transform.position.x) > 0f ? new Vector3(1.5f,-2f,-transform.position.z) : new Vector3(-1.5f,-2f,-transform.position.z);
        }else pos = new Vector3(transform.position.x,pos.y + 2f,transform.position.z);
        lastCoroutine = moveCamera(pos);
        StartCoroutine(lastCoroutine);
    }

    IEnumerator moveCamera(Vector3 pos){
        float _timer = 0f;
        float multiplier = 1f;
        while(_timer < 1f){
            transform.position = Vector3.Lerp(transform.position, 
                                              pos, 
                                              Mathf.SmoothStep(0f, 1f, _timer));
            _timer += Time.deltaTime * multiplier;
            if(multiplier > 0.1f)multiplier *= .92f;
            yield return null;
        }
        transform.position = pos;
        yield return null;
    }
}
