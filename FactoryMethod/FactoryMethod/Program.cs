using System;

namespace FactoryMethod
{
    // The Creator class declares the factory method that is supposed to return
    // an object of a Product class. The Creator's subclasses usually provide
    // the implementation of this method.
    abstract class Logistics
    {
        // Note that the Creator may also provide some default implementation of
        // the factory method.
        public abstract ITransport FactoryMethod();

        // Also note that, despite its name, the Creator's primary
        // responsibility is not creating products. Usually, it contains some
        // core business logic that relies on Product objects, returned by the
        // factory method. Subclasses can indirectly change that business logic
        // by overriding the factory method and returning a different type of
        // product from it.
        public string SomeOperation()
        {
            // Call the factory method to create a Product object.
            var product = FactoryMethod();
            // Now, use the product.
            var result = "logistics: The same logistics's code has just worked with "
                + product.Operation();

            return result;
        }
    }

    // Concrete Creators override the factory method in order to change the
    // resulting product's type.
    class RoadLogistics : Logistics
    {
        // Note that the signature of the method still uses the abstract product
        // type, even though the concrete product is actually returned from the
        // method. This way the Creator can stay independent of concrete product
        // classes.
        public override ITransport FactoryMethod()
        {
            return new Truck();
        }
    }

    class SeaLogistics : Logistics
    {
        public override ITransport FactoryMethod()
        {
            return new Ship();
        }
    }

    // The Product interface declares the operations that all concrete products
    // must implement.
    public interface ITransport
    {
        string Operation();
    }

    // Concrete Products provide various implementations of the Product
    // interface.
    class Truck : ITransport
    {
        public string Operation()
        {
            return "{Result of Truck}";
        }
    }

    class Ship : ITransport
    {
        public string Operation()
        {
            return "{Result of Ship}";
        }
    }

    class Client
    {
        public void Main()
        {
            Console.WriteLine("App: Launched with the RoadLogistics.");
            ClientCode(new RoadLogistics());

            Console.WriteLine("");

            Console.WriteLine("App: Launched with the SeaLogistics.");
            ClientCode(new SeaLogistics());
        }

        // The client code works with an instance of a concrete creator, albeit
        // through its base interface. As long as the client keeps working with
        // the creator via the base interface, you can pass it any creator's
        // subclass.
        public void ClientCode(Logistics logistics)
        {
            // ...
            Console.WriteLine("Client: I'm not aware of the logistics's class," +
                "but it still works.\n" + logistics.SomeOperation());
            // ...
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