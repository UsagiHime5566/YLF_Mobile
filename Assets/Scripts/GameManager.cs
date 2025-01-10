using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform dolphinModel;
    public List<Transform> markerRoots;
    public Transform targetRoot;
    public float angleThreshold = 10;

    public Transform View3DBase;
    Quaternion initRotation;

    bool isInit = false;

    void Start()
    {
        // 啟用陀螺儀
        Input.gyro.enabled = true;
        StartCoroutine(ShowPosition());
    }

    IEnumerator ShowPosition()
    {
        while(true) 
        {
            yield return new WaitForSeconds(0.33f);
            if(targetRoot != null)
            {
                float angle = GetAngleBetween(Vector3.zero, targetRoot.eulerAngles);    
                Debug.Log($"Angle: {angle}");

                if(angle < angleThreshold && !isInit)
                {
                    dolphinModel.gameObject.SetActive(true);
                    initRotation = GetGyroRotation();
                    isInit = true;
                }
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           
        }
        
        if(isInit)  // 確保已經初始化
        {
            // 獲取當前陀螺儀旋轉
            Quaternion currentRotation = GetGyroRotation();
            // 計算相對旋轉（當前旋轉相對於初始旋轉的變化）
            Quaternion relativeRotation = Quaternion.Inverse(initRotation) * currentRotation;
            // 應用反向旋轉到View3DBase
            View3DBase.rotation = Quaternion.Inverse(relativeRotation);
        }
    }

    public void OnMarkerFind(string markerName)
    {
        if(markerName == "面左")
        {
            targetRoot = markerRoots[0];
        }
        else if(markerName == "面中")
        {
            targetRoot = markerRoots[1];
        }
        else if(markerName == "面右")
        {
            targetRoot = markerRoots[2];
        }
    }

    private float GetAngleBetween(Vector3 euler1, Vector3 euler2)
    {
        // 將歐拉角轉換為四元數
        Quaternion rotation1 = Quaternion.Euler(euler1);
        Quaternion rotation2 = Quaternion.Euler(euler2);
        
        // 計算兩個旋轉之間的夾角
        float angle = Quaternion.Angle(rotation1, rotation2);
        
        return angle;
    }

    public Quaternion GetGyroRotation()
    {
        Quaternion gyroRotation = Input.gyro.attitude;
        Quaternion rotFix = new Quaternion(0, 0, 1, 0);
        Quaternion rot = gyroRotation * rotFix;
        return rot;
    }
}
