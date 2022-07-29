using System.Collections;
using UnityEngine;

namespace Platformer {
    public class ScreenShake : Singleton<ScreenShake>
    {
        [SerializeField] private CameraFollow _camera = null;
        [SerializeField] private AnimationCurve _modifierCurve = null;
        [SerializeField] private float _strength = 0f;
        private float _duration;
        private bool _isShaking = false;
        private Vector3 _startOffset;
        private Vector3 _moveOffset;

        public void StartShaking(float duration)
        {
            if (_isShaking) return;
            _isShaking = true;
            _duration = duration;
            StartCoroutine(Shaking());
        }

        private IEnumerator Shaking()
        {
            _startOffset = _camera.Offset;
            float elapsedTime = 0f;

            while(elapsedTime <= _duration)
            {
                elapsedTime += Time.deltaTime;
                float curveStrength = _modifierCurve.Evaluate(elapsedTime / _duration);
                _moveOffset = _startOffset + Random.insideUnitSphere * curveStrength * _strength;
                _moveOffset.z = _startOffset.z;
                _camera.Offset = _moveOffset;
                yield return null;
            }

            _camera.Offset = _startOffset;
            _isShaking = false;
        }
    } 
}
