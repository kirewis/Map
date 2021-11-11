using K_Space;
using UnityEngine;

namespace molinadejan
{
    public class PlayerRotate : MonoBehaviour
    {
        public float rotDamp = 5.0f;

        private Animator anim;
        private Player_Camera pCam;

        private Vector3 targetVec;

        [SerializeField]
        private bool isTurn = false;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            pCam = GetComponent<Player_Camera>();
        }

        private void Update()
        {
            Vector3 t = pCam.aim.transform.position - pCam.player_camera.transform.position;
            targetVec = new Vector3(t.x, 0, t.z);
        }

        private void FixedUpdate()
        {
            if (anim.GetBool("isMove"))
            {
                isTurn = false;

                if (!Input.GetKey(KeyCode.C) && !pCam.is_Lerp)
                {
                    Rotate();
                }
            }
            else
            {
                if(!Input.GetKey(KeyCode.C) && !pCam.is_Lerp)
                {
                    if ((pCam.xRotation > 90 || pCam.xRotation < -90) || isTurn)
                    {
                        isTurn = true;

                        Rotate();

                        if (Mathf.Abs(pCam.xRotation) < 1.0f)
                            isTurn = false;
                    }
                }
            }
            
            anim.SetBool("isTurn", isTurn);
        }

        private void Rotate()
        {
            Vector3 oldForward = transform.forward;

            var rotation = Quaternion.LookRotation(targetVec);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
                Time.fixedDeltaTime * rotDamp);

            Vector3 newForward = transform.forward;

            float angle = Vector3.Angle(oldForward, newForward);

            if (pCam.xRotation < 0)
                pCam.xRotation += angle;
            else
                pCam.xRotation -= angle;
        }
    }
}
