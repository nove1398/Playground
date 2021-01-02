using System;
using System.Collections.Generic;

namespace Playground
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.ReadLine();
        }
    }

    public interface IObserver<T>
    {
        public void Update();
    }

    public class SomeEvent
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public SomeEvent(string description, DateTime date)
        {
            Description = description;
            Date = date;
        }
    }

    public class NotificationProvider : IObserver<SomeEvent>
    {
        // Maintain a list of observers
        private List<IObserver<SomeEvent>> _observers;

        public NotificationProvider()
        {
            _observers = new List<IObserver<SomeEvent>>();
        }

        // Define Unsubscriber class
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<SomeEvent>> _observers;
            private IObserver<SomeEvent> _observer;

            public Unsubscriber(List<IObserver<SomeEvent>> observers,
                                IObserver<SomeEvent> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);
            }
        }

        // Define Subscribe method
        public IDisposable Subscribe(IObserver<SomeEvent> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        // Notify observers when event occurs
        public void NotificationEvent(string description)
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}