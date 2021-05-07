using System;

namespace Adapter
{
    // The Target defines the domain-specific interface used by the client code.
    public interface ClaseCentralXML
    {
        string GetRequest();
    }

    // The Adaptee contains some useful behavior, but its interface is
    // incompatible with the existing client code. The Adaptee needs some
    // adaptation before the client code can use it.
    class BibliotecaAnalisisJSON
    {
        public string GetSpecificRequest()
        {
            return "Specific request.";
        }
    }

    // The Adapter makes the Adaptee's interface compatible with the Target's
    // interface.
    class Adapter : ClaseCentralXML
    {
        private readonly BibliotecaAnalisisJSON _json;

        public Adapter(BibliotecaAnalisisJSON json)
        {
            this._json = json;
        }

        public string GetRequest()
        {
            return $"This is '{this._json.GetSpecificRequest()}'";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BibliotecaAnalisisJSON json = new BibliotecaAnalisisJSON();
            ClaseCentralXML target = new Adapter(json);

            Console.WriteLine("BibliotecaAnalisisJSON es incompatible");
            Console.WriteLine("Pero con el adapter puede llamar este metodo");

            Console.WriteLine(target.GetRequest());
        }
    }
}