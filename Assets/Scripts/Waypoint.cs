using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint nextWaypoint;

    [SerializeField] private Animator ringAnimator;
    [SerializeField] private EnemyPatrol[] enemies;

    private float random;
    private GameObject fill;

    private void Awake() {
        enemies = GetComponentsInChildren<EnemyPatrol>();
    }

    private void Start() {
        random = Random.Range(1,101);
        if(random % 2 == 0 && random % 6 != 0){
            Instantiate(GameAsset.Instance.coin, transform.position, transform.rotation);
        }else if(random % 6 == 0){
            if(random % 4 == 0) 
                fill = (GameObject) Instantiate(GameAsset.Instance.hati, transform.position, transform.rotation);
            else 
                fill = (GameObject) Instantiate(GameAsset.Instance.sepatu, transform.position, transform.rotation);
        }
    }

    public void Arrive(){
        if(random % 6 == 0){
            if(random % 4 == 0) {
                if(GameManager.Instance.health < 4) GameManager.Instance.health++;
                Destroy(fill);
            }
            else {
                GameManager.Instance.PlayerControl.Dash();
                Destroy(fill);
            }
        }
        foreach (EnemyPatrol enemy in enemies)
        {
            enemy.Throw();
        }
        ringAnimator.SetTrigger("Arrived");
    }

    public void Leave(){
        ringAnimator.SetTrigger("Leave");
    }

    public Vector3 Pos(){
        return transform.position;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        if(nextWaypoint != null)
        Gizmos.DrawLine(transform.position, nextWaypoint.Pos());
    }
}
