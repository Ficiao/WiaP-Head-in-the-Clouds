using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerina : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator = null;
    [SerializeField] private Animator _playerinaAnimator = null;

    private void Update()
    {
        if (_playerAnimator.GetBool("Dance"))
        {
            _playerinaAnimator.SetBool("Dance", true);
            this.enabled = false;
        }
    }
}
