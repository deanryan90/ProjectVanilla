using UnityEngine;

public interface IKick : IDamage<float>
{
    void Kick(Animator kick, AudioSource kickAudio);
}