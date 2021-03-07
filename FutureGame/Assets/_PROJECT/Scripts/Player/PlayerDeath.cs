using System;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    public void PlayerHasDied()
    {
        CursorVisibility.SetCursorVisible();
        MessageSender.SendMessageToClients("PlayerDeathEvent");
    }


}
