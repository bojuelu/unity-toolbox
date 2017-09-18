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

    public void Do()
    {
        enabled = true;
    }

    void OnEnable()
    {
        t = 0f;
    }

    void Update()
    {
        if (t >= later)
        {
            justDoIt.Invoke();
            justDoIt.RemoveAllListeners();

            if (destroySelfWhenDone)
                GameObject.Destroy(this);
            else
                enabled = false;
        }
        else
        {
            t += Time.deltaTime;
        }
    }
}
