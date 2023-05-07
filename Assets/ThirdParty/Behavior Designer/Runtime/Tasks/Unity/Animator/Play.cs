using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
    [TaskCategory("Unity/Animator")]
    [TaskDescription("Plays an animator state. Returns Success.")]
    public class Play : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The name of the state")]
        public SharedString stateName;
        [Tooltip("The layer where the state is")]
        public int layer = -1;
        [Tooltip("The normalized time at which the state will play")]
        public float normalizedTime = float.NegativeInfinity;

        [Header("等待结束")] public bool waitEnd;
        
        private Animator animator;
        private GameObject prevGameObject;
        private float _endTime;
        private bool _isPlaying;
        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                animator = currentGameObject.GetComponent<Animator>();
                prevGameObject = currentGameObject;
            }

            if (animator != null && waitEnd)
            {
                _endTime = Time.time + GetClipLength(animator, stateName.Value);
            }

            _isPlaying = false;
        }

        public override TaskStatus OnUpdate()
        {
            if (animator == null) {
                Debug.LogWarning("Animator is null");
                return TaskStatus.Failure;
            }

            if (!_isPlaying)
            {
                animator.Play(stateName.Value, layer, normalizedTime);
                _isPlaying = true;
            }
    
            if (waitEnd && Time.time < _endTime)
            {
                return TaskStatus.Running;
            }
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            stateName = "";
            layer = -1;
            normalizedTime = float.NegativeInfinity;
            _endTime = 0;
        }
        
        
        /// <summary>
        /// 获取Clip播放时长
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="clipName"></param>
        /// <returns></returns>
        public static float GetClipLength(Animator animator, string clipName)
        {
            if (null == animator ||
                string.IsNullOrEmpty(clipName) ||
                null == animator.runtimeAnimatorController)
            {
                return 0;
            }

            // 获取所有的clips	
            var clips = animator.runtimeAnimatorController.animationClips;
            if (clips is not { Length: > 0 })
            {
                return 0;
            }

            AnimationClip clip;
            for (int i = 0, len = clips.Length; i < len; ++i)
            {
                clip = clips[i];
                if (null != clip && clip.name == clipName)
                {
                    return clip.length;
                }
            }

            return 0f;
        }
    }
}
