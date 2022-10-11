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

        [SerializeField] private KeyCode sprint = KeyCode.LeftShift;

        [SerializeField] private KeyCode shoot = KeyCode.Mouse0;
        [SerializeField] private KeyCode reload = KeyCode.R;
        [SerializeField] private KeyCode grab = KeyCode.F;

        private Item _item = null;
        private bool _isCollidingWithItem = false;

        private void Start()
        {
            _movementController = GetComponent<MovementController>();
            _lifeController = GetComponent<LifeController>();
            _inventory = GetComponent<Inventory>();
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

            if (Input.GetKeyDown(grab))
            {
                if (_isCollidingWithItem)
                {
                    _item.PickUpItem();
                    _inventory.StoreItem();
                    CollisionWithItem(false, null);
                }
            }
        }


        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Note"))
            {
                _item = collision.GetComponent<Item>();
                CollisionWithItem(true, _item);
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("Note"))
            {
                CollisionWithItem(false, null);
            }
        }

        private void CollisionWithItem(bool isColliding, Item item)
        {
            _isCollidingWithItem = isColliding;
            _item = item;
            UpdateUIItemCollision(isColliding);
        }

        private void UpdateUIItemCollision(bool show)
        {
            string message = show ? "Press F to grab" : "";
            EventManager.instance.UIPanelUpdate(show, message);
        }
    }
}