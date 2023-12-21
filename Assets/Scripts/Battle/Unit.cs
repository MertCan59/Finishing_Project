using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit:MonoBehaviour
{
    public string unitName;
    public int damage;
    public int maxHp;
    public int currentHp;
    public int defencePower;

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
    public void PlayerDead()
    {
        SceneManager.LoadScene("BattleDeathScene");
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
