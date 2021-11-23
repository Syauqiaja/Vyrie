using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance;

    public Transform defeatPanel;
    public List<GameObject> healthImages = new List<GameObject>();
    private void Awake() {
        if(Instance == null) Instance = this;
        defeatPanel.gameObject.SetActive(false);
        defeatPanel.transform.localScale = Vector3.zero;
    }
    public void SetHealth(int current,int health){
        for(int i = 0; i < health; i++){
            healthImages[i].SetActive(true);
        }
        for (int i = health; i < 4; i++)
        {
            healthImages[i].SetActive(false);
        }
    }
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        Time.timeScale = 1f;
        GameManager.Instance.health = 1;
    }

    public void ShowDefeat(){
        defeatPanel.gameObject.SetActive(true);
        defeatPanel.LeanScale(Vector3.one, .7f).setEaseOutExpo();
    }
    public void Home(){
        
    }
}
