using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform _camera = null;
    [SerializeField] private float _parallaxModifierX = 0f;
    [SerializeField] private float _parallaxModifierY = 0f;
    [SerializeField] private SpriteRenderer _firstImage = null;
    [SerializeField] private SpriteRenderer _secondImage = null;
    private float _backgroundLength = 0;
    private Vector2 _backgroundStartPosition;
    private Vector3 _moveVector;
    private float _offestFromCamera = 0f;
    private float _lastCameraPosition = 0f;
    private float _distanceMoved = 0f;

    public float ParalaxModifierY { get => _parallaxModifierY; set => _parallaxModifierY = value; }

    private void Start()
    {
        _backgroundStartPosition = transform.position;
        _backgroundLength += _firstImage.bounds.size.x / 2;
        _backgroundLength += Vector3.Distance(_secondImage.transform.position, _firstImage.transform.position);
        _backgroundLength += _secondImage.bounds.size.x / 2;
        //Debug.Log(_backgroundLength);
        _moveVector = new Vector3();
        _moveVector.z = transform.position.z;
        _lastCameraPosition = _camera.position.x;
    }

    private void FixedUpdate()
    {
        _distanceMoved = _parallaxModifierX * _camera.position.x;
        _moveVector.x = _backgroundStartPosition.x + _distanceMoved;

        _distanceMoved = _parallaxModifierY * _camera.position.y;
        _moveVector.y = _backgroundStartPosition.y + _distanceMoved;

        transform.position = _moveVector;

        //float offsetFromCamera = _camera.position.x * (1 - _parallaxModifier);

        //if (offsetFromCamera > _backgroundStartPosition + _backgroundLength)
        //{
        //    _backgroundStartPosition += _backgroundLength;
        //}
        //else if(offsetFromCamera < _backgroundStartPosition - _backgroundLength)
        //{
        //    _backgroundStartPosition -= _backgroundLength;
        //}
    }
}
