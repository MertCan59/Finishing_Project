using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set;}
    public Dictionary<int, List<Character>> characters=new Dictionary<int, List<Character>>();
    public int characterTurnIndex;
    public int activeTurn;
    public Spell playerSelectedSpell;
    public bool playerIsAttacking;

    [SerializeField] private BattleSpawnPoint[] spawnPoints;
    [SerializeField] private BattleUIManager uiManager;
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    private void Start()
    {
        characters.Add(0,new List<Character>());
        characters.Add(1,new List<Character>());
    }

    public Character GetRandomPlayer()
    {
        return characters[0][Random.Range(0, characters[0].Count-1)];
    }
    public Character GetWeakestUnit()
    {
        Character weakestMember = characters[1][0];
        foreach(Character character in characters[1])
        {
            if(character.health < weakestMember.health)
            {
                weakestMember = character;
            }
        }
        return weakestMember;
    }
    private void NextTurn()
    {
        activeTurn = activeTurn == 0 ? 1 : 0;
    }
    private void NextAct()
    {
        if (characters[0].Count > 0 && characters[1].Count>0)
        {
            if (characterTurnIndex < characters[activeTurn].Count-1)
            {
                characterTurnIndex++;
            }
            else
            {
                NextTurn();
                characterTurnIndex = 0;
            }
            switch(activeTurn)
            {
                case 0:
                    //UI staff
                    uiManager.ToggelActionState(true);
                    uiManager.BuildSpellList(GetCurrentCharacter().spells);
                    break;
                case 1:
                    // UI staff and act
                    StartCoroutine(PerformAct());
                    uiManager.ToggelActionState(false);
                    break; 
            }
        }
        else 
        {
            Debug.Log("Battle is finished");
        }
    }
    IEnumerator PerformAct()
    {
        yield return new WaitForSeconds(0.5f);
        if (GetCurrentCharacter().health>0 ) 
        {
           GetCurrentCharacter().GetComponent<Enemy>().Act();
        }
        uiManager.UpdateCharacterUI();
        yield return new WaitForSeconds(0.5f);
        NextAct();
    }

    public void SelectCharacter(Character character)
    {
        if (playerIsAttacking)
        {
            DoAttack(GetCurrentCharacter(),character);
        }else if (playerSelectedSpell != null)
        {
            if (GetCurrentCharacter().CastSpell(playerSelectedSpell,character))
            {
                uiManager.UpdateCharacterUI();
                NextAct();
            }
            else
            {
                Debug.LogWarning("Not Enough Mana to Cast Spell");
            }
        }
    }

    public void DoAttack(Character attacker, Character target)
    {
        target.Hurt(attacker.attackPower);
    }

    public void StartBattle(List<Character> players,List<Character> foes)
    {
        //adding to dict key of zero which it will be our player units and add new character to that list
        // and first spawn points belong to player units
         for(int i=0;i<=players.Count-1;i++)
        {
            characters[0].Add(spawnPoints[i + 3].Spawn(players[i]));
        }  
        
        for(int i=0;i<=foes.Count-1;i++)
        {
            //adding to dict key of zero which it will be our foes and add new character to that list
            // and first spawn points belong to foes
            characters[1].Add(spawnPoints[i + 3].Spawn(foes[i]));
        }
    }
    public Character GetCurrentCharacter()
    {
        return characters[activeTurn][characterTurnIndex];
    }
}
