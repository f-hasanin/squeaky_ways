using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    [SerializeField]
    public float countdown=60f;
    public Text txtCountdown;
    public GameObject winPanel;
    public GameObject lostPanel;
    public GameObject player;
    public Slider slider;
    public float maxPoints = 100;
    public float currentProgress;
    public float CurrentProgress
    {
        get
        {
            return currentProgress;
        }
        set
        {
            currentProgress = value;
            slider.value = currentProgress;
            
        }
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        txtCountdown.text = Mathf.Round(countdown).ToString();
        if(countdown < 0)
        {
            OnGameLost();
        }
    }

    private void Start()
    {
        winPanel.SetActive(false);
        CurrentProgress = 0f;
    }


    public void IncreaseProgress(int points)
    {
        CurrentProgress += points;
        if(currentProgress >= maxPoints)
        {

            StartCoroutine(OnCOmpletion());
        }
    }

    IEnumerator OnCOmpletion()
    {
        yield return new WaitForSeconds(1);
        winPanel.SetActive(true);
        player.GetComponent<Movement>().enabled = false;
    }

    public void OnGameLost()
    {
        player.GetComponent<Movement>().enabled = false;

        lostPanel.SetActive(true);
    }
}
