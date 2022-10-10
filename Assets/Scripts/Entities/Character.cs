using Controllers;
using UnityEngine;

namespace Entities
{
    public class Character : MonoBehaviour
    {
        private MovementController _movementController;

        [SerializeField] private KeyCode moveForward = KeyCode.W;
        [SerializeField] private KeyCode moveBackward = KeyCode.S;
        [SerializeField] private KeyCode moveLeft = KeyCode.A;
        [SerializeField] private KeyCode moveRight = KeyCode.D;
        
        [SerializeField] private KeyCode sprint = KeyCode.LeftShift;

        [SerializeField] private KeyCode shoot = KeyCode.Mouse0;
        [SerializeField] private KeyCode reload = KeyCode.R;

        private void Start()
        {
            _movementController = GetComponent<MovementController>();
        }

        private void Update()
        {
            _movementController.Rotate(Vector3.up * Input.GetAxis("Mouse X"));

            if (Input.GetKey(moveForward))
            {
                _movementController.Move(Vector3.forward);
            }

            if (Input.GetKey(moveBackward))
            {
                _movementController.Move(Vector3.back);
            }

            if (Input.GetKey(moveLeft))
            {
                _movementController.Move(Vector3.left);
            }

            if (Input.GetKey(moveRight))
            {
                _movementController.Move(Vector3.right);
            }

            if (Input.GetKeyDown(sprint))
            {
                _movementController.Sprint(true);
            }

            if (Input.GetKeyUp(sprint))
            {
                _movementController.Sprint(false);
            }
        }

    }
}