using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSound : AudioPlayer
{
    [SerializeField]
    protected AudioClip _stepClip;

    public void PlayStepSound()
    {
        PlayClipWithVariablePitch(_stepClip);
    }
}
