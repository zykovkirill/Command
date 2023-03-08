
Console.WriteLine("Пример паттерна 'Команада'" );

App app = new();

SmartKettle smartKittle = new();
SmartKittleOnCommand command  = new(smartKittle);
app.SetCommand(command);
app.ClickButton();
app.UndoButton();

SmartKittleSetTemperatureCommand commandTemp = new(smartKittle, 79);
app.SetCommand(commandTemp);
app.ClickButton();
app.UndoButton();

SmartHumidifier smartHumidifier = new();
SmartHumidifierCommand humidifierCommand = new(smartHumidifier);
app.SetCommand(humidifierCommand);
app.ClickButton();
app.UndoButton();

Console.Read();




file interface ICommand
{
    void Execute();
    void Undo();
}

#region Получатели команд
file class SmartKettle
{
    const int DefaultTemp = 100;
    public void On()
    {
        Console.WriteLine("Умный чайник включён");

    }

    public void Off()
    {
        Console.WriteLine("Умный чайник выключен");

    }

    public void SetTemperature(int temp)
    {

        Console.WriteLine($"Установлена температура - {temp}");
    }

    public void ResetTemperature()
    {
        Console.WriteLine($"Установлена температура - {DefaultTemp}");
    }

}

file class SmartHumidifier
{

    public void StartHumidification()
    {
        Console.WriteLine("Увлажняем воздух");
    }

    public void StopHumidification()
    {
        Console.WriteLine("Перестаём увлажнять воздух");
    }
}

#endregion

#region Команды
file class SmartKittleOnCommand : ICommand
{
    private readonly SmartKettle _kettle;

    public SmartKittleOnCommand(SmartKettle kettle)
    {
       _kettle = kettle;
    }
    public void Execute()
    {
      _kettle.On();
    }

    public void Undo()
    {
        _kettle.Off();
    }
}
file class SmartKittleSetTemperatureCommand : ICommand
{
    private readonly SmartKettle _kettle;
    private readonly int _temperature;
    public SmartKittleSetTemperatureCommand(SmartKettle kettle, int temperature)
    {
        _kettle = kettle;
        _temperature = temperature;
    }
    public void Execute()
    {
        _kettle.SetTemperature(_temperature);
    }

    public void Undo()
    {
        _kettle.ResetTemperature();
    }
}

file class SmartHumidifierCommand : ICommand
{
    private readonly SmartHumidifier _smartHumidifier;

    public SmartHumidifierCommand(SmartHumidifier smartHumidifier)
    {
        _smartHumidifier = smartHumidifier;
    }
    public void Execute()
    {
        _smartHumidifier.StartHumidification();
    }

    public void Undo()
    {
        _smartHumidifier.StopHumidification();
    }
}
#endregion
file class App
{
    private ICommand? _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void ClickButton()
    {
        _command?.Execute();
    }
    public void UndoButton()
    {
        _command?.Undo();
    }

}