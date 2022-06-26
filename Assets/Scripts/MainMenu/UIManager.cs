using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class UIManager : MonoBehaviour
    {
        public void LoadBossBattle()
        {
            SceneManager.LoadScene("BossLevel");
        }

        public void LoadPlatformer()
        {

        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}

