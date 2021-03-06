using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : MonoBehaviour
{
    private static GameAsset _instance;
    public static GameAsset Instance {get{return _instance;}}

    public Sprite ringSprite;

    public GameObject coin;
    public GameObject hati;
    public GameObject sepatu;

    private void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
}
