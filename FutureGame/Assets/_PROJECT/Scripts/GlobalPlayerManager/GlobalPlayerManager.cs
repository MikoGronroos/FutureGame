using System.Collections.Generic;
using UnityEngine;

public class GlobalPlayerManager : MonoBehaviour
{

    private List<CharacterOwner> _players = new List<CharacterOwner>();

    #region Singleton

    private static GlobalPlayerManager _instance;

    public static GlobalPlayerManager Instance
    {
        get
        {
            return _instance;
        }
    }

    #endregion

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].SpawnPlayer.Spawn(WorldManager.Instance.SpawnPoint.SpawnPoint);
        }
    }

    public void AddPlayerToList(CharacterOwner player)
    {
        _players.Add(player);
    }

}
