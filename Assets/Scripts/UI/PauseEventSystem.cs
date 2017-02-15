/// <summary>
/// Pause event system.
/// It used for avoid click UI too frequency cuz many weird bug.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class PauseEventSystem : MonoBehaviour
{
    public float pauseTime = 1f;
    private float timer = 0f;

    private EventSystem eventSystem = null;

    public void Pause()
    {
        if (eventSystem.enabled == false)
            return;
        else
        {
            eventSystem.enabled = false;
            timer = 0f;
            this.enabled = true;
        }
    }

    private void Awake()
    {
        eventSystem = this.gameObject.GetComponent<EventSystem>();
    }

    private void Start()
    {
        this.enabled = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= pauseTime)
        {
            eventSystem.enabled = true;
            this.enabled = false;
        }
    }
}
