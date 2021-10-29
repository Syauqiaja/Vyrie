using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
   private static LevelHandler _instance;
   public static LevelHandler Instance {get{return _instance;}}
   public int currentIndex = 1;


   [SerializeField] private List<GameObject> levels = new List<GameObject>();
   public Level currentLevel;
   public Level nextLevel;
   private Level oldLevel;
   private Control playerControl;


   private void Awake() {
       if(_instance == null) _instance = this;
       else if(_instance != this) Destroy(gameObject);
       playerControl = GameObject.FindWithTag("Player").GetComponent<Control>();

       currentLevel =  Instantiate(levels[currentIndex-1], Vector3.zero, Quaternion.identity).GetComponent<Level>();

      nextLevel = Instantiate(levels[currentIndex]).GetComponent<Level>();
      nextLevel.transform.position = currentLevel.last.transform.position -
                              nextLevel.first.transform.localPosition +
                              new Vector3(Random.RandomRange(1f,2f), Random.RandomRange(3f,5f), 0f);
      currentLevel.last.nextWaypoint = nextLevel.first;
      playerControl.currWaypoint = currentLevel.first;
   }

   public void DeployLevel(){
      if(oldLevel != null) Destroy(oldLevel.gameObject);
      oldLevel = currentLevel;
      currentLevel = nextLevel;
      currentIndex++;
      nextLevel = Instantiate(levels[currentIndex]).GetComponent<Level>();
      nextLevel.transform.position = currentLevel.last.transform.position -
                              nextLevel.first.transform.localPosition +
                              new Vector3(Random.RandomRange(-1f,1f), 4f, 0f);
      currentLevel.last.nextWaypoint = nextLevel.first;
   }

}
