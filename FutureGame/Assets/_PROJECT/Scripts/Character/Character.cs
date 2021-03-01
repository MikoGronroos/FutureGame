using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] protected Race characterRace;

    public virtual Race GetCharacterRace()
    {
        return characterRace;
    }

}
