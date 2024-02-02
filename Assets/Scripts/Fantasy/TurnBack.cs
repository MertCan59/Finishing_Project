using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBack : MonoBehaviour
{
    private bool isCloneCreated;
    private GameObject character;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private float destroyTime;
    GameObject go;
    private void Awake() 
    {
        character=gameObject;
    }   
    
    private void Update() 
    {
        CreateClone();
    }

    private void CreateClone()
    {
        
        if(Input.GetKeyDown(KeyCode.E))
        {     
            if(!isCloneCreated)
             {
                go=Instantiate(characterPrefab,character.transform.position,Quaternion.identity);
                isCloneCreated=true;
            }else
            {
                character.transform.position=go.transform.position;
                Destroy(go,destroyTime);
                isCloneCreated=false;
            } 
        }
    }
}
