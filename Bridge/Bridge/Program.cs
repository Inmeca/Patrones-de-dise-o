using System;

namespace Bridge
{
    // The Abstraction defines the interface for the "control" part of the two
    // class hierarchies. It maintains a reference to an object of the
    // Implementation hierarchy and delegates all of the real work to this
    // object.
    class ControlRemoto
    {
        protected IDispositivos _implementation;

        public ControlRemoto(IDispositivos implementation)
        {
            this._implementation = implementation;
        }

        public virtual string Operation()
        {
            return "Control Remoto: Base operation with:\n" +
                _implementation.OperationImplementation();
        }
    }

    // You can extend the Abstraction without changing the Implementation
    // classes.
    class ControlRemotoAvanzado : ControlRemoto
    {
        public ControlRemotoAvanzado(IDispositivos implementation) : base(implementation)
        {
        }

        public override string Operation()
        {
            return "ControlRemotoAvanzado: Extended operation with:\n" +
                base._implementation.OperationImplementation();
        }
    }

    // The Implementation defines the interface for all implementation classes.
    // It doesn't have to match the Abstraction's interface. In fact, the two
    // interfaces can be entirely different. Typically the Implementation
    // interface provides only primitive operations, while the Abstraction
    // defines higher- level operations based on those primitives.
    public interface IDispositivos
    {
        string OperationImplementation();
    }

    // Each Concrete Implementation corresponds to a specific platform and
    // implements the Implementation interface using that platform's API.
    class Radio : IDispositivos
    {
        public string OperationImplementation()
        {
            return "Radio: The result in platform A.\n";
        }
    }

    class Television : IDispositivos
    {
        public string OperationImplementation()
        {
            return "Radio: The result in platform B.\n";
        }
    }

    class Client
    {
        // Except for the initialization phase, where an Abstraction object gets
        // linked with a specific Implementation object, the client code should
        // only depend on the Abstraction class. This way the client code can
        // support any abstraction-implementation combination.
        public void ClientCode(ControlRemoto abstraction)
        {
            Console.Write(abstraction.Operation());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            ControlRemoto abstraction;
            // The client code should be able to work with any pre-configured
            // abstraction-implementation combination.
            abstraction = new ControlRemoto(new Radio());
            client.ClientCode(abstraction);

            Console.WriteLine();

            abstraction = new ControlRemotoAvanzado(new Television());
            client.ClientCode(abstraction);
        }
    }
}