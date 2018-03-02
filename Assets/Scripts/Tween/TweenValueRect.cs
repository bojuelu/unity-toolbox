using UnityEngine;
using System.Collections;

/// <summary>
/// Tween value rect. Powered by iTween.
/// Author: BoJue.
/// </summary>
namespace UnityToolbox
{
    public class TweenValueRect : TweenBase
    {
        public Rect rectFrom = new Rect(0, 0, 0, 0);
        public Rect rectTo = new Rect(1, 1, 1, 1);
        private Rect rectNow = new Rect(0, 0, 0, 0);
        public Rect RectNow
        {
            get { return rectNow; }
            set { rectNow = value; }
        }

        protected int onUpdateInvokeTimes = 0;

        private iTween iTweenInstance = null;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Run(Rect rectFrom, Rect rectTo)
        {
            this.rectFrom = rectFrom;
            this.rectTo = rectTo;

            Run();
        }

        public override void Run()
        {
            base.Run();

            onUpdateInvokeTimes = 0;

            rectNow = rectFrom;

            tweenName = "valuerectto-" + UnityUtility.GenerateRandomStringViaCharacter(8);
            iTween.ValueTo(tweenTarget,
                iTween.Hash(
                    "name", tweenName,
                    "from", rectFrom,
                    "to", rectTo,
                    "time", duration,
                    "delay", delay,
                    "easeType", ease.ToString(),
                    "loopType", loop,
                    "onupdate", recvCallback.OnUpdateRectFuncName,
                    "onupdatetarget", recvCallback.gameObject,
                    "oncomplete", recvCallback.OnCompleteFuncName,
                    "oncompletetarget", recvCallback.gameObject,
                    "ignoretimescale", ignoreTimeScale
                )
            );

            recvCallback.onUpdateRectEvent -= this.OnUpdate;
            recvCallback.onUpdateRectEvent += this.OnUpdate;
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

        private void OnUpdate(Rect rect)
        {
            rectNow = rect;
            onUpdateInvokeTimes++;
        }
    }
}
