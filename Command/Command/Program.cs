using System;

namespace Command
{
    // The Command interface declares a method for executing a command.
    public interface IComando
    {
        void Execute();
    }

    // Some commands can implement simple operations on their own.
    class CopiarComando : IComando
    {
        private string _payload = string.Empty;

        public CopiarComando(string payload)
        {
            this._payload = payload;
        }

        public void Execute()
        {
            Console.WriteLine($"CopiarComando: Yo puedo copiar ({this._payload})");
        }
    }

    // However, some commands can delegate more complex operations to other
    // objects, called "receivers."
    class PegarComando : IComando
    {
        private Editor _editor;

        // Context data, required for launching the receiver's methods.
        private string _a;

        private string _b;

        // Complex commands can accept one or several receiver objects along
        // with any context data via the constructor.
        public PegarComando(Editor editor, string a, string b)
        {
            this._editor = editor;
            this._a = a;
            this._b = b;
        }

        // Commands can delegate to any methods of a receiver.
        public void Execute()
        {
            Console.WriteLine("PegarComando: Las cosas complejas deben ser realizadas por un receptor.");
            this._editor.DoSomething(this._a);
            this._editor.DoSomethingElse(this._b);
        }
    }

    // The Receiver classes contain some important business logic. They know how
    // to perform all kinds of operations, associated with carrying out a
    // request. In fact, any class may serve as a Receiver.
    class Editor
    {
        public void DoSomething(string a)
        {
            Console.WriteLine($"Editor: Trabajando en ({a}.)");
        }

        public void DoSomethingElse(string b)
        {
            Console.WriteLine($"Editor: Tambien trabajando en ({b}.)");
        }
    }

    // The Invoker is associated with one or several commands. It sends a
    // request to the command.
    class Invoker
    {
        private IComando _onStart;

        private IComando _onFinish;

        // Initialize commands.
        public void SetOnStart(IComando command)
        {
            this._onStart = command;
        }

        public void SetOnFinish(IComando command)
        {
            this._onFinish = command;
        }

        // The Invoker does not depend on concrete command or receiver classes.
        // The Invoker passes a request to a receiver indirectly, by executing a
        // command.
        public void DoSomethingImportant()
        {
            Console.WriteLine("Invoker: ¿Quieren que se haga algo antes de que empiece?");
            if (this._onStart is IComando)
            {
                this._onStart.Execute();
            }

            Console.WriteLine("Invoker:  Hacer algo importante");

            Console.WriteLine("Invoker: ¿Quieren que se haga algo después de que termine?");
            if (this._onFinish is IComando)
            {
                this._onFinish.Execute();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code can parameterize an invoker with any commands.
            Invoker invoker = new Invoker();
            invoker.SetOnStart(new CopiarComando("Copiando ..."));
            Editor editor = new Editor();
            invoker.SetOnFinish(new PegarComando(editor, "Copiar texto", "Pegar texto"));

            invoker.DoSomethingImportant();
        }
    }
}