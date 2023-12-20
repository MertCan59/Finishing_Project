using UnityEngine;

public class Unit:MonoBehaviour
{
    public string unitName;
    public int damage;
    public int maxHp;
    public int currentHp;
    public int defencePower;
    public int spellPower;

    public bool TakeDamage(int dmg)
    {
        currentHp -= dmg;

        if(currentHp <= 0)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }
    public void Heal(int amount)
    {
        currentHp += amount;
        if(currentHp>maxHp)
        {
            currentHp = maxHp;
        }
    }
}
