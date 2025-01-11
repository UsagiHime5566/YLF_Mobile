using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Playables;

public class GameUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject UI_Tip;
    public GameObject UI_Ready;
    public CanvasGroup WorkPanel;
    public Button BTN_Shoot;
    public CanvasGroup WaitPanel;
    public Image Image_WaterValue;

    [Header("Game Objects")]
    public GameManager gameManager;
    public AccessIOT accessIOT;
    public DolphinAnim dolphinAnim;

    [Header("CD 線上時間")]
    public float cdTime_online = 90f;
    private float nextCDTime_online = 3f;
    

    int lastIOTIndex = -1;
    
    void Start()
    {
        UI_Tip.SetActive(true);
        UI_Ready.SetActive(false);
        WorkPanel.alpha = 0;

        gameManager.OnDolphinReady += OnDolphinReady;
        accessIOT.OnIOTData += OnIOTData;

        StartCoroutine(CheckCDTime());
    }

    [ContextMenu("OnDolphinReady")]
    void OnDolphinReady()
    {
        UI_Tip.SetActive(false);
        UI_Ready.SetActive(true);
        WorkPanel.alpha = 1;
        dolphinAnim.SetPlayEnd();
    }
    
    [ContextMenu("OnIOTDataTest 1   ")]
    void OnIOTDataTest1()
    {
        OnIOTData(1);
    }

    [ContextMenu("OnIOTDataTest 0")]
    void OnIOTDataTest0()
    {
        OnIOTData(0);
    }

    void OnIOTData(int index)
    {
        if(lastIOTIndex == index) return;
        lastIOTIndex = index;

        if(index == 1){
            nextCDTime_online = Time.time + cdTime_online;
            dolphinAnim.PlayTimeline();
            WaitPanel.DOFade(1, 1f);
            BTN_Shoot.gameObject.SetActive(false);
        }

        if(index == 0){
            WaitPanel.DOFade(0, 2f);
            BTN_Shoot.gameObject.SetActive(true);
        }
    }

    IEnumerator CheckCDTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);

            float amount = (nextCDTime_online - Time.time) / cdTime_online;
            Image_WaterValue.fillAmount = amount > 1 ? 1 : 1 - amount;  
        }
    }
}
