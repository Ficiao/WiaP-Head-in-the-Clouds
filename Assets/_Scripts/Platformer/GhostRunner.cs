using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class GhostRunner : MonoBehaviour, IAggroable
    {
        [SerializeField] private RunnerMob _runnerMobScript = null;
        [SerializeField] private bool _goLeft = false;
        [SerializeField] private bool _falling = false;
        [SerializeField] private float _moveSpeed = 0.4f;
        [SerializeField] private float _fallSpeed = 0.7f;
        [SerializeField] private float _aggroRunModifier = 1.15f;
        private Vector3 _moveVector;
        private Vector3 _runVector;
        private int _damage;
        private Transform _player;
        private AggroStateType _aggroState;

        public bool Falling { get => _falling; set => _falling = value; }

        private void Start()
        {
            _moveVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _runVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _damage = _runnerMobScript.Damage;
            _runnerMobScript.enabled = false;
            _aggroState = AggroStateType.Idle;
            _runnerMobScript.Health = _runnerMobScript.MaxHealth;
        }

        private void Update()
        {
            if(_falling)
            {
                _moveVector.y = transform.position.y - (_fallSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, _moveVector, Vector3.Distance(transform.position, _moveVector) + 1);
            }
            else if (_aggroState == AggroStateType.Idle)
            {
                _moveVector.x = transform.position.x + (_moveSpeed * (_goLeft ? -1 : 1) * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, _moveVector, Vector3.Distance(transform.position, _moveVector) + 1);
            }
            else if(_aggroState == AggroStateType.Aggored)
            {
                _runVector.x = _player.position.x;
                _moveVector.x = transform.position.x + (_moveSpeed * (_goLeft ? -1 : 1) * Time.deltaTime * _aggroRunModifier);
                transform.position = Vector3.MoveTowards(transform.position, _runVector, Vector3.Distance(transform.position, _moveVector));

                if (Mathf.Abs(transform.position.x - _player.position.x) < 0.1f)
                {
                    _falling = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Wall") || collision.CompareTag("MobWall"))
            {
                if(_aggroState == AggroStateType.Aggored)
                {
                    _falling = true;
                }

                _goLeft = !_goLeft;
                return;
            }

            if(_falling && collision.CompareTag("Ground"))
            {
                Fell();
            }

            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerStats>().TakeDamage(_damage);
                _runnerMobScript.Die();
            }
        }

        private void Fell()
        {
            transform.Rotate(0, 0, 180);
            transform.Translate(Vector3.down * 0.322f);
            _falling = false;
            _runnerMobScript.enabled = true;
            this.enabled = false;
        }

        public void Aggro(Transform target)
        {
            _aggroState = AggroStateType.Aggored;
            _player = target;
            _runnerMobScript.Aggro(target);
        }

        public void Disaggro()
        {
            _aggroState = AggroStateType.Idle;
            _runnerMobScript.Disaggro();
        }
    }
}