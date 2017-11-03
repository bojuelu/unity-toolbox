/// <summary>
/// Just do it ...later! "http://picture-cdn.wheretoget.it/crgqai-l.jpg"
/// </summary>

using UnityEngine;
using UnityEngine.Events;

public class JustDoIt : MonoBehaviour
{
    public UnityEvent justDoIt;
    public float later = 1f;

    public bool destroySelfWhenDone = true;

    private float t = 0f;

    public void Restart()
    {
        t = 0f;
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

    void Update()
    {
        if (t >= later)
        {
            justDoIt.Invoke();
            enabled = false;

            if (destroySelfWhenDone)
            {
                justDoIt.RemoveAllListeners();
                GameObject.Destroy(this);
            }
        }
        else
        {
            t += Time.deltaTime;
        }
    }
}
