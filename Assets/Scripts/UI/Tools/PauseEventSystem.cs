/// <summary>
/// Pause event system.
/// It used for avoid click UI too frequency cuz many weird bug.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.EventSystems;

public class PauseEventSystem : MonoBehaviour
{
    public float pauseTime = 1f;
    private float timer = 0f;

    private EventSystem eventSystem = null;

    public void Pause()
    {
        eventSystem.enabled = false;
        timer = 0f;
        this.enabled = true;
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
            eventSystem.enabled = true;
            this.enabled = false;
        }
    }
}
