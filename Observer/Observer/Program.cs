using System;
using System.Collections.Generic;
using System.Threading;

namespace Observer
{
    public interface ISuscriptor
    {
        // Receive update from subject
        void Update(ISujeto subject);
    }

    public interface ISujeto
    {
        // Attach an observer to the subject.
        void Attach(ISuscriptor observer);

        // Detach an observer from the subject.
        void Detach(ISuscriptor observer);

        // Notify all observers about an event.
        void Notify();
    }

    // The Subject owns some important state and notifies observers when the
    // state changes.
    public class Publicador : ISujeto
    {
        // For the sake of simplicity, the Subject's state, essential to all
        // subscribers, is stored in this variable.
        public int State { get; set; } = -0;

        // List of subscribers. In real life, the list of subscribers can be
        // stored more comprehensively (categorized by event type, etc.).
        private List<ISuscriptor> _observers = new List<ISuscriptor>();

        // The subscription management methods.
        public void Attach(ISuscriptor observer)
        {
            Console.WriteLine("Publicador: Attached an observer.");
            this._observers.Add(observer);
        }

        public void Detach(ISuscriptor observer)
        {
            this._observers.Remove(observer);
            Console.WriteLine("Publicador: Detached an observer.");
        }

        // Trigger an update in each subscriber.
        public void Notify()
        {
            Console.WriteLine("Publicador: Notifying observers...");

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

        // Usually, the subscription logic is only a fraction of what a Subject
        // can really do. Subjects commonly hold some important business logic,
        // that triggers a notification method whenever something important is
        // about to happen (or after it).
        public void SomeBusinessLogic()
        {
            Console.WriteLine("\nPublicador: I'm doing something important.");
            this.State = new Random().Next(0, 10);

            Thread.Sleep(15);

            Console.WriteLine("Publicador: My state has just changed to: " + this.State);
            this.Notify();
        }
    }

    // Concrete Observers react to the updates issued by the Subject they had
    // been attached to.
    class Persona1 : ISuscriptor
    {
        public void Update(ISujeto publicador)
        {
            if ((publicador as Publicador).State < 3)
            {
                Console.WriteLine("Persona1: Reacted to the event.");
            }
        }
    }

    class Persona2 : ISuscriptor
    {
        public void Update(ISujeto publicador)
        {
            if ((publicador as Publicador).State == 0 || (publicador as Publicador).State >= 2)
            {
                Console.WriteLine("Persona2: Reacted to the event.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code.
            var publicador = new Publicador();
            var observerA = new Persona1();
            publicador.Attach(observerA);

            var observerB = new Persona2();
            publicador.Attach(observerB);

            publicador.SomeBusinessLogic();
            publicador.SomeBusinessLogic();

            publicador.Detach(observerB);

            publicador.SomeBusinessLogic();
        }
    }
}