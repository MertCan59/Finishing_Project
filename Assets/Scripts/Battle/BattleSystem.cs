using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText;

    public BattleHud playerHUD;
    public BattleHud enemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        state=BattleState.START;
        SetupBattle();
    }
    void SetupBattle()
    {
       GameObject playerGO= Instantiate(playerPrefab, playerBattleStation);
        playerUnit= playerGO.GetComponent<Unit>();
        
        GameObject enemyGo= Instantiate(enemyPrefab,enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();

        dialogueText.text = "A wild "+enemyUnit.unitName+" approaches...";
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
    }

}
