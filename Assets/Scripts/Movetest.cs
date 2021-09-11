using Photon.Pun;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]

public class Movetest : MonoBehaviourPun
{
    [SerializeField] private float movementSpeed = 20f;
    private CharacterController controller = null;

    Vector3 fwd, right;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        fwd = Camera.main.transform.forward;
        fwd.y = 0;
        fwd = Vector3.Normalize(fwd);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * fwd;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            TakeInput();
        }
    }

    private void TakeInput()
    {
        Vector3 sideMovement = right * movementSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 upMovement = fwd * movementSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");
        Vector3 heading = Vector3.Normalize(sideMovement + upMovement);

        Quaternion rotation = Quaternion.LookRotation(heading, Vector3.up);
        transform.rotation = rotation;

        //transform.forward = heading;
        controller.SimpleMove(sideMovement * movementSpeed);
        controller.SimpleMove(upMovement * movementSpeed);
    }
}

//using Photon.Pun;
//using UnityEngine;

//namespace SmokingGame.Movement
//{
//    [RequireComponent(typeof(CharacterController))]

//    public class Movement : MonoBehaviourPun
//    {
//        [SerializeField] private float movementSpeed = 5f;
//        Vector3 fwd, sideways;

//        private bool isTouched = false;
//        private Vector3 a, b;

//        private CharacterController controller = null;

//        void Start()
//        {
//            controller = GetComponent<CharacterController>();
//            fwd = Camera.main.transform.forward; //refers to upward and downwards vectors,in relation to the camera (since camera is isometric)
//            //ensuring y always = 0
//            fwd.y = 0;
//            //normalise keeps the direction (+/-), but changes length to 1, so vector can be used for motion
//            fwd = Vector3.Normalize(fwd);
//            //rotating 90 degrees from 
//            sideways = Quaternion.Euler(new Vector3(0, 90, 0)) * fwd;
//            //Camera.main.transform.right?
//        }

//        private void Update()
//        {
//            //if client is owner of player
//            if (photonView.IsMine)
//            {
//                //if (Input.anyKey)
//                //{
//                //    Move();
//                //}
//                if (Input.GetMouseButtonDown(0))
//                {
//                    print("mousebuttondown");
//                    a = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Camera.main.transform.position.z));

//                }
//                if (Input.GetMouseButton(0))
//                {
//                    print("mousebutton");
//                    isTouched = true;
//                    b = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
//                }
//                else
//                {
//                    print("mouse not touched");
//                    isTouched = false;
//                }
//            }
//        }

//        private void FixedUpdate()
//        {
//            if (isTouched)
//            {
//                print("istouchedistrue");
//                Vector3 offset = b - a;
//                Vector3 direction = Vector2.ClampMagnitude(offset, 1.0f);
//                //Move();
//                MoveByTouch(direction);
//            }
//        }

//        private void Move()
//        {
//            //Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey"));
//            Vector3 sideMovement = sideways * movementSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
//            Vector3 upMovement = fwd * movementSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

//            //direction character points to
//            Vector3 heading = Vector3.Normalize(sideMovement + upMovement);
//            //heading becomes forward vector (which is usually z if not isometric) - this rotates player
//            transform.forward = heading;
//            //movement
//            transform.position += sideMovement;
//            transform.position += upMovement;
//        }

//        private void MoveByTouch(Vector3 direction)
//        {
//            print("movebytouch");
//                var movement = new Vector3
//                {
//                    x = Input.GetAxisRaw("Horizontal"),
//                    y = 0f,
//                    z = Input.GetAxisRaw("Vertical"),
//                }.normalized;

//                controller.SimpleMove(movement * movementSpeed);
//        }
//    }
//}
