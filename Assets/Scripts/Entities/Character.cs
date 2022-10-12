using Commands;
using EventQueues;
using Controllers;
using Flyweight;
using Managers;
using UnityEngine;

namespace Entities
{
    public class Character : MonoBehaviour
    {
        public CharacterStats CharacterStats => _characterStats;
        [SerializeField] private CharacterStats _characterStats;

        //Instancias
        private MovementController _movementController;
        private LifeController _lifeController;
        private Inventory _inventory;
        private Flashlight _flashlight;

        //Binding movements

        [SerializeField] private KeyCode moveForward = KeyCode.W;
        [SerializeField] private KeyCode moveBackward = KeyCode.S;
        [SerializeField] private KeyCode moveLeft = KeyCode.A;
        [SerializeField] private KeyCode moveRight = KeyCode.D;

        [SerializeField] private KeyCode jump = KeyCode.Space;
        [SerializeField] private KeyCode sprint = KeyCode.LeftShift;

        [SerializeField] private KeyCode pickup = KeyCode.G;
        [SerializeField] private KeyCode switchFlashlight = KeyCode.F;

        // Movement properties
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.3f;
        [SerializeField] private LayerMask groundMask;
        private bool _isGrounded;

        [SerializeField] private AudioSource _interactionsAudioSource;
        [SerializeField] private AudioClip _pickupNoteAudioClip;
        [SerializeField] private AudioClip _pickupFlashlightAudioClip;

        [SerializeField] private AudioSource _walkingAudioSource;
        [SerializeField] private AudioSource _sprintingAudioSource;


        //Commands
        private CmdMovement _cmdMoveForward;
        private CmdMovement _cmdMoveBack;
        private CmdMovement _cmdMoveLeft;
        private CmdMovement _cmdMoveRight;
        private CmdRotation _cmdRotation;
        private CmdJump _cmdJump;
        private CmdStartSprinting _cmdStartSprinting;
        private CmdStopSprinting _cmdStopSprinting;
        private CmdPickUpLantern _cmdPickUpLantern;
        private CmdPickUpNote _cmdPickUpNote;
        private CmdWin _cmdWin;
        private CmdSwitchLantern _cmdSwitchLantern;

        private bool hasFlashlight = false;


        private void Start()
        {
            _movementController = GetComponent<MovementController>();
            _lifeController = GetComponent<LifeController>();
            _inventory = GetComponent<Inventory>();
            _cmdMoveForward = new CmdMovement(_movementController, Vector3.forward);
            _cmdMoveBack = new CmdMovement(_movementController, Vector3.back);
            _cmdMoveLeft = new CmdMovement(_movementController, Vector3.left);
            _cmdMoveRight = new CmdMovement(_movementController, Vector3.right);
            _cmdStartSprinting = new CmdStartSprinting(_lifeController, _movementController);
            _cmdStopSprinting = new CmdStopSprinting(_lifeController, _movementController);
            _cmdJump = new CmdJump(_movementController, Vector3.up);
            _cmdWin = new CmdWin();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            UpdateMovement();
            UpdateMovementAudio();
            UpdateFlashlightState();
        }

        private void UpdateFlashlightState()
        {
            if (hasFlashlight && Input.GetKeyDown(switchFlashlight))
            {
                EventQueueManager.instance.AddCommand(_cmdSwitchLantern);
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Note"))
            {
                Note note = collision.GetComponent<Note>();
                _cmdPickUpNote = new CmdPickUpNote(note, _inventory, _interactionsAudioSource, _pickupNoteAudioClip);
                UpdateUIPanel("Press G to pickup");
            }

            if (collision.gameObject.CompareTag("Boat"))
            {
                if (_inventory.IsFull()) EventQueueManager.instance.AddCommand(_cmdWin);
                else
                {
                    UpdateUIPanel("You must collect all notes\n before returning to the boat");
                }
            }

            if (collision.gameObject.CompareTag("Flashlight"))
            {
                _flashlight = gameObject.GetComponentInChildren<Flashlight>(true);
                Flashlight floorFlashlight = collision.GetComponent<Flashlight>();
                _cmdPickUpLantern = new CmdPickUpLantern(floorFlashlight, _flashlight, _interactionsAudioSource,
                    _pickupFlashlightAudioClip);
                UpdateUIPanel("Press G to pickup");
            }
        }

        void OnTriggerStay(Collider collision)
        {
            if (collision.gameObject.CompareTag("Note"))
            {
                if (Input.GetKey(pickup))
                {
                    EventQueueManager.instance.AddCommand(_cmdPickUpNote);
                    UpdateUIPanel(null);
                }
            }

            if (collision.gameObject.CompareTag("Flashlight"))
            {
                if (Input.GetKey(pickup))
                {
                    EventQueueManager.instance.AddCommand(_cmdPickUpLantern);
                    hasFlashlight = true;
                    _cmdSwitchLantern = new CmdSwitchLantern(_flashlight);
                    UpdateUIPanel(null);
                }
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("Note") || collision.gameObject.CompareTag("Flashlight") ||
                collision.gameObject.CompareTag("Boat"))
            {
                UpdateUIPanel(null);
            }
        }

        private void UpdateMovementAudio()
        {
            if (Input.GetKey(moveForward) || Input.GetKey(moveBackward) || Input.GetKey(moveLeft) ||
                Input.GetKey(moveRight))
            {
                _walkingAudioSource.enabled = true;
                if (_movementController.Sprinting)
                {
                    _walkingAudioSource.enabled = false;
                    _sprintingAudioSource.enabled = true;
                }
                else
                {
                    _sprintingAudioSource.enabled = false;
                }
            }
            else
            {
                _walkingAudioSource.enabled = false;
                _sprintingAudioSource.enabled = false;
            }
        }

        private void UpdateMovement()
        {
            float mouseX = Input.GetAxis("Mouse X");
            _cmdRotation = new CmdRotation(_movementController, Vector3.up * mouseX);
            EventQueueManager.instance.AddMovementCommand(_cmdRotation);

            if (Input.GetKey(moveForward)) EventQueueManager.instance.AddMovementCommand(_cmdMoveForward);
            if (Input.GetKey(moveBackward)) EventQueueManager.instance.AddMovementCommand(_cmdMoveBack);
            if (Input.GetKey(moveLeft)) EventQueueManager.instance.AddMovementCommand(_cmdMoveLeft);
            if (Input.GetKey(moveRight)) EventQueueManager.instance.AddMovementCommand(_cmdMoveRight);
            if (Input.GetKey(sprint))
            {
                EventQueueManager.instance.AddMovementCommand(_cmdStartSprinting);
            }
            else
            {
                if (_lifeController.CurrentStamina < _lifeController.MaxStamina)
                {
                    _lifeController.increaseStamina(0.5f);
                }
            }

            if (Input.GetKeyUp(sprint)) EventQueueManager.instance.AddCommand(_cmdStopSprinting);

            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (Input.GetKeyDown(jump) && _isGrounded) EventQueueManager.instance.AddMovementCommand(_cmdJump);
        }

        private void UpdateUIPanel(string message)
        {
            EventManager.instance.UIPanelUpdate(message);
        }
    }
}