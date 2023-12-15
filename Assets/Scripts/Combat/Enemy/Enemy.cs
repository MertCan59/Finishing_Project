using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public void Act()
    {
        Spell spellCast = GetRandomSpell();
        int dieRoll = Random.Range(0, 2);
        Character target=BattleManager.Instance.GetRandomPlayer();
        switch (dieRoll)
        {
            case 0:
                Defend();
                break;
            case 1:
                if (spellCast.spellType ==Spell.SpellType.Heal)
                {
                    //get friendly weak unit
                    target=BattleManager.Instance.GetWeakestUnit();
                }
                if (!CastSpell(spellCast, target))
                {
                    //attack
                }
                break;
            case 2:
                //attack
                break;
        }
    }
    Spell GetRandomSpell()
    {
        return spells[Random.Range(0, spells.Count-1)];
    }

    public override void Die()
    {
        base.Die();
    }
}