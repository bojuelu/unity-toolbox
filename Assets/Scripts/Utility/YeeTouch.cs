using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityToolbox
{
    public class YeeTouch : MonoBehaviour
    {
        public struct Finger
        {
            public bool valid;
            public Vector2 startPosition;
            public float touchedTime;
            public TouchPhase phaseLast;
            public TouchPhase phaseNow;
            public Touch touch;
        }

        Finger[] fingers = new Finger[20];
        public Finger[] Fingers { get { return fingers; } }

        public struct TwoFingers
        {
            public bool valid;
            public float initialTwoFingersDistance;
            public float lastTwoFingersDistance;
            public Vector2 deltaPosition;
            public float deltaPinch;
            public float deltaTwist;
        }
        TwoFingers twoFingers;

        //public enum Finger

        /*
            Began,
            Moved,
            Stationary,
            Ended,
            Canceled
        */

        int lastTouchCount = 0;
        Timer canInvokeOneFingerEventTimer = null;
        float canInvokeOneFingerEventMustAbove = 0.2f;

        public delegate void OneFingerHandler(Finger f0);
        public delegate void TwoFingerHandler(Finger f0, Finger f1, TwoFingers twoF);

        public event OneFingerHandler onTouch;
        public event OneFingerHandler onTouchHold;
        public event OneFingerHandler onTouchUp;
        public event OneFingerHandler onTap;
        public event OneFingerHandler onDrag;

        public event TwoFingerHandler onDragTwoFingers;
        public event TwoFingerHandler onPinchTwoFingers;
        public event TwoFingerHandler onTwistTwoFingers;

        float DragGap()
        {
            float f = 1000f;
            float result = ((float)Screen.width / f) + ((float)Screen.height / f);
            return result;
        }

        bool IsTwoFingersDeltaPositionVeryClose(Vector2 f0DeltaPos, Vector2 f1DeltaPos)
        {
            float diffX = Mathf.Abs(f0DeltaPos.x - f1DeltaPos.x);
            float diffY = Mathf.Abs(f0DeltaPos.y - f1DeltaPos.y);

            float f = 1000f;
            float gap = ((float)Screen.width / f) + ((float)Screen.height / f);

            return (diffX <= gap) & (diffY <= gap);
        }

        bool IsTwoFingersKeepSameDistance(float keepThisDistance, Vector2 f0Position, Vector2 f1Position)
        {
            float twoFingersDistance = (f0Position - f1Position).magnitude;
            float diffValue = Mathf.Abs(keepThisDistance - twoFingersDistance);

            float f = 100f;
            float gap = ((float)Screen.width / f) + ((float)Screen.height / f);

            return (diffValue <= gap);
        }

        void Awake()
        {
        }

        void Start()
        {
            canInvokeOneFingerEventTimer = new Timer();
        }

        void Update()
        {
            Touch[] touches = Input.touches;
            int touchCount = touches.Length;
            if (touchCount != lastTouchCount)
            {
                for (int i = 0; i < fingers.Length; i++)
                {
                    fingers[i].valid = false;
                }

                for (int i = 0; i < touchCount; i++)
                {
                    fingers[i].valid = true;
                    fingers[i].startPosition = touches[i].position;
                    fingers[i].touchedTime = 0f;
                    fingers[i].phaseLast = TouchPhase.Canceled;
                    fingers[i].phaseNow = touches[i].phase;
                    fingers[i].touch = touches[i];
                }

                lastTouchCount = touchCount;
            }
            else
            {
                for (int i = 0; i < touchCount; i++)
                {
                    fingers[i].touchedTime += Time.deltaTime;
                    fingers[i].touch = touches[i];

                    if (fingers[i].phaseNow != fingers[i].touch.phase)
                    {
                        fingers[i].phaseLast = fingers[i].phaseNow;
                        fingers[i].phaseNow = fingers[i].touch.phase;
                    }
                }
            }

            // check one finger gesture occur
            if (touchCount == 1 && canInvokeOneFingerEventTimer.GetTime() >= canInvokeOneFingerEventMustAbove)
            {
                // touch condiction
                if (fingers[0].phaseNow == TouchPhase.Began && fingers[0].phaseLast != TouchPhase.Began)
                {
                    if (onTouch != null)
                        onTouch(fingers[0]);

                    Debug.Log(
                        string.Format(
                            "[onTouch] fingers[0].phaseLast={0}, fingers[0].phaseNow={1}, fingers[0].touch.position={2}",
                            fingers[0].phaseLast, fingers[0].phaseNow, fingers[0].touch.position
                        )
                    );
                }

                // touch hold condiction
                if (fingers[0].phaseNow == TouchPhase.Stationary)
                {
                    if (onTouchHold != null)
                        onTouchHold(fingers[0]);

                    Debug.Log(
                        string.Format(
                            "[onTouchHold] fingers[0].phaseLast={0}, fingers[0].phaseNow={1}, fingers[0].touch.position={2}",
                            fingers[0].phaseLast, fingers[0].phaseNow, fingers[0].touch.position
                        )
                    );
                }
                else if (fingers[0].phaseNow == TouchPhase.Moved)
                {
                    float movedMagnitude = (fingers[0].touch.position - fingers[0].startPosition).magnitude;
                    if (movedMagnitude < DragGap())
                    {
                        if (onTouchHold != null)
                            onTouchHold(fingers[0]);

                        Debug.Log(
                            string.Format(
                                "[onTouchHold] fingers[0].phaseLast={0}, fingers[0].phaseNow={1}, fingers[0].touch.position={2}",
                                fingers[0].phaseLast, fingers[0].phaseNow, fingers[0].touch.position
                            )
                        );
                    }
                }

                // touch up condiction
                if (fingers[0].phaseNow == TouchPhase.Ended && fingers[0].phaseLast != TouchPhase.Ended)
                {
                    if (onTouchUp != null)
                        onTouchUp(fingers[0]);

                    Debug.Log(
                        string.Format(
                            "[onTouchUp] fingers[0].phaseLast={0}, fingers[0].phaseNow={1}, fingers[0].touch.position={2}",
                            fingers[0].phaseLast, fingers[0].phaseNow, fingers[0].touch.position
                        )
                    );
                }

                // tap condiction
                if (fingers[0].phaseNow == TouchPhase.Ended && fingers[0].phaseLast != TouchPhase.Ended)
                {
                    float movedMagnitude = (fingers[0].touch.position - fingers[0].startPosition).magnitude;
                    //Debug.Log("[tap condiction] movedMagnitude=" + movedMagnitude.ToString());
                    //Debug.Log("[tap condiction] DragGap()=" + DragGap().ToString());

                    if (movedMagnitude < DragGap())
                    {
                        if (onTap != null)
                            onTap(fingers[0]);

                        Debug.Log(
                            string.Format(
                                "[onTouchTap] fingers[0].phaseLast={0}, fingers[0].phaseNow={1}, fingers[0].touch.position={2}",
                                fingers[0].phaseLast, fingers[0].phaseNow, fingers[0].touch.position
                            )
                        );
                    }
                }

                // drag condiction
                if (fingers[0].phaseNow == TouchPhase.Moved)
                {
                    float dragMagnitude = fingers[0].touch.deltaPosition.magnitude;  //(gestures[0].touch.position - gestures[0].startPosition).magnitude;
                    //Debug.Log("dragMagnitude=" + dragMagnitude.ToString());
                    //Debug.Log("DragGap()=" + DragGap().ToString());

                    if (dragMagnitude >= DragGap())
                    {
                        if (onDrag != null)
                            onDrag(fingers[0]);

                        Debug.Log(
                            string.Format(
                                "[onDrag] fingers[0].phaseLast={0}, fingers[0].phaseNow={1}, fingers[0].touch.deltaPosition={2}",
                                fingers[0].phaseLast, fingers[0].phaseNow, fingers[0].touch.deltaPosition
                            )
                        );
                    }
                }
            }

            // check two fingers gesture occur
            if (touchCount == 2)
            {
                twoFingers.valid = true;

                if (twoFingers.initialTwoFingersDistance <= 0f)
                {
                    twoFingers.initialTwoFingersDistance = (fingers[0].startPosition - fingers[1].startPosition).magnitude;
                }

                if (fingers[0].phaseNow == TouchPhase.Moved && fingers[1].phaseNow == TouchPhase.Moved)
                {
                    bool isTwoFingersKeepSameDistance = IsTwoFingersKeepSameDistance(
                        twoFingers.initialTwoFingersDistance, fingers[0].touch.position, fingers[1].touch.position
                    );

                    bool isTwoFingersDeltaPositionVeryClose = IsTwoFingersDeltaPositionVeryClose(
                        fingers[0].touch.deltaPosition, fingers[1].touch.deltaPosition
                    );

                    // drag condiction
                    if (isTwoFingersKeepSameDistance && isTwoFingersDeltaPositionVeryClose)
                    {
                        twoFingers.deltaPosition = (fingers[0].touch.deltaPosition + fingers[1].touch.deltaPosition) * 0.5f;

                        if (onDragTwoFingers != null)
                            onDragTwoFingers(fingers[0], fingers[1], twoFingers);

                        Debug.Log("[onDragTwoFingers]" + twoFingers.deltaPosition);
                    }

                    // pinch condiction
                    if (isTwoFingersKeepSameDistance == false && isTwoFingersDeltaPositionVeryClose == false)
                    {
                        float dist = (fingers[0].touch.position - fingers[1].touch.position).magnitude;
                        if (twoFingers.lastTwoFingersDistance == 0f)
                        {
                            twoFingers.lastTwoFingersDistance = dist;
                        }
                        twoFingers.deltaPinch = dist - twoFingers.lastTwoFingersDistance;

                        if (onPinchTwoFingers != null)
                            onPinchTwoFingers(fingers[0], fingers[1], twoFingers);

                        twoFingers.lastTwoFingersDistance = dist;

                        Debug.Log("[onPinchTwoFingers]" + twoFingers.deltaPinch);
                    }

                    // twist condiction
                    if (isTwoFingersKeepSameDistance == true && isTwoFingersDeltaPositionVeryClose == false)
                    {
                        ;
                    }
                }

                canInvokeOneFingerEventTimer.Reset();
            }
            else
            {
                twoFingers.valid = false;
                twoFingers.initialTwoFingersDistance = 0f;
                twoFingers.lastTwoFingersDistance = 0f;
                twoFingers.deltaPosition = Vector3.zero;
                twoFingers.deltaPinch = 0f;
                twoFingers.deltaTwist = 0f;
            }
        }
    }

    class Timer
    {
        public float lastTime = 0f;

        public Timer()
        {
            Reset();
        }

        public void Reset()
        {
            lastTime = Time.time;
        }

        public float GetTime()
        {
            float nowTime = Time.time;
            float result = nowTime - lastTime;

            return result;
        }
    }
}
