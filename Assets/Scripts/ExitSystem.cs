using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitSystem : MonoBehaviour
{
   public void EndTheLevel()
    {
        if (GameManager.Instance.Return_CollectedStar() == 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            GameManager.Instance.WarningTextActivate();
        }
    }
}
