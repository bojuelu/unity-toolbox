using UnityEngine;

namespace UnityToolbox
{
    /// <summary>
    /// Depend on UIRoot. The elements of UIRoot.
    /// Author: BoJue.
    /// </summary>
    public class UIObject : MonoBehaviour
    {
        public string objectTag = "";
        public bool canFindViaUIRoot = true;
        private UIRoot root = null;
        public UIRoot Root { get { return root; } }
        private bool hasRegistered = false;
        public bool HasRegistered { get { return hasRegistered; } }

        public void SetParent(UIRoot root, bool resetPos = true, bool resetScale = true)
        {
            if (root == null)
            {
                Debug.LogError("when set parent to uiroot, the uiroot is null");
                return;
            }

            this.root = root;

            Vector3 origPos = this.transform.localPosition;
            Vector3 origScale = this.transform.localScale;

            this.transform.SetParent(root.transform);

            if (resetPos)
                this.transform.localPosition = origPos;
            if (resetScale)
                this.transform.localScale = origScale;
        }

        protected virtual void Start()
        {
            root = UIRoot.Instance;
            if (root == null)
            {
                Debug.LogError("UIRoot not exist, destory itself");
                GameObject.Destroy(this);
                return;
            }

            if (canFindViaUIRoot)
            {
                hasRegistered = root.Register(this);
                if (hasRegistered == false)
                    Debug.LogError(this.name + " Register to UIRoot failed");
            }
        }

        protected virtual void OnDestroy()
        {
            if (root != null && hasRegistered)
            {
                hasRegistered = root.Unregister(this);
            }
        }
    }
}
