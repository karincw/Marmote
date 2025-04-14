using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AnimeManager : MonoBehaviour
{
    public static AnimeManager Instance;
    
    private PlayableDirector director;
    private SignalReceiver receiver;


    private void Awake()
    {
        if(Instance != null) { Destroy(this);return; }
        Instance = this;

        director = GetComponent<PlayableDirector>();
        receiver = GetComponent<SignalReceiver>();
    }

    public void PlayAnime(PlayableAsset _asset, GameObject _obj)
    {
        director.playableAsset = _asset;
        TimelineAsset t = (TimelineAsset)_asset;

        foreach (var track in t.GetOutputTracks())
        {
            if (track is AnimationTrack)
            {
                director.SetGenericBinding(track, _obj.GetComponent<Animator>());
            }

            if (track is SignalTrack)
            {
                director.SetGenericBinding(track, _obj.GetComponent<SignalReceiver>());
            }
        }

        director.Play();
    }
}
