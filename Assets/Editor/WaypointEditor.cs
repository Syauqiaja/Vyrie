using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WaypointEditor{
    private void OnDrawSceneGizmos(Waypoints waypoint, GizmoType gizmoType) {
        for(int i=0; i<waypoint.waypoints.Count-1; ++i){
            Gizmos.DrawLine(waypoint.waypoints[i].position, waypoint.waypoints[i+1].position);
        }
    }
}