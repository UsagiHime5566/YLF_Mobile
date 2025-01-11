using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class PostGoogle : MonoBehaviour
{
    [Header("Google Post")]
    public string googleSheetUrl;
    public List<PostData> postData;

    public async void Post()
    {
        WWWForm wWWForm = new WWWForm();
        
        foreach(var data in postData)
        {
            wWWForm.AddField("entry." + data.entry, data.msg);
        }
        using UnityWebRequest www = UnityWebRequest.Post(googleSheetUrl, wWWForm);
        
        // 添加必要的請求頭
        www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        www.SetRequestHeader("Access-Control-Allow-Origin", "*");
        
        UnityWebRequestAsyncOperation asyncOperation = www.SendWebRequest();
        while (!asyncOperation.isDone)
        {
            await Task.Delay(100);
        }

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
    }

    [Serializable]
    public class PostData
    {
        public string entry;
        public string msg;
    }
}