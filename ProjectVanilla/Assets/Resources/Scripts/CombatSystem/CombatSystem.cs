using UnityEngine;

public class CombatSystem : MonoBehaviour, ICombat
{
    public enum CombatActions
    {
        Punch,
        Kick
    }

    private Animator animator;
    private AudioSource audioSource;
    public ICombat combat;

    private GameObject GO { get; set; }

    public void Punch(Animator punch, AudioSource punchAudio)
    {
        CreateAnimator(CombatActions.Punch);
    }

    private void Awake()
    {
        GO = gameObject;
    }

    public Animator CreateAnimator(CombatActions action)
    {
        var animator = transform.gameObject.AddComponent<Animator>();

        animator.name = action.ToString();
        return animator;
    }
}