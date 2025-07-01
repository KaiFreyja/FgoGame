using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config
{
#if UNITY_EDITOR
    public static string IP = "http://192.168.0.170";
#else
    public static string IP = "http://35.185.174.5";
#endif
    public static string API_DOMAIN
    {
        get
        {
            return IP + "/api/";
        }
    }

    public static bool IS_UI_ASSEST_BUNDLE = false;

    /// <summary>
    /// 職階路徑
    /// </summary>
    public static string RESOURCE_PROFESSION_PATH = "icon_profession/";

    /// <summary>
    /// 頭像路徑
    /// </summary>
    public static string RESOURCE_HEAD_PATH = "role_head/";

    /// <summary>
    /// 卡片路徑
    /// </summary>
    public static string RESOURCE_CARD_PATH = "card/";
}
