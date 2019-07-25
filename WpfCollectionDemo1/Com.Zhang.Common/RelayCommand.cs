using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Com.Zhang.Common
{
    //命令绑定
    public class RelayCommand : ICommand
    {
        public Action ExecuteAction; //执行方法
        public Action<object> ExecuteCommand; //执行方法 带参数
        public Func<object, bool> CanExecuteCommand; //执行方法的条件
        public RelayCommand(Action action)// 执行事件
        {
            ExecuteAction = action;
        }
        //执行带参数的事件
        public RelayCommand(Action<object> action)
        {
            ExecuteCommand = action;
        }
        //根据条件执行带参数的事件
        public RelayCommand(Action<object> action, Func<object, bool> can)
        {
            ExecuteCommand = action;
            CanExecuteCommand = can;
        }
        //当命令可执行状态发生改变时，应被激活
        public event EventHandler CanExecuteChanged;

        //用于判断命令是否可以执行
        public bool CanExecute(object parameter)
        {
            if (ExecuteAction != null) return true;
            return CanExecuteCommand == null || CanExecuteCommand(parameter);
        }
        //命令执行
        public void Execute(object parameter)
        {
            if (ExecuteCommand != null) ExecuteCommand(parameter);
            else ExecuteAction();
        }
    }
}
