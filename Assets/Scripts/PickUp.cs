using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public AudioClip grabSound;
    AudioSource audio;
    [SerializeField] private String _name="";
    [SerializeField] private String _description="";
    [SerializeField] private String _bonus = "";
    [SerializeField] int _points = 30;

    public string namee { get { return _name; } }
    public string description { get { return _description; } }
    public string bonus { get { return _bonus; } }
    public int points { get { return _points; } }


    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //play sound
            audio.PlayOneShot(grabSound);

            ////make dialogue box active
            GameObject.Find("PickUpManager").GetComponent<PickUpSpawner>().DisplayMessage(namee, description, bonus, points);
            GameObject.Find("ProgressManager").GetComponent<ProgressManager>().IncreaseProgress(points);

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        try
        {
            PhotonNetwork.Destroy(this.gameObject);

        }
        catch
        {
            Debug.Log("error deleting pickup");
        }
    }

}
