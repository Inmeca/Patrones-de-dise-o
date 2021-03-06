using System;

namespace AbstractFactory
{
    // The Abstract Factory interface declares a set of methods that return
    // different abstract products. These products are called a family and are
    // related by a high-level theme or concept. Products of one family are
    // usually able to collaborate among themselves. A family of products may
    // have several variants, but the products of one variant are incompatible
    // with products of another.
    public interface IFurnitureFactory
    {
        IChair CreateChair();

        ISofa CreateSofa();
    }

    // Concrete Factories produce a family of products that belong to a single
    // variant. The factory guarantees that resulting products are compatible.
    // Note that signatures of the Concrete Factory's methods return an abstract
    // product, while inside the method a concrete product is instantiated.
    class VictorianFurnitureFactory : IFurnitureFactory
    {
        public IChair CreateChair()
        {
            return new VictorianChair();
        }

        public ISofa CreateSofa()
        {
            return new VictorianSofa();
        }
    }

    // Each Concrete Factory has a corresponding product variant.
    class ModernFurnitureFactory : IFurnitureFactory
    {
        public IChair CreateChair()
        {
            return new ModernChair();
        }

        public ISofa CreateSofa()
        {
            return new ModernSofa();
        }
    }

    // Each distinct product of a product family should have a base interface.
    // All variants of the product must implement this interface.
    public interface IChair 
    {
        string UsefulFunctionA();
    }

    // Concrete Products are created by corresponding Concrete Factories.
    class VictorianChair : IChair
    {
        public string UsefulFunctionA()
        {
            return "The result of the product Victorian Chair.";
        }
    }

    class ModernChair : IChair
    {
        public string UsefulFunctionA()
        {
            return "The result of the product Modern Chair";
        }
    }

    // Here's the the base interface of another product. All products can
    // interact with each other, but proper interaction is possible only between
    // products of the same concrete variant.
    public interface ISofa
    {
        // Product B is able to do its own thing...
        string UsefulFunctionB();

        // ...but it also can collaborate with the ProductA.
        //
        // The Abstract Factory makes sure that all products it creates are of
        // the same variant and thus, compatible.
        string AnotherUsefulFunctionB(IChair collaborator);
    }

    // Concrete Products are created by corresponding Concrete Factories.
    class VictorianSofa : ISofa
    {
        public string UsefulFunctionB()
        {
            return "The result of the product Victorian Sofa";
        }

        // The variant, Product B1, is only able to work correctly with the
        // variant, Product A1. Nevertheless, it accepts any instance of
        // AbstractProductA as an argument.
        public string AnotherUsefulFunctionB(IChair collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the Victorian Sofa collaborating with the ({result})";
        }
    }

    class ModernSofa : ISofa
    {
        public string UsefulFunctionB()
        {
            return "The result of the product Modern Sofa";
        }

        // The variant, Product B2, is only able to work correctly with the
        // variant, Product A2. Nevertheless, it accepts any instance of
        // AbstractProductA as an argument.
        public string AnotherUsefulFunctionB(IChair collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the Modern Sofa collaborating with the ({result})";
        }
    }

    // The client code works with factories and products only through abstract
    // types: AbstractFactory and AbstractProduct. This lets you pass any
    // factory or product subclass to the client code without breaking it.
    class Client
    {
        public void Main()
        {
            // The client code can work with any concrete factory class.
            Console.WriteLine("Client: Testing client code with the first factory type...");
            ClientMethod(new VictorianFurnitureFactory());
            Console.WriteLine();

            Console.WriteLine("Client: Testing the same client code with the second factory type...");
            ClientMethod(new ModernFurnitureFactory());
        }

        public void ClientMethod(IFurnitureFactory factory)
        {
            var chair = factory.CreateChair();
            var sofa = factory.CreateSofa();

            Console.WriteLine(sofa.UsefulFunctionB());
            Console.WriteLine(sofa.AnotherUsefulFunctionB(chair));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Client().Main();
        }
    }
}