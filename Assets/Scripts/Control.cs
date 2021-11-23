using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private ParticleSystem particle;
    private LineRenderer lineRenderer;
    private Rigidbody2D rigidbody;
    private Collider2D collider2D;
    private Vector2 velocity;
    private Color endColor = new Vector4(1f,1f,1f,0.1f);
    private Color startColor = new Vector4(1f,1f,1f,0f);

    [Header("Waypoint")]
    public Waypoint currWaypoint;

    private bool isMoving = false;
    private bool canMove = true;
    private float lastPressed =0f;

    private void Awake() {
        particle = GetComponent<ParticleSystem>();
        lineRenderer = GetComponent<LineRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }
    void Start()
    {
        transform.position = currWaypoint.transform.position;
        lineRenderer.startColor = startColor;

          setParticle(0) ;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0) && !isMoving && Time.timeScale >0f){
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Physics2D.Raycast(pos, Vector3.forward).collider != null){
                lastPressed = 0.1f;
            }
        }
        Debug.Log("PRESSED + "+lastPressed);
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
        setParticle(50) ;
        StopCoroutine(removeTrail());
        lineRenderer.endColor = endColor;
        lineRenderer.SetPosition(0, currWaypoint.Pos());
        transform.up = currWaypoint.nextWaypoint.Pos() - transform.position;
        velocity = currWaypoint.nextWaypoint.Pos() - transform.position;
        velocity.Normalize();
        currWaypoint.Leave();
    }

    void Arrived(){
        setParticle(0) ;
        transform.position = currWaypoint.nextWaypoint.Pos();
        currWaypoint = currWaypoint.nextWaypoint;
        currWaypoint.Arrive();
        transform.up = Vector3.up;
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
    
    private IEnumerator removeTrailMove(){
        float _timer = 1f;
        while (_timer > 0.1f)
        {
            lineRenderer.endColor = Color.Lerp(startColor, endColor, _timer);
            _timer -= Time.deltaTime * 4f;
            yield return null;
        }
        lineRenderer.endColor = startColor;
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            if(GameManager.Instance.health < 1){
                Death();
                GetComponent<Collider2D>().enabled = false;
                UIHandler.Instance.ShowDefeat(); 
                isMoving =false;
                canMove = false;
                StartCoroutine(removeTrail());
            }else{
                --GameManager.Instance.health;
                GoPrev();
            }
        }
    }

    // TWEENING
    void Spawn(){
    }

    void Death(){
        LeanTweenExt.LeanMoveLocalY(gameObject, -10f, 1f).setEaseInCubic().setOnComplete(OnDeath);
    }

    public void Dash(){
        setParticle(50) ;
        isMoving = false;
        canMove = false;
        transform.up = currWaypoint.nextWaypoint.Pos() - transform.position;
        collider2D.enabled = false;
        LeanTweenExt.LeanMove(gameObject, currWaypoint.nextWaypoint.Pos(), 0.4f)
        .setEaseInCubic()
        .setOnComplete(afterDash);
        currWaypoint.Leave();
    }
    void afterDash(){
        collider2D.enabled = true;
        setParticle(0);
        CameraMovement.Instance.MoveVertical(transform.position);
        if(currWaypoint == LevelHandler.Instance.currentLevel.last){
            LevelHandler.Instance.DeployLevel();
        }
        transform.up = Vector3.up;
        currWaypoint = currWaypoint.nextWaypoint;
        currWaypoint.Arrive();
    }

    void OnDeath(){
        gameObject.SetActive(false);
    }

    void OnBack(){
        canMove = true;
    }

    void GoPrev(){
        isMoving = false;
        setParticle(0);
        StartCoroutine(removeTrail());
        LeanTweenExt.LeanMove(gameObject, currWaypoint.Pos(), 0.5f).setEaseOutCubic().setOnComplete(OnBack);
    }

    void setParticle(int val){
        var emissionPart = particle.emission;
        emissionPart.rateOverTime = val;
    }
}
