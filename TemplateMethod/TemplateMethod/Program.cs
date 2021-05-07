using System;

namespace TemplateMethod
{
    // The Abstract Class defines a template method that contains a skeleton of
    // some algorithm, composed of calls to (usually) abstract primitive
    // operations.
    //
    // Concrete subclasses should implement these operations, but leave the
    // template method itself intact.
    abstract class JuegosInteligenciaArtificial
    {
        // The template method defines the skeleton of an algorithm.
        public void TemplateMethod()
        {
            this.takeTurn();
            this.collectResources();
            this.buildStructures();
            this.buildUnits();
            this.attack();
            this.BaseOperation3();
            this.Hook2();
        }

        // These operations already have implementations.
        protected void takeTurn()
        {
            Console.WriteLine("JuegosInteligenciaArtificial says: I am doing the bulk of the work");
        }

        protected void buildStructures()
        {
            Console.WriteLine("JuegosInteligenciaArtificial says: But I let subclasses override some operations");
        }

        protected void BaseOperation3()
        {
            Console.WriteLine("JuegosInteligenciaArtificial says: But I am doing the bulk of the work anyway");
        }

        // These operations have to be implemented in subclasses.
        protected abstract void collectResources();

        protected abstract void attack();

        // These are "hooks." Subclasses may override them, but it's not
        // mandatory since the hooks already have default (but empty)
        // implementation. Hooks provide additional extension points in some
        // crucial places of the algorithm.
        protected virtual void buildUnits() { }

        protected virtual void Hook2() { }
    }

    // Concrete classes have to implement all abstract operations of the base
    // class. They can also override some operations with a default
    // implementation.
    class OrcsInteligenciaArtificial : JuegosInteligenciaArtificial
    {
        protected override void collectResources()
        {
            Console.WriteLine("OrcsInteligenciaArtificial says: Implemented collectResources");
        }

        protected override void attack()
        {
            Console.WriteLine("OrcsInteligenciaArtificial says: Implemented attack");
        }
    }

    // Usually, concrete classes override only a fraction of base class'
    // operations.
    class MonstersInteligenciaArtificial : JuegosInteligenciaArtificial
    {
        protected override void collectResources()
        {
            Console.WriteLine("MonstersInteligenciaArtificial says: Implemented collectResources");
        }

        protected override void attack()
        {
            Console.WriteLine("MonstersInteligenciaArtificial says: Implemented attack");
        }

        protected override void buildUnits()
        {
            Console.WriteLine("MonstersInteligenciaArtificial says: Overridden buildUnits");
        }
    }

    class Client
    {
        // The client code calls the template method to execute the algorithm.
        // Client code does not have to know the concrete class of an object it
        // works with, as long as it works with objects through the interface of
        // their base class.
        public static void ClientCode(JuegosInteligenciaArtificial juegosInteligenciaArtificial)
        {
            // ...
            juegosInteligenciaArtificial.TemplateMethod();
            // ...
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Same client code can work with different subclasses:");

            Client.ClientCode(new OrcsInteligenciaArtificial());

            Console.Write("\n");

            Console.WriteLine("Same client code can work with different subclasses:");
            Client.ClientCode(new MonstersInteligenciaArtificial());
        }
    }
}