using System;
using Source.Gadgeteers.Game.Effects;
using Source.Gadgeteers.Game.Entities;
using Source.Gadgeteers.Game.Items;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Source.Gadgeteers.Game
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Player))]
    public class PlayerController : SingletonBehaviour<PlayerController>
    {
        private Player _player;
		private Controls _controls;
        
        [SerializeField]
        public UnityEvent OpenCloseInventory;
        
        public Player Player => _player;
        
        protected override void OnAwake()
        {
            _player = GetComponent<Player>();
			_controls = new Controls();
            
            // Controls events
            _controls.Player.Attack.performed += OnAttack;
            _controls.Player.UseGadget1.performed += c => OnUseGadgetPerformed(c, 1);
            _controls.Player.UseGadget1.canceled += c => OnUseGadgetCanceled(c, 1);
            _controls.Player.UseGadget2.performed += c => OnUseGadgetPerformed(c, 2);
            _controls.Player.UseGadget2.canceled += c => OnUseGadgetCanceled(c, 2);
            _controls.Player.UseGadget3.performed += c => OnUseGadgetPerformed(c, 3);
            _controls.Player.UseGadget3.canceled += c => OnUseGadgetCanceled(c, 3);
            _controls.Player.UseGadget4.performed += c => OnUseGadgetPerformed(c, 4);
            _controls.Player.UseGadget4.canceled += c => OnUseGadgetCanceled(c, 4);
            
            _controls.Player.OpenCloseInventory.performed += OnOpenCloseInventory;
        }

        private void Start()
        {
            _player.EffectCtrl.Apply<PoisonEffect>(_player, 10);
            
            var weapon = Util.CreateFromPrefab<Knuckles>();
            _player.Inventory.Put(weapon, 0);
            
            var g1 = Util.CreateFromPrefab<LoyalSpear>();
            _player.Inventory.Put(g1, 1);
            
            var g2 = Util.CreateFromPrefab<Quaker>();
            _player.Inventory.Put(g2, 2);
            
            var boots = Util.CreateFromPrefab<Crushers>();
            _player.Inventory.Put(boots, 3);
            
            var suit = Util.CreateFromPrefab<WardensJacket>();
            _player.Inventory.Put(suit, 4);
        }

        private void Update()
        {
            // Movement
            var cam = Camera.main;
            var dir = _controls.Player.Move.ReadValue<Vector2>().normalized;
            
            Vector2 face = Input.mousePosition - new Vector3(cam.scaledPixelWidth / 2f, cam.scaledPixelHeight / 2f, 0);
            face.y /= MathF.Sin(cam.transform.rotation.eulerAngles.x * MathF.PI / 180);
            face.Normalize();
            
            _player.transform.LookAt(new Vector3(face.x, 0, face.y) + _player.transform.position);
            _player.Move(dir * Time.deltaTime);
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            if (_player.Inventory[0] is Weapon weapon && weapon.Cooldown.IsReady)
            {
                weapon.Attack();
            }
        }

        private void OnUseGadgetPerformed(InputAction.CallbackContext context, int slot)
        {
            if (_player.Inventory[slot] is Gadget gadget and IHasHitbox hasHitbox && gadget.Cooldown.IsReady)
            {
                hasHitbox.Hitbox.SetMarkerVisible(true);
            }
        }

        private void OnUseGadgetCanceled(InputAction.CallbackContext context, int slot)
        {
            if (_player.Inventory[slot] is Gadget gadget && gadget.Cooldown.IsReady)
            {
                if (gadget is IHasHitbox hasHitbox)
                {
                    hasHitbox.Hitbox.SetMarkerVisible(false);
                }
                gadget.Use(context);
            }
        }
        
        private void OnOpenCloseInventory(InputAction.CallbackContext context)
        {
            OpenCloseInventory?.Invoke();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }
        
        private void OnDisable()
        {
			_controls.Disable();
        }
    }
}