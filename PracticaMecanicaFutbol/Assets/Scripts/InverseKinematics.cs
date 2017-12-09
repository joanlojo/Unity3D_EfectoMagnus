using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ENTICourse.IK
{

    // A typical error function to minimise
    public delegate float ErrorFunction(Our_Vector3 target, float[] solution);

    public struct PositionRotation
    {
        Our_Vector3 position;
        Our_Quaternion rotation;

        public PositionRotation(Our_Vector3 position, Our_Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        // PositionRotation to Vector3
        public static implicit operator Our_Vector3(PositionRotation pr)
        {
            return pr.position;
        }
        // PositionRotation to Quaternion
        public static implicit operator Our_Quaternion(PositionRotation pr)
        {
            return pr.rotation;
        }
    }

    //[ExecuteInEditMode]
    public class InverseKinematics : MonoBehaviour
    {
        [Header("Joints")]
        public Transform BaseJoint;


        // [ReadOnly]
        public RobotJoints[] Joints = null;
        // The current angles
        //[ReadOnly]
        public float[] Solution = null;

        [Header("Destination")]
        public Transform Effector;
        [Space]
        public Transform Destination;
        public float DistanceFromDestination;
        private Our_Vector3 target = new Our_Vector3(0, 0, 0);

        [Header("Inverse Kinematics")]
        [Range(0, 1f)]
        public float DeltaGradient = 0.1f; // Used to simulate gradient (degrees)
        [Range(0, 100f)]
        public float LearningRate = 0.25f; // How much we move depending on the gradient

        [Space()]
        [Range(0, 1f)]
        public float StopThreshold = 0f; // If closer than this, it stops
        [Range(0, 10f)]
        public float SlowdownThreshold = 0f; // If closer than this, it linearly slows down


        public ErrorFunction ErrorFunction;



        [Header("Debug")]
        public bool DebugDraw = true;



        // Use this for initialization
        void Start()
        {
            if (Joints == null)
                GetJoints();

            ErrorFunction = DistanceFromTarget;
        }

        //[ExposeInEditor(RuntimeOnly = false)]
        public void GetJoints()
        {
            Joints = BaseJoint.GetComponentsInChildren<RobotJoints>();
            Solution = new float[Joints.Length];
        }



        // Update is called once per frame
        void Update()
        {
            // Do we have to approach the target?
            //TODO
            target.x = Destination.transform.position.x;
            target.y = Destination.transform.position.y;
            target.z = Destination.transform.position.z;
            // ApproachTarget(target);
            //ForwardKinematics(Solution);
            if (DistanceFromTarget(target, Solution) > StopThreshold)
            {
                Debug.Log("a");
                ApproachTarget(target);
                for (int i = 0; i < Joints.Length; i++)
                {

                    Joints[i].MoveArm(Solution[i]);
                }
            }

            if (DebugDraw)
            {
                Debug.DrawLine(Effector.transform.position, new Vector3(target.x, target.y, target.z), Color.green);
                Debug.DrawLine(Destination.transform.position, new Vector3(target.x, target.y, target.z), new Color(0, 0.5f, 0));
            }
        }

        public void ApproachTarget(Our_Vector3 target)
        {
            //TODO
            for (int i = 0; i < Solution.Length; i++)
            {
                float gradient = CalculateGradient(target, Solution, i, DeltaGradient);
                Solution[i] -= LearningRate * gradient;
            }
        }


        public float CalculateGradient(Our_Vector3 target, float[] Solution, int i, float delta)
        {
            //TODO 
            float gradient = 0;
            float solutionAngle = Solution[i];
            float distance = DistanceFromTarget(target, Solution);
            Solution[i] += delta;
            float distancePlus = DistanceFromTarget(target, Solution);
            gradient = (distancePlus - distance) / delta;

            return gradient;
        }

        // Returns the distance from the target, given a solution
        public float DistanceFromTarget(Our_Vector3 target, float[] Solution)
        {
            Our_Vector3 dist = new Our_Vector3(0, 0, 0);
            Our_Vector3 point = ForwardKinematics(Solution);
            dist.x = point.x - target.x;
            dist.y = point.y - target.y;
            dist.z = point.z - target.z;
            return dist.Module(); //ns si esto es asi, devolvia vecotr3.distnce(point,target)
        }


        /* Simulates the forward kinematics,
         * given a solution. */



        public PositionRotation ForwardKinematics(float[] Solution)
        {
            Our_Vector3 prevPoint = new Our_Vector3(0, 0, 0);
            prevPoint.x = Joints[0].transform.position.x;
            prevPoint.y = Joints[0].transform.position.y;
            prevPoint.z = Joints[0].transform.position.z;
            //Quaternion rotation = Quaternion.identity;

            // Takes object initial rotation into account
            Our_Quaternion rotation = new Our_Quaternion(0, new Our_Vector3(0, 0, 0));
            rotation.x = transform.rotation.x;
            rotation.y = transform.rotation.y;
            rotation.z = transform.rotation.z;
            rotation.w = transform.rotation.w;
            for (int i = 1; i < Joints.Length; i++)
            {
                // Rotates around a new axis
                rotation.Multiply(new Our_Quaternion(Solution[i - 1], Joints[i - 1].axis)); 
                Our_Vector3 nextPoint = new Our_Vector3(0, 0, 0);
                nextPoint.x = prevPoint.x + rotation.x * Joints[i].StartOffset.x;
                nextPoint.y = prevPoint.y + rotation.y * Joints[i].StartOffset.y;
                nextPoint.z = prevPoint.z + rotation.z * Joints[i].StartOffset.z;


                if (DebugDraw)
                    //Debug.DrawLine(prevPoint, nextPoint, Color.blue);

                    prevPoint = nextPoint;
            }

            // The end of the effector
            return new PositionRotation(prevPoint, rotation);
        }
    }
}