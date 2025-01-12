using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Text CurrentMarkerText;
    public Button BTN_SavePosition;
    public Button BTN_Shoot;

    [Header("Model")]
    public Transform dolphinModel;
    public List<Transform> markerRoots;
    public Transform targetRoot;
    public float angleThreshold = 10;

    public Transform View3DBase;
    public AccessGoogleSheet accessGoogleSheet;
    public AccessIOT accessIOT;
    Quaternion initRotation;

    bool isInit = false;
    public int currentMarkerIndex = 0;

    public System.Action OnDolphinReady;

    void Start()
    {
        // 啟用陀螺儀
        Input.gyro.enabled = true;
        StartCoroutine(ShowPosition());
        BTN_SavePosition?.onClick.AddListener(OnSavePositionClick);
        BTN_Shoot?.onClick.AddListener(OnShootClick);
    }

    IEnumerator ShowPosition()
    {
        while(true) 
        {
            yield return new WaitForSeconds(0.33f);
            if(targetRoot != null)
            {
                float angle = GetAngleBetween(Vector3.zero, targetRoot.eulerAngles);    
                //Debug.Log($"Angle: {angle}");

                if(angle < angleThreshold && !isInit)
                {
                    isInit = true;
                    initRotation = GetGyroRotation();

                    accessGoogleSheet.GetDolphinPosition(currentMarkerIndex, (position, rx, scale) => {
                        dolphinModel.localPosition = position;
                        dolphinModel.localRotation = Quaternion.Euler(rx, 0, 0);
                        dolphinModel.localScale = new Vector3(scale, scale, scale);
                        dolphinModel.gameObject.SetActive(true);
                        OnDolphinReady?.Invoke();
                    });
                }
            }
        }
    }

    void OnSavePositionClick()
    {
        accessGoogleSheet.SetDolphinPosition(currentMarkerIndex, dolphinModel.localPosition, dolphinModel.localRotation.eulerAngles.x, dolphinModel.localScale.x);
    }

    void OnShootClick()
    {
        BTN_Shoot.interactable = false;
        accessIOT.CallIOTSet_JavaScript();
        StartCoroutine(EnableShootButton());
    }

    private IEnumerator EnableShootButton()
    {
        yield return new WaitForSeconds(3);
        BTN_Shoot.interactable = true;
    }

    void Update()
    {
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
            currentMarkerIndex = 0;
            if(CurrentMarkerText) CurrentMarkerText.text = "面左";
        }
        else if(markerName == "面中")
        {
            targetRoot = markerRoots[1];
            currentMarkerIndex = 1;
            if(CurrentMarkerText) CurrentMarkerText.text = "面中";
        }
        else if(markerName == "面右")
        {
            targetRoot = markerRoots[2];
            currentMarkerIndex = 2;
            if(CurrentMarkerText) CurrentMarkerText.text = "面右";
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
