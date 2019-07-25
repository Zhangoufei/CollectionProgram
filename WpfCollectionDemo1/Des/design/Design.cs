using System;

namespace Des.design
{

    /// <summary>
    /// 代理接口
    /// </summary>
    public interface IUserProcessor
    {
        void Send(string str);
    }


    /// <summary>
    /// 设计模式-代理模式  真实对象
    /// </summary>
    public class Design : IUserProcessor
    {
        public void Send(string str)
        {
            Console.WriteLine(str);
        }
    }

    public class DesignPro : IUserProcessor
    {
        IUserProcessor design;
        public DesignPro(IUserProcessor userProcessor)
        {
            design = userProcessor;
        }

        private void SendPre()
        {

            Console.WriteLine("开始执行前");
        }

        private void SendPost()
        {
            Console.WriteLine("开始执行后");
        }
        public void Send(string str)
        {
            SendPre();
            design.Send(str);
            SendPost();
        }
    }

}
