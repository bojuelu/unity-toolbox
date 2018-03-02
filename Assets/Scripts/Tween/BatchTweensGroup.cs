using UnityEngine;
using System.Collections;

namespace UnityToolbox
{
    /// <summary>
    /// Run a lot of BatchTweens at once. Powered by iTween.
    /// Author: BoJue.
    /// </summary>
    public class BatchTweensGroup : MonoBehaviour
    {
        public BatchTweens[] batchTweens;

        public bool autoStart = true;

        public void Run()
        {
            for (int i = 0; i < batchTweens.Length; i++)
            {
                if (batchTweens[i].enabled)
                {
                    batchTweens[i].Run();
                }
            }
        }

        public void Pause()
        {
            for (int i = 0; i < batchTweens.Length; i++)
            {
                if (batchTweens[i].enabled)
                {
                    batchTweens[i].Pause();
                }
            }
        }

        public void Resume()
        {
            for (int i = 0; i < batchTweens.Length; i++)
            {
                if (batchTweens[i].enabled)
                {
                    batchTweens[i].Resume();
                }
            }
        }

        public void Stop()
        {
            for (int i = 0; i < batchTweens.Length; i++)
            {
                if (batchTweens[i].enabled)
                {
                    batchTweens[i].Stop();
                }
            }
        }

        public bool IsRunning()
        {
            bool isRunning = false;
            for (int i = 0; i < batchTweens.Length; i++)
            {
                isRunning |= (batchTweens[i].IsRunning() && batchTweens[i].enabled);
            }
            return isRunning;
        }

        void Awake()
        {
            for (int i = 0; i < batchTweens.Length; i++)
            {
                batchTweens[i].autoStart = autoStart;
            }
        }
    }
}
