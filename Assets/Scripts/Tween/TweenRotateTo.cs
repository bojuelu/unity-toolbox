/// <summary>
/// Tween rotate to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenRotateTo : TweenBase
{
    public bool useNowAsFrom = false;
    public Vector3 rotateFrom = Vector3.zero;
    public Vector3 rotateTo = Vector3.forward * 180;

    protected override void Awake()
    {
        base.Awake();
        base.tweenType = "rotateto";
    }

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
            rotateFrom = this.transform.localRotation.eulerAngles;
        else
            this.transform.localRotation = new Quaternion(
                rotateFrom.x, rotateFrom.y, rotateFrom.z, transform.localRotation.w
            );
        
        iTween.RotateTo(base.tweenTarget,
            iTween.Hash(
                "name", base.tweenType,
                "islocal", base.isLocal,
                "rotation", this.rotateTo,
                "time", base.duration,
                "delay", base.delay,
                "easeType", base.Ease.ToString(),
                "loopType", base.Loop,
                "ignoretimescale", base.ignoreTimeScale,
                "oncomplete", base.Callback.OnCompleteFuncName,
                "oncompletetarget", base.Callback.gameObject
            )
        );
    }
}
