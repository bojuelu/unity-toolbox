using UnityEngine;
using UnityEngine.UI;

namespace UnityToolbox
{
    /// <summary>
    /// Button object swap.
    /// Author: BoJue.
    /// </summary>
    public class ButtonObjectSwap : Button
    {
        public GameObject normal;
        public GameObject highlighted;
        public GameObject pressed;
        public GameObject disabled;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            switch (state)
            {
                case Selectable.SelectionState.Normal:
                    normal.SetActive(true);
                    highlighted.SetActive(false);
                    pressed.SetActive(false);
                    disabled.SetActive(false);
                    break;
                case Selectable.SelectionState.Highlighted:
                    normal.SetActive(false);
                    highlighted.SetActive(true);
                    pressed.SetActive(false);
                    disabled.SetActive(false);
                    break;
                case Selectable.SelectionState.Pressed:
                    normal.SetActive(false);
                    highlighted.SetActive(false);
                    pressed.SetActive(true);
                    disabled.SetActive(false);
                    break;
                case Selectable.SelectionState.Disabled:
                    normal.SetActive(false);
                    highlighted.SetActive(false);
                    pressed.SetActive(false);
                    disabled.SetActive(true);
                    break;
                default:
                    normal.SetActive(true);
                    highlighted.SetActive(false);
                    pressed.SetActive(false);
                    disabled.SetActive(false);
                    break;
            }
        }
    }
}
