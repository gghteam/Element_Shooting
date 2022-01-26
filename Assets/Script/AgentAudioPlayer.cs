using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAudioPlayer : AudioPlayer
{
    [SerializeField]
    private AudioClip _hitClip=null, _deathClip=null, _voiceLineclip=null;
    // �ǰ�, ����, �߽߰��� ����

    public void PlayHitSound()
    {
        PlayClipWithVariablePitch(_hitClip);
    }

    public void PlayDeathSound()
    {
        PlayClip(_deathClip);
    }

    public void PlayVoiceSound()
    {
        PlayClipWithVariablePitch(_voiceLineclip);
    }
}
