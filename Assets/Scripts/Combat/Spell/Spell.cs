using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public string spellName;
    public int spellPower;
    public int manaCost;
    public enum SpellType {Attack,Heal}
    public SpellType spellType;

    private Vector3 targetPosition;

    private float destroyTime = 0.25f;

    private void Update()
    {
        if (targetPosition != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position,targetPosition,31f);
            if(Vector3.Distance(transform.position,targetPosition)< 0.25f)
            {
                Destroy(this.gameObject,1f);
            }
        }
        else
        {
            Destroy(this.gameObject, destroyTime);
        }
    }
    public void CastSpell(Character target)
    {
        targetPosition=target.transform.position;
        if (spellType == SpellType.Attack)
        {
            //Hurt The Character
            target.Hurt(spellPower);
        }
        else if(spellType == SpellType.Heal)
        {
            //Heal The Character
            target.Heal(spellPower);
        }
    }
}
