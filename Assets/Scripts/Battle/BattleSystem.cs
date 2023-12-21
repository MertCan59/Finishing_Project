using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{

    public BattleState state;
    public GameObject enemyPrefab;
    public Transform enemyBattleStation;
    public GameObject healSpell;
    public GameObject iceSpell;
    public GameObject fireSpell;


    Unit playerUnit;
    Unit enemyUnit;
    InitialTheGame theGame;

    [SerializeField] private GameObject playerGO;
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform playerTransform;
    [SerializeField] Animator playerAnimator;

    public TextMeshProUGUI dialogueText;
    public BattleHud playerHUD;
    public BattleHud enemyHUD;


    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        theGame = new InitialTheGame();
        state =BattleState.START;
        StartCoroutine(SetupBattle());        
    }
    private void Update()
    {
        OnAttack();
        OnDefend();
        OnSpell();
    }
    IEnumerator SetupBattle()
    {
        
        playerUnit= playerGO.GetComponent<Unit>();
        
        GameObject enemyGo= Instantiate(enemyPrefab,enemyBattleStation.transform.position,Quaternion.identity);
        enemyUnit = enemyGo.GetComponent<Unit>();

        dialogueText.text = "A wild "+enemyUnit.unitName+" approaches...";
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(0.5f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    IEnumerator PlayerAttack()
    {
        bool isAttacking = true;
        playerAnimator.SetBool("isAttacking",isAttacking);
        bool isDead =enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHp);
        yield return new WaitForSeconds(.3f);
        isAttacking = false;
        playerAnimator.SetBool("isAttacking",isAttacking);
        dialogueText.text = "The Attack is succesful";
        yield return new WaitForSeconds(1f);
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }


    IEnumerator PlayerSpell()
    {
        yield return new WaitForSeconds(0.1f);
        panel.SetActive(true);
        bool isActive = panel.activeSelf;
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.H) && isActive)
            {
                bool isAttacking = true;
                int healingPower=Random.Range(10, 25);
                GameObject castspell = Instantiate(healSpell, playerTransform.position, Quaternion.identity);
                playerAnimator.SetBool("isAttacking", isAttacking);
                playerUnit.Heal(healingPower);
                playerHUD.SetHP(playerUnit.currentHp);
                Destroy(castspell, .75f);
                dialogueText.text = "You are healed: "+healingPower;
                yield return new WaitForSeconds(0.5f);
                isAttacking = false;
                playerAnimator.SetBool("isAttacking", isAttacking);
                panel.SetActive(false);
                isActive = panel.activeSelf;
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
                break;
            }
            if (Input.GetKeyDown(KeyCode.I) && isActive)
            {
                bool isAttacking = true;
                int magicPower= Random.Range(23,42);
                GameObject castspell = Instantiate(iceSpell, enemyBattleStation.transform.position, Quaternion.identity);
                playerAnimator.SetBool("isAttacking", isAttacking);
                bool isDead = enemyUnit.TakeDamage(magicPower);
                enemyHUD.SetHP(enemyUnit.currentHp);
                dialogueText.text = "You used iceball "+magicPower;
                yield return new WaitForSeconds(.5f);
                Destroy(castspell);
                isAttacking = false;
                playerAnimator.SetBool("isAttacking", isAttacking);
                panel.SetActive(false);
                isActive = panel.activeSelf;
                if (isDead)
                {
                    state = BattleState.WON;
                    EndBattle();
                }
                else
                {
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                }
                break;
            }
            if (Input.GetKeyDown(KeyCode.F) && isActive)
            {
                bool isAttacking = true;
                int magicPower = Random.Range(18, 32);
                GameObject castspell = Instantiate(fireSpell, enemyBattleStation.transform.position, Quaternion.identity);
                playerAnimator.SetBool("isAttacking", isAttacking);
                bool isDead = enemyUnit.TakeDamage(magicPower);
                enemyHUD.SetHP(enemyUnit.currentHp);
                dialogueText.text = "You used fireball: "+magicPower;
                yield return new WaitForSeconds(.5f);
                Destroy(castspell);
                isAttacking = false;
                playerAnimator.SetBool("isAttacking", isAttacking);
                panel.SetActive(false);
                isActive = panel.activeSelf;
                if (isDead)
                {
                    state = BattleState.WON;
                    EndBattle();
                }
                else
                {
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                }
                break;
            }
            yield return null;
        }
    }
    IEnumerator PlayerDefence()
    {
        dialogueText.text = playerUnit.unitName + " is defending";
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage - playerUnit.defencePower);
        playerHUD.SetHP(playerUnit.currentHp);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks";
        yield return new WaitForSeconds(1f);
        bool isDead=  playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit.currentHp);
        if (playerUnit.currentHp <= 0)
        {
            playerUnit.PlayerDead();
        }
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state=BattleState.PLAYERTURN;
            PlayerTurn(); 
        }
    }
    void EndBattle()
    {
        switch(state)
        {
            case BattleState.WON:
                dialogueText.text = "You have won the battle! CONGRULATIONS ";
                break;
            case BattleState.LOST:
                dialogueText.text = "You were defeated ";
                break;
        }
    }
    void PlayerTurn()
    {
        dialogueText.text = "Choose an action";

    }

    void OnAttack()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(PlayerAttack());
        }
    }
    void OnDefend()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(PlayerDefence());
        }
    }
    void OnSpell()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(PlayerSpell());
        }
    }
}
