using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class AccessGoogleSheet : MonoBehaviour
{
    public string url = @"https://script.google.com/macros/s/AKfycbyWHZD4pck6ldbmmIvAkx2wxOJovGwGWrob1-id1sdGtoeHLJc6xJqxOOCXWWVonCBZKw/exec";
    public string url_param = "?id={0}";
    public string url_post = "?id={0}&x={1}&y={2}&z={3}&rx={4}";
    public GameObject OK_Message;
    public string IOTBaseUrl = "ylf2025.iottalk.tw/signal";

    public void SetDolphinPosition(int id, Vector3 position, float rx)
    {
        StartCoroutine(SetDolphinPositionCoroutine(id, position, rx));
    }

    public void GetDolphinPosition(int id, Action<Vector3> callback)
    {
        StartCoroutine(GetDolphinPositionCoroutine(id, callback));
    }

    public void SendIOT()
    {
        StartCoroutine(SendIOTCoroutine());
    }

    private IEnumerator SetDolphinPositionCoroutine(int id, Vector3 position, float rx)
    {
        string fullUrl = url + string.Format(url_post, id, position.x, position.y, position.z, rx);
        Debug.Log("發送請求URL: " + fullUrl);
        UnityWebRequest request = UnityWebRequest.Get(fullUrl);
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        yield return request.SendWebRequest();

        try
        {
            if (request.result == UnityWebRequest.Result.Success)
            {
                string urlResponse = request.downloadHandler.text;
                Debug.Log("Response: " + urlResponse);
                if(OK_Message) OK_Message.SetActive(true);
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error: {e.Message}");
        }
        finally
        {
            request.Dispose();
        }
        yield return new WaitForSeconds(2);
        if(OK_Message) OK_Message.SetActive(false);
    }

    public IEnumerator GetDolphinPositionCoroutine(int id, Action<Vector3> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url + string.Format(url_param, id));
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        yield return request.SendWebRequest();

        Vector3 position = new Vector3(0, -2, 10);
        try
        {
            if (request.result == UnityWebRequest.Result.Success)
            {
                string urlResponse = request.downloadHandler.text;
                Debug.Log("Response: " + urlResponse);
                string[] s = urlResponse.Split(',');
                position = new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error: {e.Message}");
        }
        finally
        {
            request.Dispose();
        }

        callback(position);
    }

    IEnumerator SendIOTCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(url + "?iot=1");
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        yield return request.SendWebRequest();

        try
        {
            if (request.result == UnityWebRequest.Result.Success)
            {
                string urlResponse = request.downloadHandler.text;
                Debug.Log("Response: " + urlResponse);
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error: {e.Message}");
        }
        finally
        {
            request.Dispose();
        }
    }



    public void SendIntSignal(int signal)
    {
        string url = "https://" + IOTBaseUrl;
        Debug.Log(url);
        StartCoroutine(SendIntRequest(url, signal));
    }

    private IEnumerator SendIntRequest(string url, int signal)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            // 直接將整數轉換為字串後轉為位元組
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(signal.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "text/plain");  // 改為純文字格式
            request.SetRequestHeader("Authorization", "Bearer fa1a4509915146fdb07ae71645b561cf");
            request.SetRequestHeader("Access-Control-Allow-Origin", "*");
            request.useHttpContinue = false;
            
            yield return request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("發送請求失敗: " + request.error);
            }
            else
            {
                Debug.Log("請求發送成功: " + request.downloadHandler.text);
            }
        }
    }
}
