using System;


namespace VariableScope
{

    public class TrafficLight
    {

        private ITrafficLightState _state;
        public TrafficLight()
        {
            _state = new RedState();
        }
        public void ChangeLight()
        {
            _state = _state.Change();
        }


    }

    public interface ITrafficLightState
    {
        ITrafficLightState Change();

    }

    public class RedState : ITrafficLightState
    {
        public ITrafficLightState Change()
        {
            Console.WriteLine("Stop! Light is Red.");
            return new GreenState();
        }
    }
    public class GreenState : ITrafficLightState
    {
        public ITrafficLightState Change()
        {
            Console.WriteLine("Go! Light is Green.");
            return new YellowState();
        }
    }

    public class YellowState : ITrafficLightState
    {
        public ITrafficLightState Change()
        {
            Console.WriteLine("Slow down! Light is Yellow.");
            return new RedState();
        }
    }







    class Program
    {

        static void Main(string[] args)
        {
            TrafficLight trafficLight = new TrafficLight();
            for (int i = 0; i < 6; i++)
            {
                trafficLight.ChangeLight();
            }

        }

    }
}