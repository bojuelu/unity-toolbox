using UnityEngine;
using System.Collections;

/// <summary>
/// Tween value float. Powered by iTween.
/// Author: BoJue.
/// </summary>
namespace UnityToolbox
{
    public class TweenValueFloat : TweenBase
    {
        public float floatFrom = 0f;
        public float floatTo = 1f;
        private float floatNow = 0f;
        public float FloatNow
        {
            get { return floatNow; }
            set { floatNow = value; }
        }

        protected int onUpdateInvokeTimes = 0;

        private iTween iTweenInstance = null;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Run(float floatFrom, float floatTo)
        {
            this.floatFrom = floatFrom;
            this.floatTo = floatTo;

            Run();
        }

        public override void Run()
        {
            base.Run();

            onUpdateInvokeTimes = 0;

            floatNow = floatFrom;

            tweenName = "valuefloatto-" + UnityUtility.GenerateRandomStringViaCharacter(8);
            iTween.ValueTo(tweenTarget,
                iTween.Hash(
                    "name", tweenName,
                    "from", floatFrom,
                    "to", floatTo,
                    "time", duration,
                    "delay", delay,
                    "easeType", ease.ToString(),
                    "loopType", loop,
                    "onupdate", recvCallback.OnUpdateFloatFuncName,
                    "onupdatetarget", recvCallback.gameObject,
                    "oncomplete", recvCallback.OnCompleteFuncName,
                    "oncompletetarget", recvCallback.gameObject,
                    "ignoretimescale", ignoreTimeScale
                )
            );

            recvCallback.onUpdateFloatEvent -= this.OnUpdate;
            recvCallback.onUpdateFloatEvent += this.OnUpdate;
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

        private void OnUpdate(float f)
        {
            floatNow = f;
            onUpdateInvokeTimes++;
        }
    }
}
