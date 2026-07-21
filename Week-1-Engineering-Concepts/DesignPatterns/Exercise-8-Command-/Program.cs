using System;

namespace CommandPatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Receiver
            Light light = new Light();

            // Commands
            ICommand lightOn = new LightOnCommand(light);
            ICommand lightOff = new LightOffCommand(light);

            // Invoker
            RemoteControl remote = new RemoteControl();

            remote.SetCommand(lightOn);
            remote.PressButton();

            remote.SetCommand(lightOff);
            remote.PressButton();
        }
    }
}
