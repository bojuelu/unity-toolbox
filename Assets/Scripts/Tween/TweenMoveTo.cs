using UnityEngine;
using System.Collections;

/// <summary>
/// Tween move to. Powered by iTween.
/// Author: BoJue.
/// </summary>
namespace UnityToolbox
{
    public class TweenMoveTo : TweenBase
    {
        public bool useNowAsFrom = false;
        public Vector3 moveFrom = Vector3.zero;
        public Vector3 moveTo = Vector3.up * 10;

        private iTween iTweenInstance = null;

        public void Run(Vector3 moveFrom, Vector3 moveTo, bool isLocal = true)
        {
            this.moveFrom = moveFrom;
            this.moveTo = moveTo;
            this.isLocal = isLocal;

            this.Run();
        }

        public override void Run()
        {
            base.Run();

            if (useNowAsFrom)
                moveFrom = tweenTarget.transform.localPosition;
            else
                tweenTarget.transform.localPosition = moveFrom;

            tweenName = "moveto-" + UnityUtility.GenerateRandomStringViaCharacter(8);
            iTween.MoveTo(tweenTarget,
                iTween.Hash(
                    "name", tweenName,
                    "islocal", isLocal,
                    "position", moveTo,
                    "time", duration,
                    "delay", delay,
                    "easeType", ease.ToString(),
                    "loopType", loop,
                    "ignoretimescale", ignoreTimeScale,
                    "oncomplete", recvCallback.OnCompleteFuncName,
                    "oncompletetarget", recvCallback.gameObject
                )
            );
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
    }
}
