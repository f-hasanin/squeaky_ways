using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmokingGame
{
    public class RangedWeapon : MonoBehaviourPun
    {
        [SerializeField] private GameObject nozzle = null;
        [SerializeField] private GameObject bullet = null;
        [SerializeField] private float bulletSpeed = 5f;
        private void Start()
        {
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                TakeInput();
            }
        }

        public void TakeInput()
        {
            if (!Input.GetMouseButton(0))
            {
                return;
            }
            Shoot();
        }

        public void Shoot()
        {
            photonView.RPC("RpcShoot", RpcTarget.All, gameObject.transform.forward * 10);
        }

        [PunRPC]
        public void RpcShoot(Vector3 vel)
        {
            GameObject bullet = Instantiate((GameObject)Resources.Load("Bullet"), nozzle.transform.position, nozzle.transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = vel;
            Destroy(bullet, 1);
            //var bulletInstance = Instantiate
            //   (
            //   bullet,
            //   nozzle.position,
            //   nozzle.rotation
            //   );
            //bulletInstance.GetComponent<Rigidbody>().velocity = bulletInstance.transform.forward * bulletSpeed;
        }
    }
}


