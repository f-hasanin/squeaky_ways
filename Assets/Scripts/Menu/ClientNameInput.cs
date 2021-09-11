using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmokingGame.Menus
{
    public class ClientNameInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInputField = null;

        [SerializeField] private Button startButton = null;

        private const string PlayerPrefsNameKey = "PlayerName";

        private void Start()
        {
            SetUpInputField();
        }
        
        //for remembering last inputted player name
        private void SetUpInputField()
        {
            //check for player name key
            if (!PlayerPrefs.HasKey(PlayerPrefsNameKey))
            {
                return;
            }
            //if key exists
            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

            nameInputField.text = defaultName;

            SetPlayerName(defaultName);
        }

        public void SetPlayerName(string name)
        {

            //ADD VALIDATION + MAYBE ON SUBMIT CALIDATION TOO
            //interactable prevents button from being pressable unless >= 1 character is enetered in the textfield
            //startButton.interactable = !string.IsNullOrEmpty(name);
            startButton.interactable = true;
        }

        public void SavePlayerName()
        {
            string playerName = nameInputField.text;

            PhotonNetwork.NickName = playerName;

            PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);

            print("saveplayer ran");
        }
    }
}

