using UnityEngine;
using UnityEngine.UI;

namespace UnityToolbox
{
    public class SmoothProgressbar : MonoBehaviour
    {
        private Image progressbar;
        public float smoothLerpTime = 1f;
        private float smoothLerpTimer = 0f;

        public float progress = 0f;
        private float progressLast = 0f;
        private float progressSmooth = 0f;
        public float ProgressSmooth { get { return progressSmooth; } }


        void Start()
        {
            progressbar = gameObject.GetComponent<Image>();
            if (progressbar)
            {
                if (progressbar.type != Image.Type.Filled)
                {
                    Debug.LogWarning("This progressbar image type is not Image.Type.Filled. This component will be useless.");
                }
            }
        }

        void Update()
        {
            if (!progressbar)
            {
                Debug.LogWarning("Progressbar image is null");
                return;
            }

            if (progress != progressLast)
            {
                if (smoothLerpTimer < smoothLerpTime)
                {
                    if (smoothLerpTime > 0f)
                    {
                        smoothLerpTimer += Time.deltaTime;
                        progressSmooth = Mathf.Lerp(progressLast, progress, (smoothLerpTimer / smoothLerpTime));
                    }
                    else
                    {
                        progressSmooth = progressLast = progress;
                    }
                }
                else
                {
                    smoothLerpTimer = smoothLerpTime;
                    progressSmooth = progressLast = progress;
                    smoothLerpTimer = 0f;
                }
            }
            else
            {
                smoothLerpTimer = 0f;
                progressSmooth = progressLast = progress;
            }

            progressbar.fillAmount = progressSmooth;
        }
    }

}
