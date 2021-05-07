using System;
using System.Collections.Generic;

namespace Strategy
{
    // The Context defines the interface of interest to clients.
    class Context
    {
        // The Context maintains a reference to one of the Strategy objects. The
        // Context does not know the concrete class of a strategy. It should
        // work with all strategies via the Strategy interface.
        private IEstrategia _estrategia;

        public Context()
        { }

        // Usually, the Context accepts a strategy through the constructor, but
        // also provides a setter to change it at runtime.
        public Context(IEstrategia estrategia)
        {
            this._estrategia = estrategia;
        }

        // Usually, the Context allows replacing a Strategy object at runtime.
        public void SetStrategy(IEstrategia estrategia)
        {
            this._estrategia = estrategia;
        }

        // The Context delegates some work to the Strategy object instead of
        // implementing multiple versions of the algorithm on its own.
        public void DoSomeBusinessLogic()
        {
            Console.WriteLine("Context: Usando la estrategia: ");
            var result = this._estrategia.DoAlgorithm(8,2);
            Console.WriteLine(result);
        }
    }

    // The Strategy interface declares operations common to all supported
    // versions of some algorithm.
    //
    // The Context uses this interface to call the algorithm defined by Concrete
    // Strategies.
    public interface IEstrategia
    {
        //object DoAlgorithm(object data);
        int DoAlgorithm(int num1, int num2);
    }

    // Concrete Strategies implement the algorithm while following the base
    // Strategy interface. The interface makes them interchangeable in the
    // Context.
    class EstrategiaSuma : IEstrategia
    {
        public int DoAlgorithm(int num1, int num2)
        {
            return num1 + num2;
        }
    }

    class EstrategiaResta : IEstrategia
    {
        public int DoAlgorithm(int num1, int num2)
        {
            return num1 - num2;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code picks a concrete strategy and passes it to the
            // context. The client should be aware of the differences between
            // strategies in order to make the right choice.
            var context = new Context();

            Console.WriteLine("Cliente: Estrategia suma");
            context.SetStrategy(new EstrategiaSuma());
            context.DoSomeBusinessLogic();

            Console.WriteLine();

            Console.WriteLine("Cliente: Estrategia resta");
            context.SetStrategy(new EstrategiaResta());
            context.DoSomeBusinessLogic();
        }
    }
}