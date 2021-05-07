
using System;

namespace Mediator
{
    // The Mediator interface declares a method used by components to notify the
    // mediator about various events. The Mediator may react to these events and
    // pass the execution to other components.
    public interface IMediator
    {
        void Notify(object sender, string ev);
    }

    // Concrete Mediators implement cooperative behavior by coordinating several
    // components.
    class Autenticacion : IMediator
    {
        private Input _input;

        private TextBox _textBox;

        public Autenticacion(Input input, TextBox textBox)
        {
            this._input = input;
            this._input.SetMediator(this);
            this._textBox = textBox;
            this._textBox.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {
            if (ev == "A")
            {
                Console.WriteLine("Mediator reacts on A and triggers folowing operations:");
                this._textBox.DoC();
            }
            if (ev == "D")
            {
                Console.WriteLine("Mediator reacts on D and triggers following operations:");
                this._input.DoB();
                this._textBox.DoC();
            }
        }
    }

    // The Base Component provides the basic functionality of storing a
    // mediator's instance inside component objects.
    class Componente
    {
        protected IMediator _mediator;

        public Componente(IMediator mediator = null)
        {
            this._mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }

    // Concrete Components implement various functionality. They don't depend on
    // other components. They also don't depend on any concrete mediator
    // classes.
    class Input : Componente
    {
        public void DoA()
        {
            Console.WriteLine("Component input does A.");

            this._mediator.Notify(this, "A");
        }

        public void DoB()
        {
            Console.WriteLine("Component input does B.");

            this._mediator.Notify(this, "B");
        }
    }

    class TextBox : Componente
    {
        public void DoC()
        {
            Console.WriteLine("Component textBox does C.");

            this._mediator.Notify(this, "C");
        }

        public void DoD()
        {
            Console.WriteLine("Component textBox does D.");

            this._mediator.Notify(this, "D");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code.
            Input input = new Input();
            TextBox textBox = new TextBox();
            new Autenticacion(input, textBox);

            Console.WriteLine("Client triggets operation A.");
            input.DoA();

            Console.WriteLine();

            Console.WriteLine("Client triggers operation D.");
            textBox.DoD();
        }
    }
}