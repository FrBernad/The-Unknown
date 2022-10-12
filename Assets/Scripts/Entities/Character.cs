using Controllers;
using Managers;
using UnityEngine;

namespace Entities
{
    public class Character : MonoBehaviour
    {
        private MovementController _movementController;
        private LifeController _lifeController;
        private Inventory _inventory;

        [SerializeField] private KeyCode moveForward = KeyCode.W;
        [SerializeField] private KeyCode moveBackward = KeyCode.S;
        [SerializeField] private KeyCode moveLeft = KeyCode.A;
        [SerializeField] private KeyCode moveRight = KeyCode.D;

        [SerializeField] private KeyCode jump = KeyCode.Space;
        [SerializeField] private KeyCode sprint = KeyCode.LeftShift;

        [SerializeField] private KeyCode pickup = KeyCode.G;
        [SerializeField] private KeyCode switchFlashlight = KeyCode.F;

        private Flashlight _flashlight;

        private bool _isGrounded;

        public Transform groundCheck;
        public float groundDistance = 0.3f;
        public LayerMask groundMask;

        public float jumpHeight = 1f;
        private const float _GRAVITY = -9.81f;

        private void Start()
        {
            _movementController = GetComponent<MovementController>();
            _lifeController = GetComponent<LifeController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _inventory = GetComponent<Inventory>();
        }

        private void Update()
        {
            UpdateMovement();
            UpdateFlashlight();
        }

        private void UpdateFlashlight()
        {
            if (_flashlight != null && Input.GetKeyDown(switchFlashlight))
            {
                _flashlight.Switch();
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Note"))
            {
                UpdateUIPanel("Press G to pickup");
            }

            if (collision.gameObject.CompareTag("Boat"))
            {
                if (_inventory.IsFull())
                {
                    EventManager.instance.GameOver(true);
                    UpdateUIPanel(null);
                }
                else
                {
                    UpdateUIPanel("You must collect all notes\n before returning to the boat");
                }
            }

            if (_flashlight == null && collision.gameObject.CompareTag("Flashlight"))
            {
                UpdateUIPanel("Press G to pickup");
            }
        }

        void OnTriggerStay(Collider collision)
        {
            if (collision.gameObject.CompareTag("Note"))
            {
                if (Input.GetKey(pickup))
                {
                    Note note = collision.GetComponent<Note>();
                    note.Pickup();
                    _inventory.StoreItem();
                    UpdateUIPanel(null);
                }
            }

            if (_flashlight == null && collision.gameObject.CompareTag("Flashlight"))
            {
                if (Input.GetKey(pickup))
                {
                    Flashlight floorFlashlight = collision.GetComponent<Flashlight>();
                    floorFlashlight.Pickup();

                    _flashlight = gameObject.GetComponentInChildren<Flashlight>(true);
                    _flashlight.gameObject.SetActive(true);
                    UpdateUIPanel(null);
                }
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("Note"))
            {
                UpdateUIPanel(null);
            }

            if (collision.gameObject.CompareTag("Flashlight"))
            {
                UpdateUIPanel(null);
            }

            else if (collision.gameObject.CompareTag("Boat"))
            {
                UpdateUIPanel(null);
            }
        }


        private void UpdateMovement()
        {
            float mouseX = Input.GetAxis("Mouse X");
            _movementController.Rotate(Vector3.up * mouseX);

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

            if (Input.GetKey(sprint))
            {
                if (_lifeController.CurrentStamina > 0)
                {
                    _movementController.Sprint(true);
                    _lifeController.decreaseStamina(0.3f);
                }
                else
                {
                    _movementController.Sprint(false);
                }
            }
            else
            {
                if (_lifeController.CurrentStamina < _lifeController.MaxStamina)
                {
                    _lifeController.increaseStamina(0.5f);
                }
            }

            if (Input.GetKeyUp(sprint))
            {
                _movementController.Sprint(false);
            }

            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (Input.GetKeyDown(jump) && _isGrounded)
            {
                float force = Mathf.Sqrt(jumpHeight * -2f * _GRAVITY);
                _movementController.Jump(Vector3.up * force);
            }
        }

        private void UpdateUIPanel(string message)
        {
            EventManager.instance.UIPanelUpdate(message);
        }
    }
}