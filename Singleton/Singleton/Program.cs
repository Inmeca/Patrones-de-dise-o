using System;

namespace Singleton
{
    // The Singleton class defines the `GetInstance` method that serves as an
    // alternative to constructor and lets clients access the same instance of
    // this class over and over.
    class Presidente
    {
        public string presidente;
        // The Singleton's constructor should always be private to prevent
        // direct construction calls with the `new` operator.
        private Presidente(string presidente) {
            this.presidente = presidente;
        }

        // The Singleton's instance is stored in a static field. There there are
        // multiple ways to initialize this field, all of them have various pros
        // and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        private static Presidente _instance;

        // This is the static method that controls the access to the singleton
        // instance. On the first run, it creates a singleton object and places
        // it into the static field. On subsequent runs, it returns the client
        // existing object stored in the static field.
        public static Presidente GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Presidente("Andres Manuel Lopez Obrador");
            }
            return _instance;
        }

        // Finally, any singleton should define some business logic, which can
        // be executed on its instance.
        public static void someBusinessLogic()
        {
            // ...
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Presidente mexico = Presidente.GetInstance();
            Presidente puebla = Presidente.GetInstance();

            if (mexico == puebla)
            {
                Console.WriteLine("Singleton works, ambos estados tienen el mismo presidente (" + mexico.presidente +  ")");
            }
            else
            {
                Console.WriteLine("Singleton failed, ambos estados tienen diferentes presidente");
            }
        }
    }
}