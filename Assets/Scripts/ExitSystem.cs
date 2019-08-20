using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitSystem : MonoBehaviour
{
   public void EndTheLevel()
    {
        if (GameManager.Instance.Return_CollectedStar() == 0)
        {
            GameManager.Instance.ActivateEndGamePanel();
        }
        else
        {
            GameManager.Instance.WarningTextActivate();
        }
    }
}
