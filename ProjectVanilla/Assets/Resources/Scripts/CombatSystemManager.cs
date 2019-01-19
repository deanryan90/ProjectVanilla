using System.Collections;
using UnityEngine;

public class CombatSystemManager : CombatSystem
{
    public void CreateCombatAction(CombatSystem.CombatActions action)
    {
        Animator animator = transform.gameObject.AddComponent<Animator>();
        AudioSource audioSource = transform.gameObject.AddComponent<AudioSource>();

        switch (action)
        {
            case CombatActions.Kick:
                //Kick(animator, audioSource);
                break;

            case CombatActions.Punch:
                Punch(animator, audioSource);
                break;
        }
    }
}