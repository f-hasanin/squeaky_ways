using Photon.Pun;
using UnityEngine;

//CODE LOGIC OBTAINED FROM PHOTON DOCUMENTATION https://doc.photonengine.com/en-us/pun/current/demos-and-tutorials/pun-basics-tutorial/player-instantiation
namespace SmokingGame
{
    public class PlayerSpawner : MonoBehaviourPun
    {
        //photon spawns stuff from the resources folder in clients, so technically String could be used rather than gameobject
        //but this ensures that there won't be problems if prefab name is changed at any point
        [SerializeField] private GameObject playerPrefab = null;
        public static GameObject LocalPlayerInstance;
        public GameObject spawnPoint;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                PlayerSpawner.LocalPlayerInstance = this.gameObject;
            }
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            GameObject cam = playerPrefab.transform.Find("Camera").gameObject;

            //ensure camera is on
            cam.GetComponent<CameraMovement>().enabled = true;
            cam.SetActive(true);

            if (PlayerSpawner.LocalPlayerInstance == null)
            {
                //since player is instantiated usng photon, input is taken only from player prefab that is instantiated by said client
                PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.transform.position, Quaternion.identity);
                //*quaternion identity is basically quaternion.zero
            }
            else
            {
                Debug.Log("player already instantiated");
            }

            ////if the player is not the client, disable the camera
            //if (!playerPrefab.GetPhotonView().IsMine)
            //{
            //    cam.GetComponent<CameraMovement>().enabled = false;
            //    cam.SetActive(false);
            //}
        }
    }
}

