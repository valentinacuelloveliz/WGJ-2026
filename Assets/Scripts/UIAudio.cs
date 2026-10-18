using UnityEngine;

public class UIAudio : MonoBehaviour
{
    public static UIAudio Instance { get; private set; }

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip playClip;

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayHover() => Play(hoverClip);
    public void PlayClick() => Play(clickClip);
    public void PlayPlay() => Play(playClip);

    void Play(AudioClip clip)
    {
        if (clip != null && source != null)
        {
            source.PlayOneShot(clip);
        }
    }
}