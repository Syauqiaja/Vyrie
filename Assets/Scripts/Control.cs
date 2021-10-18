using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private LineRenderer lineRenderer;
    private Rigidbody2D rigidbody;
    private Waypoints waypointHandler;
    private Vector3 nextPoint;
    private Vector3 prevPoint;
    private Vector2 velocity;
    private Color endColor = new Vector4(1f,1f,1f,0.1f);
    private Color startColor = new Vector4(1f,1f,1f,0f);

    private bool isMoving = false;
    private bool canMove = true;
    private float lastPressed =0f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        waypointHandler = GameObject.FindWithTag("Waypoint").GetComponent<Waypoints>();
        rigidbody = GetComponent<Rigidbody2D>();
        transform.position = waypointHandler.NextWaypoint();
        nextPoint = waypointHandler.NextWaypoint();
        prevPoint = transform.position;
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

        if((transform.position - nextPoint).magnitude < 0.2f && isMoving) Arrived();
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
        prevPoint = transform.position;
        lineRenderer.SetPosition(0, prevPoint);
        transform.up = nextPoint - transform.position;
        velocity = nextPoint - transform.position;
        velocity.Normalize();
    }

    void Arrived(){
        transform.position = nextPoint;
        nextPoint = waypointHandler.NextWaypoint();
        transform.up = Vector3.up;
        CameraMovement.Instance.MoveVertical(transform.position.y + 2f);
        StartCoroutine(removeTrail());
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
}
