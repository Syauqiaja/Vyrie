using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] public List<Transform> waypoints = new List<Transform>();

    public Vector3 NextWaypoint(){
        Vector3 result = Vector3.zero;
        if(waypoints.Count > 0){
            result = waypoints[0].position;
            waypoints.RemoveAt(0);
        }
            
        
        return result;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        for(int i=0; i<waypoints.Count-1; ++i){
            Gizmos.DrawLine(waypoints[i].position, waypoints[i+1].position);
        }
    }
}
