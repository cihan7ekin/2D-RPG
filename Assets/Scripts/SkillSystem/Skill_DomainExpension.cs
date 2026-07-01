using System.Collections.Generic;
using UnityEngine;

public class Skill_DomainExpension : Skill_Base
{
    [SerializeField] protected GameObject domianPrefab;

    [Header("Slowing Down Upgrade")]
    [SerializeField] private float slowDownPercents = 0.8f;
    [SerializeField] private float slowDownDomainDuration = 5f;

    [Header("Shard Spell Casting Upgrade")]
    [SerializeField] private int shardSpellToCast = 10;
    [SerializeField] private float shardCastingDuration = 1f;
    [SerializeField] private float shardCastingDomainDuration = 8f;
    private float spellCastTimer;
    private float spellsPerSeconds;

    [Header("Time Echo Spell Casting Upgrade")]
    [SerializeField] private int timeEchoSpellToCast = 8;
    [SerializeField] private float timeEchoCastingDuration = 2f;
    [SerializeField] private float timeEchoCastingDomainDuration = 5f;

    [Header("Domain details")]
    public float maxDomainSize = 10f;
    public float expandSpeed = 3f;

    private List<Enemy> trappedTargets = new List<Enemy>();
    private Transform currentTarget;

    public void CreateDomain()
    {
        spellsPerSeconds = GetSpellsToCast() / GetDomainDuration();

        GameObject domain = Instantiate(domianPrefab, transform.position, Quaternion.identity);
        domain.GetComponent<SkillObject_DomainExpansion>().SetupDomainExpansion(this);
    }

    public bool InstantDomain()
    {
        return upgradeType != SkillUpgradeType.Domain_EchoSpam && upgradeType != SkillUpgradeType.Domain_ShardSpam;
    }

    public void DoSpellCasting()
    {
        spellCastTimer -= Time.deltaTime;

        if (currentTarget == null)
            currentTarget = FindTargetInDomain();

        if (currentTarget != null && spellCastTimer < 0)
        {
            CastSpell(currentTarget);
            spellCastTimer = 1 / spellsPerSeconds;
            currentTarget = null;
        }
    }

    private void CastSpell(Transform target)
    {
        if (upgradeType == SkillUpgradeType.Domain_EchoSpam)
        {
            Vector3 offset = Random.value < 0.5f ? new Vector2(1, 0) : new Vector2(0, 1);
            skillManager.timeEcho.CreateTimeEcho(target.position + offset);
        }

        if (upgradeType == SkillUpgradeType.Domain_ShardSpam)
        {
            skillManager.shard.CreateRawShard(target, true);
        }
    }

    private Transform FindTargetInDomain()
    {
        trappedTargets.RemoveAll(target => target == null || target.health.isDead);

        if (trappedTargets.Count == 0)
            return null;

        int randomIndex = Random.Range(0, trappedTargets.Count);
        currentTarget = trappedTargets[randomIndex].transform;

        return currentTarget;
    }

    public float GetDomainDuration()
    {
        if (upgradeType == SkillUpgradeType.Domain_SlowingDown)
            return slowDownDomainDuration;
        else if (upgradeType == SkillUpgradeType.Domain_ShardSpam)
            return shardCastingDomainDuration;
        else if (upgradeType == SkillUpgradeType.Domain_EchoSpam)
            return timeEchoCastingDomainDuration;

        return 0;

    }

    public float GetSlowPercentage()
    {
        if (upgradeType == SkillUpgradeType.Domain_SlowingDown)
            return slowDownPercents;
        else if (upgradeType == SkillUpgradeType.Domain_ShardSpam)
            return shardCastingDuration;
        else if (upgradeType == SkillUpgradeType.Domain_EchoSpam)
            return timeEchoCastingDuration;

        return 0;
    }

    private int GetSpellsToCast()
    {
        if (upgradeType == SkillUpgradeType.Domain_ShardSpam)
            return shardSpellToCast;
        else if (upgradeType == SkillUpgradeType.Domain_EchoSpam)
            return timeEchoSpellToCast;

        return 0;
    }

    public void AddTarget(Enemy targetToAdd)
    {
        trappedTargets.Add(targetToAdd);
    }

    public void ClearTargets()
    {
        foreach (var enemy in trappedTargets)
            enemy.StopSlowDown();

        trappedTargets = new List<Enemy>();
    }
}
