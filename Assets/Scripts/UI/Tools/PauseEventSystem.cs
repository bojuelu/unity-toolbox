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

    private static List<PauseEventSystem> pauseCaller;

    public void Pause(float time)
    {
        pauseTime = time;
        Pause();
    }

    public void Pause()
    {
        if (pauseCaller == null)
            pauseCaller = new List<PauseEventSystem>();
        
        eventSystem.enabled = false;
        timer = 0f;
        this.enabled = true;

        if (pauseCaller.Contains(this) == false)
            pauseCaller.Add(this);
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
            if (pauseCaller == null)
                pauseCaller = new List<PauseEventSystem>();

            if (pauseCaller.Contains(this))
                pauseCaller.Remove(this);

            if (pauseCaller.Count <= 0)
            {
                eventSystem.enabled = true;
            }

            this.enabled = false;
        }
    }
}
