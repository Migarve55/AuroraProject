﻿using System;
using System.Diagnostics;
using System.Windows.Input;

namespace SimulatorApp.Common;

public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Predicate<object>? _canExecute;

    /// <summary>
    /// Creates a new command.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    /// <param name="canExecute">The execution status logic.</param>
    public RelayCommand(Action<object> execute, Predicate<object>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;           
    }

    [DebuggerStepThrough]
    public bool CanExecute(object parameters)
    {
        return _canExecute?.Invoke(parameters) ?? true;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public void Execute(object parameters)
    {
        _execute(parameters);
    }

    public void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}