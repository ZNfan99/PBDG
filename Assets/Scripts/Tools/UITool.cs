using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UITool
{
    private static GameObject m_CanvasObj = null;//画布上的2D画布物件

    public static void ReleaseCanvas()
    {
        m_CanvasObj = null;
    }

    //找寻限定在Canvas画布下的UI界面
    public static GameObject FindUIGameObject(string UIName)
    {
        if (m_CanvasObj == null)
        {
            m_CanvasObj = UnityTool.FindGameObject("Canvas");
        }
        if(m_CanvasObj == null) { 
        return null;
        }
        return UnityTool.FindChildGameObject(m_CanvasObj, UIName);
    }

    //取得UI组件
    public static T GetUIComponent<T>(GameObject Container, string UIName) where T : UnityEngine.Component
    {
        // 找出子物件 
        GameObject ChildGameObject = UnityTool.FindChildGameObject(Container, UIName);
        if (ChildGameObject == null)
            return null;

        T tempObj = ChildGameObject.GetComponent<T>();
        if (tempObj == null)
        {
            Debug.LogWarning("元件[" + UIName + "]不是[" + typeof(T) + "]");
            return null;
        }
        return tempObj;
    }

    public static Button GetButton(string BtnName)
    {
        //取得Canvas
        GameObject UIRoot = GameObject.Find("Canvas");
        if(UIRoot == null)
        {
            Debug.LogWarning("場景上沒有UI Canvas");
            return null;
        }

        //找出对应的Button
        Transform[] allChildren = UIRoot.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.name == BtnName)
            {
                Button tmpBtn = child.gameObject.GetComponent<Button>();
                if (tmpBtn == null)
                    Debug.LogWarning("UI原件[" + BtnName + "]不是Button");
                return tmpBtn;
            }
        }
        Debug.LogWarning("UI Canvas中沒有Button[" + BtnName + "]存在");
        return null;
    }

    // 取得UI元件
    public static T GetUIComponent<T>(string UIName) where T : UnityEngine.Component
    {
        // 取得Canvas
        GameObject UIRoot = GameObject.Find("Canvas");
        if (UIRoot == null)
        {
            Debug.LogWarning("場景上沒有UI Canvas");
            return null;
        }
        return GetUIComponent<T>(UIRoot, UIName);
    }
}