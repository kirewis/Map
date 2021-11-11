using UnityEngine;

namespace molinadejan
{
    public class PlayerMove : MonoBehaviour
    {
        public float walkSpeed = 6.0f;
        public float runSpeed = 10.0f;

        public float jumpHeight = 10.0f;
        public float gravity = 9.8f;
        public float jumpDamp = 1.0f;

        public float landHeight = 7.0f;

        public float stepOffset = 0.1f;

        private Animator anim;
        private CharacterController cc;

        private float v, h;
        private bool isRun, isJump;

        [SerializeField]
        private float height;

        private float maxYPos;

        private Vector3 velocity;
        private Vector3 moveDir;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            cc = GetComponent<CharacterController>();
        }
        
        private void Update()
        {
            Move();
            Run();

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }

        private void FixedUpdate()
        {
            if (isJump)
            {
                UpdateOnAir();
            }
            else
            {
                if(!anim.GetBool("isLand"))
                    UpdateOnGround();
            }
        }

        private void UpdateOnGround()
        {
            Vector3 groundMove = moveDir * Time.fixedDeltaTime;

            groundMove *= isRun ? runSpeed : walkSpeed;

            Vector3 stepDown = Vector3.down * stepOffset;
            
            cc.Move(groundMove + stepDown);

            if (!cc.isGrounded)
            {
                cc.Move(-stepDown);
                SetInAir(0);
            }
        }
        
        private void UpdateOnAir()
        {
            HeadCollisionCheck();

            velocity.y -= gravity * Time.fixedDeltaTime;
            cc.Move(velocity * Time.fixedDeltaTime);

            maxYPos = Mathf.Max(maxYPos, transform.position.y);

            if (cc.isGrounded)
            {
                height = maxYPos - transform.position.y;
                maxYPos = 0;

                if (height >= landHeight)
                    anim.SetBool("isLand", true);
                else
                    isJump = false;
            }
            else
            {
                isJump = true;
            }

            anim.SetBool("isJump", isJump);
        }
        
        private void Move()
        {
            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");

            moveDir = transform.forward * v + transform.right * h;
            moveDir.Normalize();

            anim.SetFloat("v", v);
            anim.SetFloat("h", h);

            anim.SetBool("isMove", v != 0 || h != 0);
            anim.SetFloat("speed", Mathf.Sqrt(v * v + h * h));
        }

        private void Run()
        {
            isRun = Input.GetKey(KeyCode.LeftShift);
            
            if (v <= 0)
                isRun = false;

            anim.SetBool("isRun", isRun);
        }
        
        private void Jump()
        {
            if (!isJump)
            {
                float yVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
                SetInAir(yVelocity);
            }
        }

        private void HeadCollisionCheck()
        {
            float height = cc.center.y * 2;

            if (velocity.y > 0 && Physics.Raycast(transform.position + Vector3.up * height, Vector3.up, 0.05f))
                velocity.y = 0;
        }

        private void SetInAir(float yVelocity)
        {
            isJump = true;
            velocity = moveDir * walkSpeed * jumpDamp;
            velocity.y = yVelocity;
            anim.SetBool("isJump", isJump);
        }
    }
}
