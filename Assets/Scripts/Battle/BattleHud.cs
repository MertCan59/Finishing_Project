using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = "Name: "+unit.unitName;
        hpSlider.maxValue= unit.maxHp;
        hpSlider.value = unit.currentHp;
    }
    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
