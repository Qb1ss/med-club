using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("PARAMETERS")]
    [Tooltip("Card sound")]
    [SerializeField] private AudioSource _cardAudioSource = null;
    [Space (height: 5f)]

    [Tooltip("Button sound")]
    [SerializeField] private AudioSource _buttonAudioSource = null;
    [Space(height: 5f)]

    [Tooltip("Correct sound")]
    [SerializeField] private AudioSource _correctAudioSource = null;
    [Tooltip("Correct sound")]
    [SerializeField] private AudioSource _incorrectAudioSource = null;

    #region PUBLIC FIELDS

    public AudioSource CardAudioSource { get => _cardAudioSource; set => _cardAudioSource = value; }
    public AudioSource ButtonAudioSource { get => _buttonAudioSource; set => _buttonAudioSource = value; }
    public AudioSource CorrectAudioSource { get => _correctAudioSource; set => _correctAudioSource = value; }
    public AudioSource IncorrectAudioSource { get => _incorrectAudioSource; set => _incorrectAudioSource = value; }

    #endregion
}
