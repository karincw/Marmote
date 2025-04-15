using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Shy
{
    public class AnimeCompo : MonoBehaviour
    {
        private PlayableDirector director;
        private TimelineAsset tAsset;
        private SignalReceiver receiver;

        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
            tAsset = (TimelineAsset)director.playableAsset;
            receiver = GetComponent<SignalReceiver>();
        }

        public void PlayAnime(AnimationClip _asset)
        {
            foreach (var track in tAsset.GetOutputTracks())
            {
                if (track is AnimationTrack animeTrack)
                {
                    foreach (var clip in animeTrack.GetClips())
                    {
                        AnimationPlayableAsset newAnimeAsset = ScriptableObject.CreateInstance<AnimationPlayableAsset>();
                        newAnimeAsset.clip = _asset;
                        
                        clip.asset = newAnimeAsset;
                        break;
                    }
                }
            }

            director.Play();
        }
    }
}
