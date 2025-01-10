using UnityEngine;
using System;

public class WebSave
{
    private static WebSave instance;
    public static WebSave Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WebSave();
            }
            return instance;
        }
    }

    private const string SAVE_KEY_PREFIX = "SAVE_";

    public void SaveData<T>(string key, T data)
    {
        string saveKey = SAVE_KEY_PREFIX + key;
        
        // 处理基本数据类型和常用简单类型
        switch (data)
        {
            case string stringValue:
                PlayerPrefs.SetString(saveKey, stringValue);
                break;
            case int intValue:
                PlayerPrefs.SetInt(saveKey, intValue);
                break;
            case float floatValue:
                PlayerPrefs.SetFloat(saveKey, floatValue);
                break;
            case double doubleValue:
                PlayerPrefs.SetString(saveKey, doubleValue.ToString());
                break;
            case bool boolValue:
                PlayerPrefs.SetInt(saveKey, boolValue ? 1 : 0);
                break;
            case long longValue:
                PlayerPrefs.SetString(saveKey, longValue.ToString());
                break;
            default:
                // 其他复杂类型使用 JSON 序列化
                string jsonData = JsonUtility.ToJson(data);
                PlayerPrefs.SetString(saveKey, jsonData);
                break;
        }
        PlayerPrefs.Save();
    }

    public T GetData<T>(string key, T defaultValue = default)
    {
        string saveKey = SAVE_KEY_PREFIX + key;
        if (!PlayerPrefs.HasKey(saveKey))
        {
            return defaultValue;
        }

        Type type = typeof(T);
        try
        {
            // 处理基本数据类型和常用简单类型
            if (type == typeof(string))
                return (T)(object)PlayerPrefs.GetString(saveKey);
            if (type == typeof(int))
                return (T)(object)PlayerPrefs.GetInt(saveKey);
            if (type == typeof(float))
                return (T)(object)PlayerPrefs.GetFloat(saveKey);
            if (type == typeof(double))
                return (T)(object)double.Parse(PlayerPrefs.GetString(saveKey));
            if (type == typeof(bool))
                return (T)(object)(PlayerPrefs.GetInt(saveKey) == 1);
            if (type == typeof(long))
                return (T)(object)long.Parse(PlayerPrefs.GetString(saveKey));

            // 其他复杂类型使用 JSON 反序列化
            string jsonData = PlayerPrefs.GetString(saveKey);
            return JsonUtility.FromJson<T>(jsonData);
        }
        catch
        {
            return defaultValue;
        }
    }
}
