using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmokingGame.Menus
{
    public class MainMenu : MonoBehaviourPunCallbacks
    {

        [SerializeField] private GameObject findOpponentPanel = null;
        [SerializeField] private GameObject waitingStatusPanel = null;
        [SerializeField] private TextMeshProUGUI waitingStatusText = null;

        private bool isConnecting = false;
        //will need to change game version if game is released - ppl on different game versions cannot play together
        private const string GameVersion = "0.1";
        private const int MaxPlayersPerRoom = 2;

        private void Awake()
        {
            //sync the scene between all players
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public void loadOne()
        {
            StartCoroutine(sceneLoading());
        }

        private IEnumerator sceneLoading()
        {
            yield return new WaitForSeconds(3f);

            // note: this requires "using UnityEngine.SceneManagement;"
            SceneManager.LoadScene("Scene_One");
        }

        public void ReloadLvl(string name)
        {

            SceneManager.LoadScene(name);
        }

        public void FindOpponent()
        {
            isConnecting = true;
            findOpponentPanel.SetActive(false);
            waitingStatusPanel.SetActive(true);

            waitingStatusText.text = "Searching...";

            if (PhotonNetwork.IsConnected) //if already connected (from before)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else //otherwise connect using settings
            {
                PhotonNetwork.GameVersion = GameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Master");

            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
            }

        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            waitingStatusPanel.SetActive(false);
            findOpponentPanel.SetActive(true);
            Debug.Log($"Disconnected due to: {cause}");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("No clients waiting for an opponent creating a new room");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Client successfully joined room");
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

            if (playerCount != MaxPlayersPerRoom)
            {
                waitingStatusText.text = "Waiting For Opponent";
                Debug.Log("Client is waiting for an opponent");
            }
            else
            {
                waitingStatusText.text = "Opponent found";
                Debug.Log("Matching is ready to begin");
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;

                waitingStatusText.text = "Opponent found";
                Debug.Log("Match is ready to begin");

                PhotonNetwork.LoadLevel("Scene_One");
            }
        }
    }
}

