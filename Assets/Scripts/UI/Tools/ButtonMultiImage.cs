using UnityEngine;
using UnityEngine.UI;
using System;

namespace UnityToolbox
{
    /// <summary>
    /// Make multi-image trasnition.
    /// Author: Riposte. website: http://answers.unity3d.com/users/254200/riposte.html
    /// Refrence: http://answers.unity3d.com/questions/820311/ugui-multi-image-button-transition.html
    /// </summary>
    public class ButtonMultiImage : Button
    {
        private Graphic[] m_graphics;
        protected Graphic[] Graphics
        {
            get
            {
                if (m_graphics == null)
                {
                    m_graphics = targetGraphic.transform.GetComponentsInChildren<Graphic>();
                }
                return m_graphics;
            }
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            Color color;
            switch (state)
            {
                case Selectable.SelectionState.Normal:
                    color = this.colors.normalColor;
                    break;
                case Selectable.SelectionState.Highlighted:
                    color = this.colors.highlightedColor;
                    break;
                case Selectable.SelectionState.Pressed:
                    color = this.colors.pressedColor;
                    break;
                case Selectable.SelectionState.Disabled:
                    color = this.colors.disabledColor;
                    break;
                default:
                    color = Color.black;
                    break;
            }
            if (base.gameObject.activeInHierarchy)
            {
                switch (this.transition)
                {
                    case Selectable.Transition.ColorTint:
                        ColorTween(color * this.colors.colorMultiplier, instant);
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private void ColorTween(Color targetColor, bool instant)
        {
            if (this.targetGraphic == null)
            {
                return;
            }

            foreach (Graphic g in this.Graphics)
            {
                if (g)
                    g.CrossFadeColor(targetColor, (!instant) ? this.colors.fadeDuration : 0f, true, true);
            }
        }
    }
}
