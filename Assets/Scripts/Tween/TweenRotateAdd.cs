using UnityEngine;
using System.Collections;

/// <summary>
/// Tween rotate add. Powered by iTween.
/// Author: BoJue.
/// </summary>
namespace UnityToolbox
{
    public class TweenRotateAdd : TweenBase
    {
        public Vector3 rotateAmount = new Vector3(90, 90, 90);

        private iTween iTweenInstance = null;

        public override void Run()
        {
            base.Run();

            Space space = Space.World;
            if (isLocal)
                space = Space.Self;

            tweenName = "rotateadd-" + UnityUtility.GenerateRandomString(8);
            iTween.RotateAdd(tweenTarget,
                iTween.Hash(
                    "name", tweenName,
                    "space", space,
                    "amount", rotateAmount,
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
