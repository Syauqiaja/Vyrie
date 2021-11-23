using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Control PlayerControl;
    private int hp;
    public int health{
        get{return hp;}
        set{
            if(hp < 4 || value < 0 ){
                UIHandler.Instance.SetHealth(hp,value);
                Debug.Log("HP = " + hp);
                hp = value;
            }
        }
    }
    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
    }
    
    private void Start() {
        health = 1;
        PlayerControl = GameObject.FindWithTag("Player").GetComponent<Control>();
    }
}
