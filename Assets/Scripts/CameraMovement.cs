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

    public void MoveVertical(float pos){
        if(lastCoroutine != null) StopCoroutine(lastCoroutine);
        lastCoroutine = moveCamera(pos);
        StartCoroutine(lastCoroutine);
    }

    IEnumerator moveCamera(float pos){
        float _timer = 0f;
        while(_timer < 1f){
            transform.position = Vector3.Lerp(transform.position, 
                                            new Vector3(transform.position.x, pos, transform.position.z), 
                                            _timer);
            _timer += Time.deltaTime * 0.1f;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
        yield return null;
    }
}
