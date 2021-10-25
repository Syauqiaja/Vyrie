using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint nextWaypoint;

    [SerializeField] private Animator ringAnimator;
    [SerializeField] private EnemyPatrol[] enemies;

    private void Awake() {
        enemies = GetComponentsInChildren<EnemyPatrol>();
    }

    public void Arrive(){
        foreach (EnemyPatrol enemy in enemies)
        {
            enemy.Throw();
        }
        ringAnimator.SetTrigger("Arrived");
    }

    public void Leave(){
        // ringAnimator.SetTrigger("Leave");
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
