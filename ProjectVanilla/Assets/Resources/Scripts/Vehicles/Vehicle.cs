using UnityEngine;

namespace Resources.Scripts.Vehicles
{
    public class Vehicle : MonoBehaviour
    {
        public float HoverForce = 65.0f;
        public float HoverHeight = 3.5f;
        public float MaxSpeed = 100.0f;
        public float PowerInput;
        public float Speed = 90.0f;
        public float TurnInput;
        public float TurnSpeed = 5f;

       
        
        private void FixedUpdate()
        {
            var ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, HoverHeight))
            {
                var propHeight = (HoverHeight - hit.distance) / HoverHeight;
                var appliedHoverForce = Vector3.up * propHeight * HoverForce;
                GetComponent<Rigidbody>().AddForce(appliedHoverForce, ForceMode.Acceleration);
            }

            GetComponent<Rigidbody>().AddRelativeForce(0, 0, PowerInput * Speed);
            GetComponent<Rigidbody>().AddRelativeTorque(0, TurnInput * TurnSpeed, 0);
        }

        private void Update()
        {
            PowerInput = Input.GetAxis("Vertical");
            TurnInput = Input.GetAxis("Horizontal");
            var forwardDraw = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(transform.position, forwardDraw, Color.red);
            Accelerate();
        }

        private void Accelerate()
        {
            if (Input.GetKey(KeyCode.U)) GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * Speed);
            if (Input.GetKey(KeyCode.J)) GetComponent<Rigidbody>().AddRelativeForce(-Vector3.up * Speed);
            if (Input.GetKey(KeyCode.K)) GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Speed);
            if (Input.GetKey(KeyCode.H)) GetComponent<Rigidbody>().AddRelativeForce(-Vector3.forward * Speed);
            if (GetComponent<Rigidbody>().velocity.magnitude > MaxSpeed) GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * MaxSpeed;
        }
    }
}