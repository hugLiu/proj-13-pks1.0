using System;
using System.Collections.Generic;
using PKS.Utils;

namespace PKS.Models
{
    /// <summary>元数据集合</summary>
    [Serializable]
    public class UserBehaviorCollection : List<UserBehavior>
    {
        /// <summary>构造函数</summary>
        public UserBehaviorCollection()
        {
        }
        /// <summary>构造函数</summary>
        public UserBehaviorCollection(IEnumerable<UserBehavior> values) : base(values)
        {
        }
        /// <summary>生成JSON串</summary>
        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
