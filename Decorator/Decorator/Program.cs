using System;

namespace Decorator
{
    // The base Component interface defines operations that can be altered by
    // decorators.
    public abstract class DataSource
    {
        public abstract string Operation();
    }

    // Concrete Components provide default implementations of the operations.
    // There might be several variations of these classes.
    class FileDataSource : DataSource
    {
        public override string Operation()
        {
            return "FileDataSource";
        }
    }

    // The base Decorator class follows the same interface as the other
    // components. The primary purpose of this class is to define the wrapping
    // interface for all concrete decorators. The default implementation of the
    // wrapping code might include a field for storing a wrapped component and
    // the means to initialize it.
    abstract class DataSourceDecorator : DataSource
    {
        protected DataSource _dataSource;

        public DataSourceDecorator(DataSource dataSource)
        {
            this._dataSource = dataSource;
        }

        public void SetComponent(DataSource dataSource)
        {
            this._dataSource = dataSource;
        }

        // The Decorator delegates all work to the wrapped component.
        public override string Operation()
        {
            if (this._dataSource != null)
            {
                return this._dataSource.Operation();
            }
            else
            {
                return string.Empty;
            }
        }
    }

    // Concrete Decorators call the wrapped object and alter its result in some
    // way.
    class EncryptionDecorator : DataSourceDecorator
    {
        public EncryptionDecorator(DataSource comp) : base(comp)
        {
        }

        // Decorators may call parent implementation of the operation, instead
        // of calling the wrapped object directly. This approach simplifies
        // extension of decorator classes.
        public override string Operation()
        {
            return $"EncryptionDecorator({base.Operation()})";
        }
    }

    // Decorators can execute their behavior either before or after the call to
    // a wrapped object.
    class CompressionDecorator : DataSourceDecorator
    {
        public CompressionDecorator(DataSource comp) : base(comp)
        {
        }

        public override string Operation()
        {
            return $"CompressionDecorator({base.Operation()})";
        }
    }

    public class Client
    {
        // The client code works with all objects using the Component interface.
        // This way it can stay independent of the concrete classes of
        // components it works with.
        public void ClientCode(DataSource dataSource)
        {
            Console.WriteLine("RESULT: " + dataSource.Operation());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            var simple = new FileDataSource();
            Console.WriteLine("Client: I get a simple dataSource:");
            client.ClientCode(simple);
            Console.WriteLine();

            // ...as well as decorated ones.
            //
            // Note how decorators can wrap not only simple components but the
            // other decorators as well.
            EncryptionDecorator decorator1 = new EncryptionDecorator(simple);
            CompressionDecorator decorator2 = new CompressionDecorator(decorator1);
            Console.WriteLine("Client: Now I've got a decorated DataSource:");
            client.ClientCode(decorator2);
        }
    }
}