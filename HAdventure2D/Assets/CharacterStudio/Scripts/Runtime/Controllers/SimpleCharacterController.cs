using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace CharacterStudio
{
    public class SimpleCharacterController : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private SpriteResolver _spriteResolver;

        private int movementFrame = 0;
        private float animationTimer = 0f;
        public float movementFrameRate = 0.15f;

        public float interval = 0.1f;
        public float moveSpeed = 2.0f;

        private readonly Dictionary<string, string[]> movementAnimations = new Dictionary<string, string[]>
        {
            { "Idle", new[] { "Idle_0", "Idle_1", "Idle_2", "Idle_3", "Idle_2", "Idle_1", "Idle_0" } },
            { "Walk", new[] { "Walk_0", "Walk_1", "Walk_2", "Walk_3", "Walk_4", "Walk_5", "Walk_6", "Walk_7" } },
            { "Run", new[] { "Run_0", "Run_1", "Run_2", "Run_3", "Run_4", "Run_5", "Run_6", "Run_7" } },
        };

        private readonly string[][] attackAnimations =
        {
            new[] { "SAttack_01_0", "SAttack_01_1", "SAttack_01_2", "SAttack_01_3", "SAttack_01_4", "SAttack_01_5", "SAttack_01_6", "SAttack_01_7" },
            new[] { "SAttack_02_0", "SAttack_02_1", "SAttack_02_2", "SAttack_02_3", "SAttack_02_4", "SAttack_02_5", "SAttack_02_6", "SAttack_02_7", "SAttack_02_8", "SAttack_02_9", "SAttack_02_10", "SAttack_02_11" },
            new[] { "SAttack_03_0", "SAttack_03_1", "SAttack_03_2", "SAttack_03_3", "SAttack_03_4", "SAttack_03_5", "SAttack_03_6", "SAttack_03_7", "SAttack_03_8", "SAttack_03_9", "SAttack_03_10", "SAttack_03_11" },
        };

        private int currentMovement = 0; // 0: Idle, 1: Walk, 2: Run
        private bool isAttacking = false;

        private void Awake()
        {
            _spriteResolver = GetComponent<SpriteResolver>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            SetAnimation("Idle", movementAnimations["Idle"][0]);
        }

        private void Update()
        {
            HandleMovementInput();
            HandleAttackInput();
            AnimateMovement();
        }

        private void HandleMovementInput()
        {
            if (isAttacking) return;

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(h, v, 0f).normalized;
            if (direction.magnitude > 0.01f)
            {
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
            if (direction != Vector3.zero)
            {
                _spriteRenderer.flipX = direction.x < 0; // Flip sprite based on horizontal input
            }

            int newMovement = 0; // Idle

            if (h != 0 || v != 0)
            {
                newMovement = Input.GetKey(KeyCode.LeftShift) ? 2 : 1; // Run or Walk
            }

            if (newMovement != currentMovement)
            {
                currentMovement = newMovement;
                movementFrame = 0;
                animationTimer = 0f;

                string category = GetCurrentMovementCategory();
                string[] frames = movementAnimations[category];
                SetAnimation(category, frames[0]);
            }
        }

        private void AnimateMovement()
        {
            if (isAttacking) return;

            string category = GetCurrentMovementCategory();
            if (!movementAnimations.TryGetValue(category, out var frames)) return;

            animationTimer += Time.deltaTime;
            if (animationTimer >= movementFrameRate)
            {
                animationTimer = 0f;
                movementFrame = (movementFrame + 1) % frames.Length;
                SetAnimation(category, frames[movementFrame]);
            }
        }

        private string GetCurrentMovementCategory()
        {
            return currentMovement switch
            {
                1 => "Walk",
                2 => "Run",
                _ => "Idle"
            };
        }

        private void HandleAttackInput()
        {
            if (Input.GetKeyDown(KeyCode.C) && !isAttacking)
            {
                StartCoroutine(AttackCombo());
            }
        }

        private IEnumerator AttackCombo()
        {
            isAttacking = true;

            for (int i = 0; i < attackAnimations.Length; i++)
            {
                string category = $"SAttack_0{i + 1}";
                string[] frames = attackAnimations[i];

                for (int j = 0; j < frames.Length; j++)
                {
                    SetAnimation(category, frames[j]);
                    yield return new WaitForSeconds(interval);
                }
            }

            isAttacking = false;
            movementFrame = 0;
            SetAnimation(GetCurrentMovementCategory(), movementAnimations[GetCurrentMovementCategory()][0]);
        }

        private void SetAnimation(string category, string label)
        {
            if (_spriteResolver != null)
            {
                _spriteResolver.SetCategoryAndLabel(category, label);
            }
        }
    }
}
