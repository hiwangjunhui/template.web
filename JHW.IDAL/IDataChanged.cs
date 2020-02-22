using System;
using System.Collections.Generic;

namespace JHW.IDAL
{
    /// <summary>
    /// 数据库发生改变
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataChanged<T> where T : class
    {
        /// <summary>
        /// 数据库发生改变时触发
        /// </summary>
        event EventHandler<DataChangedEventArgs<T>> OnDataChanged;
    }

    public class DataChangedEventArgs<T> : EventArgs
    {
        public DataChangedEventArgs(ChangedTypes changedType)
        {
            ChangedType = changedType;
        }

        /// <summary>
        /// 数据发生改变的方式
        /// </summary>
        public ChangedTypes ChangedType { get; private set; }

        /// <summary>
        /// 发生改变的数据
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }

    /// <summary>
    /// 数据发生改变的方式
    /// </summary>
    public enum ChangedTypes
    {
        /// <summary>
        /// 新增
        /// </summary>
        Inserted,

        /// <summary>
        /// 修改
        /// </summary>
        Updated,

        /// <summary>
        /// 删除
        /// </summary>
        Deleted,

        /// <summary>
        /// 未知
        /// </summary>
        Unknown
    }
}
