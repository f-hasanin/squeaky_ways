using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpSpawner : MonoBehaviour
{
    //list of NRT pickups 
    [SerializeField] public List<GameObject> pickpsList = new List<GameObject>();
    //array of spawn points for the pickups
    [SerializeField] public GameObject[] spawnPoints = null;
    //text on canvas (to be chnged when item is picked up)
    [SerializeField] public GameObject dialogueBox = null;
    [SerializeField] public Text textName;
    [SerializeField] public Text textDescription;

    private void Start()
    {
        for(int i =0; i<pickpsList.Count; i++)
        {
            PhotonNetwork.Instantiate(pickpsList[i].name, spawnPoints[i].transform.position, Quaternion.identity);
        }
    }

    public void DisplayMessage(string name, string desc, string bonus, int points)
    {
        dialogueBox.SetActive(true);
        textName.text = name;
        textDescription.text = desc;
        StartCoroutine(DestroyMessage());
    }

    IEnumerator DestroyMessage()
    {
        print("deleting msg...");
        yield return new WaitForSeconds(3);
        dialogueBox.SetActive(false);
        print("deleted.");
    }

}

