using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider slider;
    [SerializeField]
    public float minHealth = 0;
    [SerializeField]
    public float currentHealth;
    public GameObject deathPanel;
    public GameObject player;
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
            slider.value = currentHealth;
        }
    }

    private void Start()
    {
        deathPanel.SetActive(false);
        player.GetComponent<Movement>().enabled = true;

        CurrentHealth = 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Smoke")
        {
            print("collided with smoke");
            LoseHealth(34);
        }
    }

    void LoseHealth(float damage)
    {
        CurrentHealth -= damage;

        if(CurrentHealth<= 0)
        {
            player.GetComponent<Movement>().enabled = false;

            deathPanel.SetActive(true);
            //die();
        }
    }
}
