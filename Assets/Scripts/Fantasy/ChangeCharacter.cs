using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    private int characterIndex=0;
    // Start is called before the first frame update
    void Start()
    {
        characters[characterIndex].SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        ChangeCharacterIndex();
    }
    private void ChangeCharacterIndex()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            characters[characterIndex].SetActive(false);
            characterIndex++;   

            if(characterIndex>=characters.Length)
            {
                characterIndex=0;
            }

            characters[characterIndex].SetActive(true);
        }
    }
}
