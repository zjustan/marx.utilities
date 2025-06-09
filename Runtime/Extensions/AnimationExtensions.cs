using UnityEngine;

namespace Marx.Utilities
{

    public static class AnimationExtensions
    {
        /// <summary>
        /// Plays an animation clip on the specified animation component with an optional speed adjustment.
        /// </summary>
        /// <param name="animation">The animation component on which the clip will be played.</param>
        /// <param name="clip">The animation clip to play.</param>
        /// <param name="speed">The speed at which the animation clip will play. Defaults to 1f.</param>
        public static void Play(this Animation animation, AnimationClip clip, float speed = 1f) {
            if (!animation.AnimationHasClip(clip)) animation.AddClip(clip, clip.name);

            animation[clip.name].speed = speed;
            animation.Play(clip.name);
        }

        /// <summary>
        /// Determines whether the specified animation contains the given animation clip.
        /// </summary>
        /// <param name="animation">The animation component to check for the clip.</param>
        /// <param name="clip">The animation clip to look for in the animation component.</param>
        /// <returns>True if the animation contains the specified clip; otherwise, false.</returns>
        private static bool AnimationHasClip(this Animation animation, AnimationClip clip) {
            foreach (AnimationState state in animation) {
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