
using System;
using System.Collections.Generic;

namespace ChainResponsibility
{
    // The Handler interface declares a method for building the chain of
    // handlers. It also declares a method for executing a request.
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(object request);
    }

    // The default chaining behavior can be implemented inside a base handler
    // class.
    abstract class Animales : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;

            // Returning a handler from here will let us link handlers in a
            // convenient way like this:
            // monkey.SetNext(squirrel).SetNext(dog);
            return handler;
        }

        public virtual object Handle(object request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }

    class Mono : Animales
    {
        public override object Handle(object request)
        {
            if ((request as string) == "Platano")
            {
                return $"Mono: Yo comere el {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class Gato : Animales
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "Atun")
            {
                return $"Gato: Yo comere el {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class Perro : Animales
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "Pollo")
            {
                return $"Perro: Yo comere el pollo {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class Client
    {
        // The client code is usually suited to work with a single handler. In
        // most cases, it is not even aware that the handler is part of a chain.
        public static void ClientCode(Animales handler)
        {
            foreach (var comida in new List<string> { "Atun", "Platano", "Manzana" })
            {
                Console.WriteLine($"Cliente: Quien quiere un {comida}?");

                var result = handler.Handle(comida);

                if (result != null)
                {
                    Console.Write($"   {result}");
                }
                else
                {
                    Console.WriteLine($"   {comida} Nadie la quiso");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The other part of the client code constructs the actual chain.
            var mono = new Mono();
            var gato = new Gato();
            var perro = new Perro();

            mono.SetNext(gato).SetNext(perro);

            // The client should be able to send a request to any handler, not
            // just the first one in the chain.
            Console.WriteLine("Chain: Mono > Gato > Perro\n");
            Client.ClientCode(mono);
            Console.WriteLine();

            Console.WriteLine("Subchain: Gato > Perro\n");
            Client.ClientCode(gato);
        }
    }
}