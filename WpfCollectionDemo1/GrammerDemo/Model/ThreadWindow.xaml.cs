using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GrammerDemo.Model
{
    /// <summary>
    /// ThreadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ThreadWindow : Window
    {
        public ThreadWindow()
        {
            InitializeComponent();
        }
        Task task;
        /// task使用
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //任务1
            task = new Task(run);
            Console.WriteLine("任务标识" + task.GetHashCode() + "状态" + task.Status + "当前线程状态:" + Thread.CurrentThread.GetHashCode());
            task.Start();
            Action<Task> action = new Action<Task>(taskStart);
            task.ContinueWith(action);

        }

        public void taskStart(Task task)
        {

            task = new Task(run);
            task.Start();
            Console.WriteLine("任务标识" + task.GetHashCode() + "状态" + task.Status + "当前线程状态:" + Thread.CurrentThread.GetHashCode());
        }

        public void run()
        {
            Console.WriteLine("this  is run ");
        }

       


        //定义集合大小为51个，也可以不定义大小
        static BlockingCollection<int> blocking = new BlockingCollection<int>(51);
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            blocking = new BlockingCollection<int>();
            Console.WriteLine("当前blocking为：" + blocking.IsCompleted + "设置了集合大小count一样是0，blocking.Count:" + blocking.Count());
            //当前线程标识
            Console.WriteLine(Thread.CurrentThread.GetHashCode());
            for (int i = 0; i < 3; i++)
            {
                ////如果添加到第3个，就设置添加完成，这时在添加就会抛异常
                //if (i == 3)
                //{
                //    blocking.CompleteAdding();
                //}

                Action<object> action = new Action<object>(run);
                Task task = new Task(action, i);
                task.RunSynchronously();
            }

            Console.WriteLine("设置添加完成前：" + blocking.IsAddingCompleted);
            //设置添加完成后
            blocking.CompleteAdding();
            Console.WriteLine("设置添加完成后：" + blocking.IsAddingCompleted);
            #region 同步取 取3个
            //for (int i = 0; i < 3; i++)
            //{
            //    Action actionTake = new Action(take);
            //    actionTake();
            //}
            #endregion
            //并发读取

            #region 并发步取 取3个
            //blocking.IsCompleted 只有当集合被添加进内容，然后又都被取光了以后，他才会等于ture，否则都是false
            //当IsCompleted为ture时，就不能再取了否则会抛异常

            //同时取，结果是 
            //blocking:0
            //blocking:2
            //blocking:1
            if (!blocking.IsCompleted)//如果集合没取光
            {
                Action actionTake2 = new Action(take);
                Parallel.Invoke(actionTake2, actionTake2, actionTake2);
            }
            #endregion

            Console.WriteLine("当前blocking为：" + blocking.IsCompleted + ",blocking数量为：" + blocking.Count());
            //数据被取光了以后， blocking.Count()为0
        }
        public static void take()
        {
            //同步取，blocking.Count()会真实的表现，而异步取，Count是不准确的，因为我取count的时候，可能集合已经又被取出数据了，测试10次肯定会出现不真实的情况
            Console.WriteLine("blocking:" + blocking.Take() + ",blocking数量为：" + blocking.Count());
        }
        public static void run(object i)
        {
            int currentI = int.Parse(i.ToString());
            blocking.TryAdd(currentI);
        }
    }
}
