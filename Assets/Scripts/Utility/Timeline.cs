using UnityEngine;
using UnityEngine.Events;

namespace UnityToolbox
{
    public class Timeline : MonoBehaviour
    {
        public float totalTime = 10f;
        public float passedTime = 0f;

        [System.Serializable]
        public class Slot : System.Object
        {
            public float timeAt = 0f;
            public UnityEvent todo = null;
            public bool hasTriggered = false;
        }
        public Slot[] slots;

        public void Start()
        {
            if (passedTime >= totalTime || passedTime <= 0f)
            {
                Restart();
            }
        }

        public void Restart()
        {
            passedTime = 0f;
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].hasTriggered = false;
            }
            enabled = true;
        }

        public void Pause()
        {
            enabled = false;
        }

        public void Resume()
        {
            enabled = true;
        }

        void OnEnable()
        {
            if (passedTime >= totalTime)
            {
                Restart();
            }
        }

        void Update()
        {
            passedTime += Time.deltaTime;

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].hasTriggered)
                    continue;

                if (passedTime >= slots[i].timeAt)
                {
                    slots[i].todo.Invoke();
                    slots[i].hasTriggered = true;
                }
            }

            if (passedTime >= totalTime)
            {
                enabled = false;
                return;
            }
        }
    }
}
