using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("带优先级参数的日志节点")]
    [TaskIcon("{SkinColor}LogIcon.png")]
    internal sealed class PriorityLog : Action
    {
        [Tooltip("Text to output to the log")] 
        public SharedString text;
        [Tooltip("Is this text an error?")] 
        public SharedBool logError;
        [Tooltip("Should the time be included in the log message?")]
        public SharedBool logTime;
        [Tooltip("优先级")] 
        public SharedFloat priority;

        public override void OnStart()
        {
        }

        public override TaskStatus OnUpdate()
        {
            // Log the text and return success
            if (logError.Value)
            {
                Debug.LogError(logTime.Value ? $"{Time.time}: {text}" : text);
            }
            else
            {
                Debug.Log(logTime.Value ? $"{Time.time}: {text}" : text);
            }

            return TaskStatus.Success;
        }

        /// <summary>
        /// 重写获取优先级的方法。
        /// </summary>
        /// <returns>获取到的优先级。</returns>
        public override float GetPriority()
        {
            return priority.Value;
        }

        public override void OnReset()
        {
            // Reset the properties back to their original values
            text = "";
            logError = false;
            logTime = false;
        }
    }
}