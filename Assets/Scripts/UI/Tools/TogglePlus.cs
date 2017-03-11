/// <summary>
/// Instead of UGUI Toggle isOn using one graphic show or not show. Here use two graphic switch each other.
/// Author: BuJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TogglePlus : Toggle
{
    public Graphic onImage;
    public Graphic offImage;

    void Update()
    {
        if (graphic != null)
            graphic.enabled = false;

        if (onImage != null)
            onImage.enabled = isOn;
        if (offImage != null)
            offImage.enabled = !isOn;
    }
}
