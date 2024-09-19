using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("PARAMETERS")]
    [Tooltip("Card sound")]
    [SerializeField] private AudioSource _cardAudioSource = null;

    #region PUBLIC FIELDS

    public AudioSource CardAudioSource { get => _cardAudioSource; set => _cardAudioSource = value; }

    #endregion
}
