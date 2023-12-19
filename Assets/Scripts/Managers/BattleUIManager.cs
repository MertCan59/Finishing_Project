using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject spellPanel;
    [SerializeField] private Button[] actionButtons;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI[] characterInfo;

    private void Start()
    {
        spellPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin,ray.direction);
            if(hitInfo.collider != null && hitInfo.collider.CompareTag("Player"))
            {
                BattleManager.Instance.SelectCharacter(hitInfo.collider.GetComponent<Character>());
            }
        }
    }
    public void ToggelSpellPanel(bool state)
    {
        if (state)
        {
            BuildSpellList(BattleManager.Instance.GetCurrentCharacter().spells);
        }
    }

    public void ToggelActionState(bool state)
    {
        ToggelSpellPanel(state);
        foreach(Button button in actionButtons)
        {
            button.interactable = state;
        }
    }

    public void BuildSpellList(List<Spell>spells)
    {
        if (spellPanel.transform.childCount > 0)
        {
            foreach(Button button in spellPanel.transform.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
        }
        foreach(Spell spell in spells)
        {
            Button spellButton=Instantiate<Button>(button,spellPanel.transform);
            spellButton.GetComponentInChildren<TextMeshProUGUI>().text=spell.spellName;
            spellButton.onClick.AddListener(() => SelectSpell(spell));
        }
    }

    void SelectSpell(Spell spell)
    {
        BattleManager.Instance.playerSelectedSpell = spell;
        BattleManager.Instance.playerIsAttacking = false;
    }
    void SelectAttack()
    {
        BattleManager.Instance.playerSelectedSpell = null;
        BattleManager.Instance.playerIsAttacking = true;
    }
    public void UpdateCharacterUI()
    {
        for(int i = 0; i < BattleManager.Instance.characters[0].Count;i++)
        {
            Character character = BattleManager.Instance.characters[0][i];
            characterInfo[i].text = string.Format("{0} HP: {1} /{2}, MP: {3} ",character.characterName,character.health,character.maxHealth, character.manaPoint);
        }
    }
    public void Defend()
    {
        BattleManager.Instance.GetCurrentCharacter().Defend();
    }
}
