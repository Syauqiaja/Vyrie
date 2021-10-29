using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaypointEditor : EditorWindow {

    [MenuItem("Tools/WaypointManagerWindow")]
    public static void Open(){
        GetWindow<WaypointEditor>();
    }

    public Transform waypointRoot;
    public GameObject waypointPrefab;

    private void OnGUI() {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));
        EditorGUILayout.PropertyField(obj.FindProperty("waypointPrefab"));

        if(waypointRoot == null || waypointPrefab == null){
            EditorGUILayout.HelpBox("Root transform & prefabs must be assigned!", MessageType.Warning);
        }else{
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    private void DrawButtons(){
        if(GUILayout.Button("Create Waypoint")) {
            CreateWaypoint();
        }
    }

    private void CreateWaypoint(){
        GameObject waypointObject = (GameObject) Instantiate(waypointPrefab);
        waypointObject.name = "Waypoint "+(waypointRoot.childCount+1);
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        Waypoint prevWaypoint;

        if(waypointRoot.childCount > 1){
            prevWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            prevWaypoint.nextWaypoint = waypoint;

            waypoint.transform.position = prevWaypoint.transform.position;
            waypoint.transform.forward = prevWaypoint.transform.forward;

            Selection.activeGameObject = waypointObject;
        }else{
            GameObject.FindGameObjectWithTag("Player").GetComponent<Control>().currWaypoint = waypoint;
            waypointObject.transform.position = waypointRoot.position;
            for (int i = 0; i < waypointObject.transform.childCount; i++)
            {
                if(waypointObject.transform.GetChild(i).CompareTag("Enemy"))
                    waypointObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    
    // private void CreateWaypointAfter(){
    //     GameObject waypointObject = new GameObject("Waypoint "+waypointRoot.childCount, typeof(Waypoint));
    //     waypointObject.transform.SetParent(waypointRoot, false);

    //     Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
    //     Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

    //     if(selectedWaypoint.nextWaypoint != null){
    //         waypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
    //         waypoint.nextWaypoint.prevWaypoint = waypoint;
    //     }

    //     waypoint.prevWaypoint = selectedWaypoint;
    //     selectedWaypoint.nextWaypoint = waypoint;

    //     waypoint.transform.position = selectedWaypoint.transform.position;
    //     waypoint.transform.forward = selectedWaypoint.transform.forward;

    //     waypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
    //     Selection.activeGameObject = waypointObject.gameObject;
    // }
    // private void RemoveWaypoint(){
    //     Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
    //     if(selectedWaypoint.nextWaypoint != null){
    //         selectedWaypoint.nextWaypoint.prevWaypoint = selectedWaypoint.prevWaypoint;
    //     }
    //     if(selectedWaypoint.prevWaypoint != null){
    //         selectedWaypoint.prevWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
    //         Selection.activeGameObject = selectedWaypoint.prevWaypoint.gameObject;
    //     }

    //     DestroyImmediate(selectedWaypoint.gameObject);
    // }
}
