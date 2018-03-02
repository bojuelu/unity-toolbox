using UnityEngine.Events;

namespace UnityToolbox
{
    /// <summary>
    /// A panel has BringIn(open) and Dismiss(close) behavior.
    /// Author: BoJue.
    /// </summary>
    public class UIPanel : UIObject
    {
        public bool defaultIsShow = true;

        protected bool isShow = true;
        public bool IsShow { get { return isShow; } }

        public UnityEvent onBringInEvent;
        public UnityEvent onDismissEvent;

        public virtual void BringIn()
        {
            isShow = true;

            if (onBringInEvent != null)
                onBringInEvent.Invoke();
        }

        public virtual void Dismiss()
        {
            isShow = false;

            if (onDismissEvent != null)
                onDismissEvent.Invoke();
        }

        protected override void Start()
        {
            base.Start();

            isShow = defaultIsShow;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (onBringInEvent != null)
                onBringInEvent.RemoveAllListeners();

            if (onDismissEvent != null)
                onDismissEvent.RemoveAllListeners();
        }
    }
}
