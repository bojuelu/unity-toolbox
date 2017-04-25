/// <summary>
/// Pause event system.
/// It used for avoid click UI too frequency cuz many weird bug.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PauseEventSystem : MonoBehaviour
{
    public float pauseTime = 1f;
    private float timer = 0f;

    private EventSystem eventSystem = null;

    private static int callPauseCount = 0;

    public void Pause(float time)
    {
        pauseTime = time;
        Pause();
    }

    public void Pause()
    {
        eventSystem.enabled = false;
        timer = 0f;
        this.enabled = true;
        callPauseCount++;
    }

    private void Awake()
    {
        eventSystem = this.gameObject.GetComponent<EventSystem>();
        if (eventSystem == null)
        {
            eventSystem = GameObject.FindObjectOfType<EventSystem>();
        }
    }

    private void Start()
    {
        this.enabled = false;
    }

    private void OnDisable()
    {
        eventSystem.enabled = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= pauseTime)
        {
            callPauseCount--;
            this.enabled = false;

            if (callPauseCount <= 0)
            {
                callPauseCount = 0;
                eventSystem.enabled = true;
            }

        }
    }
}
