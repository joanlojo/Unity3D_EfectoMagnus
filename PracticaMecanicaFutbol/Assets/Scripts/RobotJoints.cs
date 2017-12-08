using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ENTICourse.IK
{

    public class RobotJoints : MonoBehaviour
    {

        [Header("Joint Limits")]
        // A single 1, which is the axes of movement
        public Vector3 axis;
        public Our_Vector3 Axis = new Our_Vector3(0, 0, 0);
        public float MinAngle;
        public float MaxAngle;

        [Header("Initial position")]
        // The offset at resting position
        //[ReadOnly]
        public Vector3 startOffset;
        public Our_Vector3 StartOffset = new Our_Vector3(0, 0, 0);

        // The initial one
        //[ReadOnly]
        public Vector3 zeroEuler;
        public Our_Vector3 ZeroEuler = new Our_Vector3(0, 0, 0);

        [Header("Movement")]
        // It lerps the speed to zero, from this distance
        [Range(0, 1f)]
        public float SlowdownThreshold = 0.5f;
        [Range(0, 360f)]
        public float Speed = 1f; // Degrees per second




        void Awake()
        {
            ZeroEuler.x = zeroEuler.x;
            ZeroEuler.y = zeroEuler.y;
            ZeroEuler.z= zeroEuler.z;
            ZeroEuler.x = transform.localEulerAngles.x;
            ZeroEuler.y = transform.localEulerAngles.y;
            ZeroEuler.z = transform.localEulerAngles.z;

            StartOffset.x = startOffset.x;
            StartOffset.y = startOffset.y;
            StartOffset.z= startOffset.z;

            Axis.x = axis.x;
            Axis.y = axis.y;
            Axis.z= axis.z;


            StartOffset.x = transform.localPosition.x;
            StartOffset.y = transform.localPosition.y;
            StartOffset.z = transform.localPosition.z;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        // Try to move the angle by delta.
        // Returns the new angle.
        public float ClampAngle(float angle, float delta = 0)
        {
            return Mathf.Clamp(angle + delta, MinAngle, MaxAngle);
        }

        // Get the current angle
        public float GetAngle()
        {
            float angle = 0;
            if (Axis.x == 1) angle = transform.localEulerAngles.x;
            else
            if (Axis.y == 1) angle = transform.localEulerAngles.y;
            else
            if (Axis.z == 1) angle = transform.localEulerAngles.z;

            return ClampAngle(angle); //clamp
        }
        public float SetAngle(float angle)
        {
            angle = ClampAngle(angle);
            if (Axis.x == 1)
            {
                Our_Quaternion rot = new Our_Quaternion(angle * Mathf.Rad2Deg, new Our_Vector3(1f, 0f, 0f));
                transform.localEulerAngles = new Vector3(angle, 0, 0);
            }
            else if (Axis.y == 1)
            {
                Our_Quaternion rot = new Our_Quaternion(angle * Mathf.Rad2Deg, new Our_Vector3(0f, 1f, 0f));
                transform.localEulerAngles = new Vector3(0, angle, 0);
            }
            else if (Axis.z == 1)
            {
                Our_Quaternion rot = new Our_Quaternion(angle * Mathf.Rad2Deg, new Our_Vector3(0f, 0f, 1f));
                transform.localEulerAngles = new Vector3(0, 0, angle);
            }
            return angle;
        }



        // Moves the angle to reach 
        public float MoveArm(float angle)
        {
            return SetAngle(angle);
        }

        private void OnDrawGizmos()
        {
            Debug.DrawLine(transform.position, transform.parent.position, Color.red);
        }
    }
}