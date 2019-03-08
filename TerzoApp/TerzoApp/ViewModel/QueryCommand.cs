using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoApp.ViewModel
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/8 18:13:41													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoApp.ViewModel
{
    public class QueryCommand : ICommand
    {
        #region Field
        private Action _execute;
        private Func<bool> _canExecute;

        #endregion

        public QueryCommand(Action execute):this(execute,null)
        {
        }

        public QueryCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if(_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if(_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
