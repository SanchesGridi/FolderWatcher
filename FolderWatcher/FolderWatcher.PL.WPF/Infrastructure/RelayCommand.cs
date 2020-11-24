using System;
using System.Windows.Input;

namespace FolderWatcher.PL.WPF.Infrastructure
{
    public class RelayCommand : ICommand
    {
        private event EventHandler _myEvent;

        public event EventHandler MyEvent
        {
            add { _myEvent += value; ; }
            remove { _myEvent -= value;  }
        }



        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }
        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private Action<T> _execute;
        private Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute) : this(execute, null) { }
        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            T value = (T)Convert.ChangeType(parameter, typeof(T));
            _execute.Invoke(value);
        }
        public bool CanExecute(object parameter)
        {
            T value = (T)Convert.ChangeType(parameter, typeof(T));
            return _canExecute?.Invoke(value) ?? true;
        }
    }
}