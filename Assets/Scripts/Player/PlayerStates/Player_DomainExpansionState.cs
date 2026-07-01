using UnityEngine;

public class Player_DomainExpansionState : PlayerState
{
    private Vector2 originalPosition;
    private float originalGravity;
    private float maxDistanceToGoUp;
    private bool isLevitating = false;
    private bool createDomain;

    public Player_DomainExpansionState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        originalPosition = player.transform.position;
        originalGravity = rb.gravityScale;
        maxDistanceToGoUp = GetAvailableRiseDistance();

        player.SetVelocity(0, player.riseSpeed);

        player.health.SetCanTakeDamage(false);
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(originalPosition, player.transform.position) >= maxDistanceToGoUp && isLevitating == false)
            Levitate();

        if (isLevitating)
        {
            skillManager.domainExpension.DoSpellCasting();
            if (stateTimer < 0)
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        rb.gravityScale = originalGravity;
        isLevitating = false;
        createDomain = false;

        player.health.SetCanTakeDamage(true);
    }

    private void Levitate()
    {
        isLevitating = true;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;

        stateTimer = skillManager.domainExpension.GetDomainDuration();

        if (createDomain == false)
        {
            createDomain = true;
            skillManager.domainExpension.CreateDomain();
        }
    }

    private float GetAvailableRiseDistance()
    {
        RaycastHit2D hit = Physics2D.Raycast(originalPosition, Vector2.up, player.riseMaxDistance, player.whatIsGround);

        float riseDistance = hit.collider != null ? hit.distance - 1 : player.riseMaxDistance;

        return riseDistance;
    }
}
