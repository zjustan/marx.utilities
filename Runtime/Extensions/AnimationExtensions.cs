using UnityEngine;

namespace Marx.Utilities
{

    public static class AnimationExtensions
    {
        public static void Play(this Animation animation, AnimationClip clip, float speed = 1f)
        {
            if (!animation.AnimationHasClip(clip))
                animation.AddClip(clip, clip.name);

            animation[clip.name].speed = speed;
            animation.Play(clip.name);
        }

        private static bool AnimationHasClip(this Animation animation, AnimationClip clip)
        {
            foreach (AnimationState state in animation)
            {
                if (state.clip == clip)
                {
                    state.name = clip.name;
                    return true;
                }
            }
            return false;
        }
    }

}