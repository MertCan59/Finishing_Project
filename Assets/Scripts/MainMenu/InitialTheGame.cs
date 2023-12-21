using UnityEngine.SceneManagement;
using UnityEngine;


public class InitialTheGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Chapter1");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("Battle");
        }


    }
}
