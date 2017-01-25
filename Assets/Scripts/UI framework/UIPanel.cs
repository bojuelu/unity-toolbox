/// <summary>
/// A panel has BringIn(open) and Dismiss(close) behavior.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public abstract class UIPanel : UIObject
{
    protected bool isShow = true;
    public bool IsShow { get { return isShow; } }

    public virtual void BringIn()
    {
        isShow = true;
    }

    public virtual void Dismiss()
    {
        isShow = false;
    }
}
