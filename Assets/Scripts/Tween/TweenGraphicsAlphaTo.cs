using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Tween lots of graphics alpha to. Powered by iTween.
/// Author: BoJue.
/// </summary>
namespace UnityToolbox
{
    public class TweenGraphicsAlphaTo : TweenValueFloat
    {
        public Graphic[] skip;
        private Graphic[] graphics;

        public override void Run()
        {
            graphics = this.tweenTarget.GetComponentsInChildren<Graphic>();
            base.Run();
        }

        void Update()
        {
            if (isTweening && onUpdateInvokeTimes > 0)
                ApplyAlpha();
        }

        protected override void OnComplete()
        {
            base.OnComplete();

            ApplyAlpha();
        }

        void ApplyAlpha()
        {
            if (graphics == null)
                return;

            for (int i = 0; i < graphics.Length; i++)
            {
                if (graphics[i] == null)
                {
                    continue;
                }
                else
                {
                    if (SkipThisGraphic(graphics[i]))
                    {
                        continue;
                    }
                    else
                    {
                        graphics[i].color = new Color(
                            graphics[i].color.r,
                            graphics[i].color.g,
                            graphics[i].color.b,
                            this.FloatNow
                        );
                    }
                }
            }
        }

        bool SkipThisGraphic(Graphic g)
        {
            if (g.gameObject.name.Contains("skip-tween"))
                return true;

            if (skip == null)
                return false;
            if (skip.Length <= 0)
                return false;

            for (int i = 0; i < skip.Length; i++)
            {
                if (skip[i] == null)
                    continue;
                if (skip[i] == g)
                    return true;
            }
            return false;
        }
    }
}
