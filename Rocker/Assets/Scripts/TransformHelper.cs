using UnityEngine;
using System.Collections;
using System;

public delegate string TransformPart(int part);
/// <summary>
///  变换组件助手类
/// </summary>
public class TransformHelper
{
    /// <summary>
    /// 根据父物体，在层级未知情况下查找子物体
    /// </summary>
    /// <param name="parentTf">父物体变换组件</param>
    /// <param name="childName">需要查找的子物体名称</param>
    /// <returns>子物体的变换组件</returns>
    public static Transform GetChild(Transform parentTf, string childName)
    {
        if (string.IsNullOrEmpty(childName))
        {
            return parentTf;
        }

        Transform childTf = parentTf.Find(childName);
        if (childTf != null)
            return childTf;
        int count = parentTf.childCount;
        for (int i = 0; i < count; i++)
        {
            childTf = GetChild(parentTf.GetChild(i), childName);
            if (childTf != null)
                return childTf;
        }
        return null;
    }

    /// <summary>
    /// 通过tag名获得子物体
    /// </summary>
    /// <param name="parentTf">父物体变换组件</param>
    /// <param name="tagName">子物体的tag名</param>
    /// <returns></returns>
    public static Transform GetChildByTag(Transform parentTf, string tagName)
    {
        for (int i = 0; i < parentTf.transform.childCount; i++)
        {
            var child = parentTf.transform.GetChild(i);
            if (child.CompareTag(tagName))
            {
                return child;
            }
        }
        return null;
    }

    public static void ChangeLayer(GameObject go, string layer, bool changeChildren)
    {
        if (go == null)
        {
            return;
        }

        if (changeChildren)
        {

            int count = go.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                var child = go.transform.GetChild(i);
                ChangeLayer(child.gameObject, layer, changeChildren);
            }
        }

        go.layer = LayerMask.NameToLayer(layer);
    }

    public static void DestroyChild(Transform parentTr, string childName)
    {
        Transform childTr = GetChild(parentTr, childName);
        if (null == childTr)
        {
            return;
        }

        UnityEngine.Object.Destroy(childTr);
    }

    public static void DestroyAllChild(Transform parentTr)
    {
        int childCount = parentTr.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Transform childTr = parentTr.GetChild(i);
            childTr.parent = null;
            GameObject.Destroy(childTr.gameObject);
        }
    }

    public static void DestroyAllChildImmediate(Transform parentTr)
    {
        int childCount = parentTr.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Transform childTr = parentTr.GetChild(i);
            childTr.parent = null;
            GameObject.DestroyImmediate(childTr.gameObject);
        }
    }

    #region
    /// <summary>
    /// 获取节点( BaseSprite 以及 BaseSprite 的子类, 调用 BaseSprite.GetParts )
    /// </summary>
    /// <param name="target"></param>
    /// <param name="parent"></param>
    /// <param name="child"></param>
    /// <returns></returns>
    public static Transform GetBodyParts(Transform target, string parent, string child)
    {
        Transform modelPart = GetBodyParts(target, parent);
        if (null == modelPart)
        {
            return null;
        }

        if (string.IsNullOrEmpty(child))
        {
            return modelPart;
        }
        Transform part = GetBodyParts(modelPart, child);
        return part;
    }

    public static Transform GetBodyParts(Transform parentTr, string parts)
    {
        return TransformHelper.GetChild(parentTr, parts);
    }
    #endregion

}
