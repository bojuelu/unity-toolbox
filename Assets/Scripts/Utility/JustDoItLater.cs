/// <summary>
/// Just do it ...later! "http://picture-cdn.wheretoget.it/crgqai-l.jpg"
/// Sometimes, we want to do something, but it should wait for a while, so... just do it later!
/// It's another option between MonoBehavior.StartCorutine().
/// Author: BoJue.
/// 
/// Example:
/// public class MyPokemonAction : MonoBehaviour
/// {
///     public Animation snorlax;
///     public AnimationClip yawn;
///     public AnimationClip lieDown;
/// 
///     // The Snorlax is going to sleep, 
///     // she make a yawn, then lie down for sleep.
///     public void GoToSleep()
///     {
///         // play yawn directly.
///         PlayClip(snorlax, yawn);
/// 
///         // play lie down after 5 seconds.
///         JustDoItLater justDoItLater = snorlax.gameObject.AddComponent<JustDoItLater>();
///         object[] things = new object[2];
///         things[0] = snorlax as object;
///         things[1] = lieDown as object;
///         justDoItLater.justDo = OnPlayClip;  // just do: use animation to play the clip
///         justDoItLater.it = things;  // it: the animation and clip
///         justDoItLater.later = 5f;  // later: in this case, yawn action spend 5 seconds.
///     }
/// 
///     private void PlayClip(Animation character, AnimationClip clip)
///     {
///         character.clip = clip;
///         character.Play();
///     }
/// 
///     private object OnPlayClip(object[] objs)
///     {
///         Animation snorlax = objs[0] as Animation;
///         AnimationClip lieDown = objs[1] as AnimationClip;
///         PlayClip(snorlax, lieDown);
///         return null;
///     }
/// }
/// </summary>

using UnityEngine;
using System.Collections;

public class JustDoItLater : MonoBehaviour
{
    public delegate object Method(object[] objs);
    public Method justDo = null;
    public object[] it = null;
    public float later = 0f;

    private float t = 0f;

    void Update()
    {
        t += Time.deltaTime;
        if (t < later)
        {
            return;
        }
        else
        {
            if (justDo != null)
                justDo.Invoke(it);
            GameObject.Destroy(this);
        }
    }
}
