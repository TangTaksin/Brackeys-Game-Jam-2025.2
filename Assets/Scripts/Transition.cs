using System;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    Animator _animator;

    public static Action CalledFadeIn,CallFadeOut;

    public static Action FadeInOver, FadeOutOver;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CalledFadeIn += PlayFadeIn;
    }

    private void OnDisable()
    {
        CalledFadeIn -= PlayFadeIn;
    }


    public void PlayFadeIn()
    {
        _animator.Play("generic_transition_in");
    }

    public void PlayFadeOut()
    {
        _animator.Play("generic_transition_out");
    }
    

    public void FinishFadeIn()
    {
        FadeInOver?.Invoke();
    }

    public void FinishFadeOut()
    {
        FadeOutOver?.Invoke();
    }
}
