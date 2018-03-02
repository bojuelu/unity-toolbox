using UnityEngine;
using System.Collections;

/// <summary>
/// Tween value color. Powered by iTween.
/// Author: BoJue.
/// </summary>
namespace UnityToolbox
{
    public class TweenValueColor : TweenBase
    {
        public Color colorFrom = Color.black;
        public Color colorTo = Color.white;
        private Color colorNow = Color.black;
        public Color ColorNow
        {
            get { return colorNow; }
            set { colorNow = value; }
        }

        protected int onUpdateInvokeTimes = 0;

        private iTween iTweenInstance = null;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Run(Color colorFrom, Color colorTo)
        {
            this.colorFrom = colorFrom;
            this.colorTo = colorTo;

            Run();
        }

        public override void Run()
        {
            base.Run();

            onUpdateInvokeTimes = 0;

            colorNow = colorFrom;

            tweenName = "valuecolorto-" + UnityUtility.GenerateRandomString(8);
            iTween.ValueTo(tweenTarget,
                iTween.Hash(
                    "name", tweenName,
                    "from", colorFrom,
                    "to", colorTo,
                    "time", duration,
                    "delay", delay,
                    "easeType", ease.ToString(),
                    "loopType", loop,
                    "onupdate", recvCallback.OnUpdateColorFuncName,
                    "onupdatetarget", recvCallback.gameObject,
                    "oncomplete", recvCallback.OnCompleteFuncName,
                    "oncompletetarget", recvCallback.gameObject,
                    "ignoretimescale", ignoreTimeScale
                )
            );

            recvCallback.onUpdateColorEvent -= this.OnUpdate;
            recvCallback.onUpdateColorEvent += this.OnUpdate;
        }

        public override void Pause()
        {
            if (iTweenInstance)
                iTweenInstance.enabled = false;
        }

        public override void Resume()
        {
            if (iTweenInstance)
                iTweenInstance.enabled = true;
        }

        private void OnUpdate(Color c)
        {
            colorNow = c;
            onUpdateInvokeTimes++;
        }
    }
}
