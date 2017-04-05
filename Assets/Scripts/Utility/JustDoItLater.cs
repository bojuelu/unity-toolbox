/// <summary>
/// Just do it ...later! "http://picture-cdn.wheretoget.it/crgqai-l.jpg"
/// </summary>

using UnityEngine;
using UnityEngine.Events;

public class JustDoItLater : MonoBehaviour
{
    public UnityEvent justDoIt;
    public float later = 1f;
    private float t = 0f;

    void Update()
    {
        if (t >= later)
        {
            justDoIt.Invoke();
            justDoIt.RemoveAllListeners();
            GameObject.Destroy(this);
        }
        else
        {
            t += Time.deltaTime;
        }
    }
}
