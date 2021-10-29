using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private LineRenderer lineRenderer;
    private Rigidbody2D rigidbody;
    private Vector2 velocity;
    private Color endColor = new Vector4(1f,1f,1f,0.1f);
    private Color startColor = new Vector4(1f,1f,1f,0f);

    [Header("Waypoint")]
    public Waypoint currWaypoint;

    private bool isMoving = false;
    private bool canMove = true;
    private float lastPressed =0f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        transform.position = currWaypoint.transform.position;
        lineRenderer.startColor = startColor;
    }

    private void Update() {
        if(Input.GetButtonDown("Fire1") && !isMoving)
        lastPressed = 0.5f;
        lastPressed -= Time.deltaTime;

        if(lastPressed > 0f && canMove)
        Launch();

        if(isMoving) {
            lineRenderer.SetPosition(1,transform.TransformPoint(0f, -0.3f, 0f));
        }

        if((transform.position - currWaypoint.nextWaypoint.Pos()).magnitude < 0.2f && isMoving) Arrived();
    }

    private void FixedUpdate() {
        if(isMoving) 
            rigidbody.MovePosition((Vector2) transform.position + velocity * Time.deltaTime * speed);
    }

    void Launch(){
        isMoving = true;
        canMove = false;
        StopCoroutine(removeTrail());
        lineRenderer.endColor = endColor;
        lineRenderer.SetPosition(0, currWaypoint.Pos());
        transform.up = currWaypoint.nextWaypoint.Pos() - transform.position;
        velocity = currWaypoint.nextWaypoint.Pos() - transform.position;
        velocity.Normalize();
        currWaypoint.Leave();
    }

    void Arrived(){
        transform.position = currWaypoint.nextWaypoint.Pos();
        currWaypoint = currWaypoint.nextWaypoint;
        currWaypoint.Arrive();
        transform.up = Vector3.up;
        Vector3 camMove;
        CameraMovement.Instance.MoveVertical(transform.position);
        StartCoroutine(removeTrail());

        if(currWaypoint == LevelHandler.Instance.currentLevel.last){
            LevelHandler.Instance.DeployLevel();
        }
    }

    private IEnumerator removeTrail(){
        float _timer = 1f;
        isMoving = false;
        while (_timer > 0.1f)
        {
            lineRenderer.endColor = Color.Lerp(startColor, endColor, _timer);
            _timer -= Time.deltaTime * 4f;
            yield return null;
        }
        lineRenderer.endColor = startColor;
        canMove = true;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            gameObject.SetActive(false);
        }
    }
}
