/// <summary>
/// Use this class to do time counting.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Timer
{
    private float current;
    private float last;

    public Timer()
    {
        current = 0;
        last = 0;
    }

	/// <summary>
    /// When you get the pass thought time you wanted, call this function to re-count the time.
	/// </summary>
    public void Reset()
    {
        last = current = Time.time;
    }
	
    /// <summary>
    /// Get a time value, since you call the function:"Reset()" after the lastest time.
    /// </summary>
    /// <returns>Time in second</returns>
    public float GetTime()
    {
        current = Time.time;
		
        return (current - last);
    }

    /// <summary>
    /// Check if it is above the time
    /// </summary>
    /// <param name="time">Time in second</param>
    /// <param name="autoReset">Auto call Reset()</param>
    /// <returns>Is above or not</returns>
    public bool IsAbove(float seconds, bool autoReset)
    {
        if (GetTime() >= seconds)
        {
            if (autoReset)
                Reset();

            return true;
        }
        return false;
    }
}
