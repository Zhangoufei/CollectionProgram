using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace 多线程类型
{
    /// <summary>
    /// 不用每个地方都使用异步 inoke等
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ThreadSafeCollection<T> : ObservableCollection<T>
    {
        private static object __threadsafelock = new object();

        public ThreadSafeCollection()
        {
            BindingOperations.EnableCollectionSynchronization(this, __threadsafelock);
        }
        public ThreadSafeCollection(IEnumerable<T> list) : base(list)
        {
            BindingOperations.EnableCollectionSynchronization(this, __threadsafelock);
        }
    }

}
