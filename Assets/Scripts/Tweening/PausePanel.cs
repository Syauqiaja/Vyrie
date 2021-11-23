using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private float tweeningTime;
    [SerializeField] private CanvasGroup blackPanel;
    [SerializeField] private Transform mainPanel;

    public void Show(){
        Time.timeScale = 0f;
        blackPanel.alpha = 0f;
        mainPanel.position = new Vector2(mainPanel.transform.position.x, -Screen.height);
        blackPanel.gameObject.SetActive(true);
        mainPanel.gameObject.SetActive(true);
        blackPanel.LeanAlpha(1f, tweeningTime).setIgnoreTimeScale(true);
        LeanTweenExt.LeanMoveLocalY(mainPanel, -180.835f, tweeningTime)
            .setEaseOutExpo()
            .setIgnoreTimeScale(true)
            .delay = 0.1f;
    }
    public void Hide(){
        blackPanel.LeanAlpha(0f, tweeningTime).setIgnoreTimeScale(true);
        LeanTweenExt.LeanMoveLocalY(mainPanel, -Screen.height, tweeningTime)
            .setEaseInExpo()
            .setOnComplete(OnComplete)
            .setIgnoreTimeScale(true);
    }
    
    public void OnComplete(){
        mainPanel.gameObject.SetActive(false);
        blackPanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
