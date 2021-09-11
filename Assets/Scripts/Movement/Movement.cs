using System;
using Photon.Pun;
using UnityEngine;

//namespace SmokingGame.Movement
//{
    [RequireComponent(typeof(Rigidbody2D))]

    public class Movement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 4f;
        private Rigidbody2D rb = null;
        private Vector3 PlayerPosChange;
        private Animator animator;
        //[SerializeField] public Camera cam;

        Camera cam;
        ////vars for thumbstick
        //private bool screenIsTouched = false;
        //private Vector2 a;
        //private Vector2 b;

        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            //if (photonView.IsMine)
            {
                //cam = GameObject.Find("Camera").GetComponent<Camera>();
                cam = gameObject.transform.Find("Camera").GetComponent<Camera>();
            }
        }

        void Update()
        {
            //if (photonView.IsMine)
            {
                PlayerPosChange = Vector3.zero; //reset change every frame
                PlayerPosChange.x = Input.GetAxisRaw("Horizontal"); //get axis raw doesn't interpolate b/w values - no acceleration or decceleration
                PlayerPosChange.y = Input.GetAxisRaw("Vertical");
                UpdateAnimationAndPos();


                //for thumbstick - calculate transform when thumbstick first touched
                //then calc offset to the point thumb moves to --> to find direction of mvoement
                //if (Input.GetMouseButtonDown(0))
                //{
                //    a = this.cam.ScreenToWorldPoint(new Vector3(
                //        Input.mousePosition.x,
                //        Input.mousePosition.y,
                //        cam.transform.position.z));
                //}
                //if (Input.GetMouseButton(0))
                //{
                //    screenIsTouched = true;
                //    b = this.cam.ScreenToWorldPoint(new Vector3(
                //        Input.mousePosition.x,
                //        Input.mousePosition.y,
                //        cam.transform.position.z));
                //}
                //else
                //{
                //    screenIsTouched = false;
                //}
            }
        }

        //private void FixedUpdate()
        //{
        //    if (screenIsTouched && photonView.IsMine)
        //    {
        //        Vector2 offset = b - a;
        //        Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
        //        MoveUsingThumbStick(direction * -1);
        //    }
        //}

        void UpdateAnimationAndPos()
        {
            if (PlayerPosChange != Vector3.zero)
            {
                MoveCharacter();
                animator.SetFloat("moveX", PlayerPosChange.x);
                animator.SetFloat("moveY", PlayerPosChange.y);
                animator.SetBool("moving", true);
            }
            else
            {
                animator.SetBool("moving", false);
            }
        }

        //can be called for thumbstick
        void MoveCharacter()
        {
            PlayerPosChange.Normalize();
            rb.MovePosition(transform.position + PlayerPosChange * movementSpeed * Time.deltaTime);
        }

        //void MoveUsingThumbStick(Vector2 dir)
        //{
        //    rb.MovePosition(dir * movementSpeed * Time.deltaTime);
        //}
    }
//}
