using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Simulator.System;
using SimulatorApp.Common;

namespace SimulatorApp.ViewModels;

public class MainViewModel : ViewModel
{
    private readonly Computer _computer;
    private readonly DispatcherTimer _timer;
    
    private bool _running;
    private long _cycle;

    public MainViewModel()
    {
        _computer = new Computer();
        PlayCommand = new RelayCommand(p => ToggleRunMode());
        StepCommand = new RelayCommand(p => DoStep(), p => !_running);
        ResetCommand = new RelayCommand(p => ResetSimulator());
        // Setup timer
        _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(100), DispatcherPriority.Normal, (sender, args) =>
        {
            DoStep();
        }, Application.Current.Dispatcher);
        var a =_timer.IsEnabled;
        _timer.Stop();
    }
    
    public bool Running
    {
        get => _running;
        private set => UpdateProperty(ref _running, value);
    }

    public long Cycle
    {
        get => _cycle;
        private set => UpdateProperty(ref _cycle, value);
    }

    public ICommand PlayCommand { get; }
    public ICommand StepCommand { get; }
    public ICommand ResetCommand { get; }

    private void ToggleRunMode()
    {
        if (Running)
        {
            StopSimulator();
        }
        else
        {
            RunSimulator();
        }
    }

    private void RunSimulator()
    {
        Running = true;
        _timer.Start();
    }
    
    private void StopSimulator()
    {
        Running = false;
        _timer.Stop();
    }
    
    private void ResetSimulator()
    {
        StopSimulator();
        _computer.Reset();
        Update();
    }

    private void DoStep()
    {
        if (_computer.DoClockCycle())
        {
            StopSimulator();
        }

        Update();
    }

    private void Update()
    {
        var snapshot = _computer.TakeStateSnapshot();
        Cycle = snapshot.ClockCycle;
    }
}