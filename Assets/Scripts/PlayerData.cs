using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;

    [SerializeField] private int health = 1;
    [SerializeField] private GameObject playerArt;

    public void healthChange(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            playerArt.SetActive(false);
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            Invoke(nameof(deathReset), 3f);
        }
    }

    private void deathReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
