/// <summary>
/// Pause unity event system.
/// It used for avoid click UI too frequency cuz many weird bug.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PauseEventSystem : MonoBehaviour
{
    private static List<PauseEventSystem> allPauseerInThisScene = new List<PauseEventSystem>();

    public float pauseTime = 1f;
    public float timer = 0f;

    private EventSystem eventSystem = null;

    private bool isPause = false;
    public bool IsPause { get { return isPause; } }

    public void Pause(float time)
    {
        pauseTime = time;
        timer = 0f;
        enabled = true;
        OnPause();
    }

    public void Pause()
    {   
        timer = 0f;
        enabled = true;
        OnPause();
    }

    public void PauseUntilResume()
    {
        enabled = false;
        OnPause();
    }

    public void Resume()
    {
        enabled = false;
        OnResume();
    }

    private void OnPause()
    {
        isPause = true;

        eventSystem.enabled = false;
    }

    private void OnResume()
    {
        isPause = false;

        for (int i = 0; i < allPauseerInThisScene.Count; i++)
        {
            PauseEventSystem pes = allPauseerInThisScene[i];
            if (pes == null)
                continue;

            if (pes.isPause)
            {
                return;
            }
        }

        eventSystem.enabled = true;
    }

    private void Awake()
    {
    }

    private void Start()
    {
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            Debug.LogError("No EventSystem in this scene");
            GameObject.Destroy(this);
        }

        this.enabled = false;

        if (allPauseerInThisScene.Contains(this) == false)
            allPauseerInThisScene.Add(this);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= pauseTime)
        {
            OnResume();

            this.enabled = false;
        }
    }
}
