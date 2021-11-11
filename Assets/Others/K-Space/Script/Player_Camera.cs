using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K_Space
{
    public class Player_Camera : MonoBehaviour
    {
        public GameObject target;
        public GameObject aim;

        public GameObject player_camera;

        [Range(-85, 85)]
        public float yRotation;
        [Range(-360,  360)]
        public float xRotation;
        [Range(0, 1)]
        public float standard_Ypos;
        [Range(0, 10)]
        public float camera_Distance_From_Character;
        [Range(0, 1)]
        public float camera_Right_Distance;
        [Range(0, 1)]
        public float camera_collide_normal;
        [Range(0, 3)]
        public float first_View_Distance;
        [Range(0, 1000)]
        public float rotSpeed;

        public bool is_collide = false;

        public float alt_Xrotation = 0;
        public float alt_Yrotation = 0;
        //lerp
        public float lerp_Xrotation = 0;
        public float lerp_Yrotation = 0;
        public float lerp_dx;
        public float lerp_dy;
        public float lerp_x;
        public float lerp_y;
        public bool is_Lerp = false;
        //
        const float aim_Distance = 15.0f;

        public bool third_View = true;
        public Ray camera_Ray;
        public RaycastHit camera_Ray_Hit;
        
        private void LateUpdate()
        {
            Change_Perspective();
            if (third_View)
            {
                if (KeyInputManager.Instance.key_Camera_Rotate)
                {
                    Third_Alt_Rotation();
                }
                else
                {
                    if(KeyInputManager.Instance.key_UP_Camera_Rotate)
                    {
                        Set_Lerp();
                    }
                    Alt_Lerp();
                    Rotation();
                    ThirdView();
                }
            }
            else
            {
                if (KeyInputManager.Instance.key_Camera_Rotate)
                {
                    First_Alt_Rotation();
                }
                else
                {
                    if (KeyInputManager.Instance.key_UP_Camera_Rotate)
                    {
                        Set_Lerp();
                    }
                    Alt_Lerp();
                    Rotation();
                    FirstView();
                }
            }
        }

        private void Change_Perspective()
        {
            if (KeyInputManager.Instance.key_cameraSwitch)
            {
                third_View = !third_View;
            }
        }

        private void FirstView()
        {
            aim.transform.rotation = target.transform.rotation;
            aim.transform.rotation *= Quaternion.Euler(new Vector3(yRotation, xRotation, 0.0f));
            aim.transform.position = target.transform.position + aim.transform.forward * aim_Distance;

            player_camera.transform.position = target.transform.position;
            player_camera.transform.position += transform.forward * first_View_Distance;
            player_camera.transform.rotation = transform.rotation;
            player_camera.transform.rotation *= Quaternion.Euler(new Vector3(yRotation, xRotation, 0.0f));
        }

        private void Rotation()
        {
            yRotation += -KeyInputManager.Instance.key_mouseY * rotSpeed * Time.deltaTime;
            yRotation = Mathf.Clamp(yRotation, -85, 85);
            alt_Yrotation = yRotation;
            
            xRotation += KeyInputManager.Instance.key_mouseX * rotSpeed * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -350, 350);

            alt_Xrotation = xRotation;
        }

        void ThirdView()
        {
            SetCamera(camera_Distance_From_Character);
            RayCast();
            SetAim();
        }

        private void SetAim()
        {
            aim.transform.position = player_camera.transform.position + player_camera.transform.forward * aim_Distance;
        }

        private void RayCast()
        {
            camera_Ray = new Ray(target.transform.position,
                (player_camera.transform.position - target.transform.position).normalized);

            int layerMask = (-1) - (1 << LayerMask.NameToLayer("Player"));

            if (Physics.SphereCast(camera_Ray.origin, 0.1f, camera_Ray.direction,
                out camera_Ray_Hit, camera_Distance_From_Character, layerMask))
            {
                SetCamera(camera_Ray_Hit.distance);
                player_camera.transform.position += camera_Ray_Hit.normal * camera_collide_normal;
                is_collide = true;
            }
            else
            {
                is_collide = false;
            }
        }

        private void SetCamera(float distance)
        {
            player_camera.transform.rotation = target.transform.rotation;
            player_camera.transform.localRotation = Quaternion.Euler(new Vector3(yRotation, xRotation, 0.0f));
            player_camera.transform.position = target.transform.position + (player_camera.transform.right * camera_Right_Distance -
                player_camera.transform.forward * camera_Distance_From_Character).normalized * distance;
        }


        //void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawRay(camera_Ray.origin, camera_Ray.direction * camera_Ray_Hit.distance);
        //    int layerMask = (-1) - (1 << LayerMask.NameToLayer("Player"));
        //    if (Physics.SphereCast(camera_Ray.origin, 0.1f, camera_Ray.direction,
        //        out camera_Ray_Hit, third_View_Distance, layerMask))
        //    {
        //        Gizmos.DrawRay(camera_Ray.origin, camera_Ray.direction * camera_Ray_Hit.distance);
        //        Gizmos.DrawSphere(camera_Ray.origin + camera_Ray.direction * camera_Ray_Hit.distance, 0.1f);
        //    }
        //    else
        //    {
        //        Gizmos.DrawRay(camera_Ray.origin, camera_Ray.direction * third_View_Distance);
        //        Gizmos.DrawSphere(camera_Ray.origin + camera_Ray.direction * third_View_Distance, 0.1f);
        //    }

        //}

        public void Third_Alt_Rotation()
        {
            //È¸Àü°ª
            alt_Yrotation += -KeyInputManager.Instance.key_mouseY * rotSpeed * Time.deltaTime;
            alt_Yrotation = Mathf.Clamp(alt_Yrotation, -85, 85);
            
            alt_Xrotation += KeyInputManager.Instance.key_mouseX * rotSpeed * Time.deltaTime;
            
            //ray
            camera_Ray = new Ray(target.transform.position,
               (player_camera.transform.position - target.transform.position).normalized);

            int layerMask = (-1) - (1 << LayerMask.NameToLayer("Player"));

            player_camera.transform.rotation = target.transform.rotation;
            player_camera.transform.localRotation = Quaternion.Euler(new Vector3(alt_Yrotation, alt_Xrotation, 0.0f));
            player_camera.transform.position = target.transform.position + (player_camera.transform.right * camera_Right_Distance -
                player_camera.transform.forward * camera_Distance_From_Character).normalized * camera_Distance_From_Character;
            if (Physics.SphereCast(camera_Ray.origin, 0.1f, camera_Ray.direction,
                out camera_Ray_Hit, camera_Distance_From_Character, layerMask))
            {
                player_camera.transform.rotation = target.transform.rotation;
                player_camera.transform.localRotation = Quaternion.Euler(new Vector3(alt_Yrotation, alt_Xrotation, 0.0f));
                player_camera.transform.position = target.transform.position + (player_camera.transform.right * camera_Right_Distance -
                player_camera.transform.forward * camera_Distance_From_Character).normalized * (camera_Ray_Hit.distance - camera_collide_normal);
//                player_camera.transform.position += camera_Ray_Hit.normal * camera_collide_normal;
            }

            aim.transform.position = player_camera.transform.position + player_camera.transform.forward * aim_Distance;
        }

        public void First_Alt_Rotation()
        {
            alt_Yrotation += -KeyInputManager.Instance.key_mouseY * rotSpeed * Time.deltaTime;
            alt_Yrotation = Mathf.Clamp(alt_Yrotation, -85, 85);
            
            alt_Xrotation += KeyInputManager.Instance.key_mouseX * rotSpeed * Time.deltaTime;
            alt_Xrotation = Mathf.Clamp(alt_Xrotation, -90, 90);
            
            aim.transform.rotation = target.transform.rotation;
            aim.transform.rotation *= Quaternion.Euler(new Vector3(alt_Yrotation, alt_Xrotation, 0.0f));
            aim.transform.position = target.transform.position + aim.transform.forward * aim_Distance;

            player_camera.transform.position = target.transform.position;
            player_camera.transform.position += transform.forward * first_View_Distance;
            player_camera.transform.rotation = transform.rotation;
            player_camera.transform.rotation *= Quaternion.Euler(new Vector3(alt_Yrotation, alt_Xrotation, 0.0f));

        }
        public void Alt_Lerp()
        {
            if(Mathf.Abs(lerp_x) < Mathf.Abs(lerp_Xrotation))
            {
                xRotation += lerp_dx;
                lerp_x += lerp_dx;
                yRotation += lerp_dy;
                lerp_y += lerp_dy;
            }
            else
            {
                is_Lerp = false;
            }
        }
        public void Set_Lerp()
        {
            lerp_Xrotation = xRotation - alt_Xrotation;
            lerp_Yrotation = yRotation - alt_Yrotation;
            xRotation = alt_Xrotation;
            yRotation = alt_Yrotation;
            lerp_dx = lerp_Xrotation / 10;
            lerp_dy = lerp_Yrotation / 10;
            lerp_x = 0.0f;
            lerp_y = 0.0f;
            is_Lerp = true;
        }
    }


}