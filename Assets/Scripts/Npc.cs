using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text txtDialogue;
    public string[] sentences;
    public bool PlayerIsInRange;
    GameObject player;

    public void Start()
    {

        dialogueBox.SetActive(false);
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PlayerIsInRange)
        {
            if (dialogueBox.activeInHierarchy)
            {
                //dialogueBox.SetActive(false);
                StartCoroutine(changeMessage());
            }
            else
            {
                dialogueBox.SetActive(true);
                changeMessage();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //disable player mvoement
            player.GetComponent<Movement>().enabled = false;
            player.GetComponent<Animator>().enabled = false;
            PlayerIsInRange = true;
            dialogueBox.SetActive(true);

            StartCoroutine(changeMessage());
        }
    }

    IEnumerator changeMessage()
    {
        txtDialogue.text = sentences[0];

        //disable rb
        for (int i = 0; i < sentences.Length; i++)
        {
            yield return new WaitForSeconds(1.5f);
            print(sentences[i]);
            txtDialogue.text = sentences[i];
        }

        yield return new WaitForSeconds(2f);
        dialogueBox.SetActive(false);

        //re-enable movement for player
        player.GetComponent<Movement>().enabled = true;
        player.GetComponent<Animator>().enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerIsInRange = false;
            //dialogueBox.SetActive(false);
        }
    }

}

