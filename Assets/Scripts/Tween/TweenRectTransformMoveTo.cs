using UnityEngine;
using System.Collections;

/// <summary>
/// Tween rect transform move to. Powered by iTween.
/// Author: BoJue.
/// </summary>
namespace UnityToolbox
{
    public class TweenRectTransformMoveTo : TweenValueVector3
    {
        public bool useNowAsFrom = false;

        public override void Run()
        {
            if (useNowAsFrom)
            {
                Vector3 nowPos = tweenTarget.GetComponent<RectTransform>().anchoredPosition3D;
                vectorFrom = nowPos;
            }

            base.Run();
        }

        void Update()
        {
            if (isTweening && onUpdateInvokeTimes > 0)
                ApplyPosition();
        }

        protected override void OnComplete()
        {
            base.OnComplete();

            ApplyPosition();
        }

        void ApplyPosition()
        {
            tweenTarget.GetComponent<RectTransform>().anchoredPosition3D = this.VectorNow;
        }
    }
}
