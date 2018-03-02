using UnityEngine;
using System.Collections;

/// <summary>
/// Tween value vector2. Powered by iTween.
/// Author: BoJue.
/// </summary>
namespace UnityToolbox
{
    public class TweenValueVector2 : TweenBase
    {
        public Vector2 vectorFrom = Vector3.zero;
        public Vector2 vectorTo = Vector3.up * 10;
        private Vector2 vectorNow = Vector3.zero;
        public Vector2 VectorNow
        {
            get { return vectorNow; }
            set { vectorNow = value; }
        }

        protected int onUpdateInvokeTimes = 0;

        private iTween iTweenInstance = null;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Run(Vector2 vectorFrom, Vector2 vectorTo)
        {
            this.vectorFrom = vectorFrom;
            this.vectorTo = vectorTo;

            Run();
        }

        public override void Run()
        {
            base.Run();

            onUpdateInvokeTimes = 0;

            vectorNow = vectorFrom;

            tweenName = "valuevector2to-" + UnityUtility.GenerateRandomStringViaCharacter(8);
            iTween.ValueTo(tweenTarget,
                iTween.Hash(
                    "name", tweenName,
                    "from", vectorFrom,
                    "to", vectorTo,
                    "time", duration,
                    "delay", delay,
                    "easeType", ease.ToString(),
                    "loopType", loop,
                    "onupdate", recvCallback.OnUpdateVector2FuncName,
                    "onupdatetarget", recvCallback.gameObject,
                    "oncomplete", recvCallback.OnCompleteFuncName,
                    "oncompletetarget", recvCallback.gameObject,
                    "ignoretimescale", ignoreTimeScale
                )
            );

            recvCallback.onUpdateVector2Event -= this.OnUpdate;
            recvCallback.onUpdateVector2Event += this.OnUpdate;
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

        private void OnUpdate(Vector2 vec2)
        {
            vectorNow = vec2;
            onUpdateInvokeTimes++;
        }
    }
}
