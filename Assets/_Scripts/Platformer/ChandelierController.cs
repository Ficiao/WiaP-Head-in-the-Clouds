using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ChandelierController : MonoBehaviour
    {
        [SerializeField] private List<Animator> _highlightAnimators = null;
        [SerializeField] private GameObject _destructableWire = null;
        [SerializeField] private List<GameObject> _cage = null;
        [SerializeField] private CameraFollow _followCamera = null;
        [SerializeField] private CinematicCamera _cinematicCamera = null;
        [SerializeField] private PlayerController _playerController = null;
        [SerializeField] private Weapon _playerWeapon = null;
        [SerializeField] private List<GameObject> _rails = null;

        public void WireActivated() 
        {
            _highlightAnimators.ForEach(a => a.SetBool("Highlight", true));
            _destructableWire.SetActive(true);
            _cage.ForEach(c => c.SetActive(true));
            return;
        }

        public void WireSnapped()
        {
            _highlightAnimators.ForEach(a => a.SetBool("Highlight", false));
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _followCamera.enabled = false;
            _cinematicCamera.enabled = true;
            _playerController.enabled = false;
            _playerWeapon.enabled = false;
            _cage.ForEach(c => c.SetActive(false));
            _rails.ForEach(r => r.SetActive(true));
            _playerController.transform.parent = transform;
            _playerController.transform.GetComponent<Rigidbody2D>().gravityScale = 1f;
            _playerController.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
}
