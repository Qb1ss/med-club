using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("PARAMETERS")]
    [Tooltip("Card sound")]
    [SerializeField] private AudioSource _cardAudioSource = null;
    [Space (height: 10f)]

    [Tooltip("Button sound")]
    [SerializeField] private AudioSource _buttonAudioSource = null;

    #region PUBLIC FIELDS

    public AudioSource CardAudioSource { get => _cardAudioSource; set => _cardAudioSource = value; }
    public AudioSource ButtonAudioSource { get => _buttonAudioSource; set => _buttonAudioSource = value; }

    #endregion
}
