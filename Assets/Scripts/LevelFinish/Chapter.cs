using System.Collections;
using UnityEngine;
public class Chapter : MonoBehaviour
{
    private bool chapter;
    private void Update()
    {
        StartCoroutine(ChangeChapter()); ;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            chapter = true;
        }
    }
    private IEnumerator ChangeChapter()
    {
        if (chapter)
        {
            GameManager.Instance.LoadLevel("Chapter2");
            yield return new WaitForSeconds(1.25f);
        }
        yield return null;
    }
}
