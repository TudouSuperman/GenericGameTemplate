using System;
using BehaviorDesigner.Runtime;

namespace BehaviorTree.Variables
{
    [Serializable]
    internal sealed class RoleInfoBlog: SharedVariable<RoleInfoBlog>
    {
        public static implicit operator RoleInfoBlog(RoleInfo value)
        {
            return new RoleInfoBlog { mValue = value };
        }
    }
}