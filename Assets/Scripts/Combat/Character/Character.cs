using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private string characterName;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int attackPower;
    [SerializeField] private int defencePower;
    [SerializeField] private int manaPoint;
    [SerializeField] private List<Spell>spells;

    public void Hurt(int amount)
    {
        int damageAmount = Random.Range(0,1)*(amount-defencePower);
        health = Mathf.Max(health-damageAmount,0);
        if (health == 0)
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        int healthAmount = Random.Range(0, 1) * (int) (amount + maxHealth*0.5f);
        health = Mathf.Min(health + healthAmount, maxHealth);
    }
    private void Defend()
    {
        defencePower += (int)(defencePower * 0.25f);
    }
    public bool CastSpell(Spell spell,Character targetCharacter)
    {
        bool successCast=(manaPoint >= spell.manaCost);

        if (successCast)
        {
            Spell spellToCast = Instantiate<Spell>(spell,transform.position,Quaternion.identity);
            manaPoint-=spell.manaCost;
            spellToCast.CastSpell(targetCharacter);
        }
        return successCast;
    }
    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
