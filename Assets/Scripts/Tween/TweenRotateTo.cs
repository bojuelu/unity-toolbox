using UnityEngine;
using System.Collections;

/// <summary>
/// Tween rotate to. Powered by iTween.
/// Author: BoJue.
/// </summary>
namespace UnityToolbox
{
    public class TweenRotateTo : TweenBase
    {
        public bool useNowAsFrom = false;
        public Vector3 rotateFrom = Vector3.zero;
        public Vector3 rotateTo = Vector3.forward * 180;

        private iTween iTweenInstance = null;

        public void Run(Vector3 rotateFrom, Vector3 rotateTo, bool isLocal)
        {
            this.rotateFrom = rotateFrom;
            this.rotateTo = rotateTo;
            this.isLocal = isLocal;

            this.Run();
        }

        public void Run(Vector3 rotateTo, bool isLocal)
        {
            this.rotateTo = rotateTo;
            this.isLocal = isLocal;

            this.Run();
        }

        public override void Run()
        {
            base.Run();

            if (useNowAsFrom)
                rotateFrom = tweenTarget.transform.localRotation.eulerAngles;
            else
                tweenTarget.transform.localRotation = new Quaternion(
                    rotateFrom.x, rotateFrom.y, rotateFrom.z, transform.localRotation.w
                );

            tweenName = "rotateto-" + UnityUtility.GenerateRandomStringViaCharacter(8);
            iTween.RotateTo(tweenTarget,
                iTween.Hash(
                    "name", tweenName,
                    "islocal", isLocal,
                    "rotation", rotateTo,
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
