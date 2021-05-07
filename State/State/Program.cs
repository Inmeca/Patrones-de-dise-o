
using System;

namespace State
{
    // The Context defines the interface of interest to clients. It also
    // maintains a reference to an instance of a State subclass, which
    // represents the current state of the Context.
    class ReproductorAudio
    {
        // A reference to the current state of the Context.
        private Estado _estado = null;

        public ReproductorAudio(Estado estado)
        {
            this.TransitionTo(estado);
        }

        // The Context allows changing the State object at runtime.
        public void TransitionTo(Estado estado)
        {
            Console.WriteLine($"ReproductorAudio: Cambio a  {estado.GetType().Name}.");
            this._estado = estado;
            this._estado.SetReproductorAudio(this);
        }

        // The Context delegates part of its behavior to the current State
        // object.
        public void Request1()
        {
            this._estado.Handle1();
        }

        public void Request2()
        {
            this._estado.Handle2();
        }
    }

    // The base State class declares methods that all Concrete State should
    // implement and also provides a backreference to the Context object,
    // associated with the State. This backreference can be used by States to
    // transition the Context to another State.
    abstract class Estado
    {
        protected ReproductorAudio _reproductorAudio;

        public void SetReproductorAudio(ReproductorAudio reproductorAudio)
        {
            this._reproductorAudio = reproductorAudio;
        }

        public abstract void Handle1();

        public abstract void Handle2();
    }

    // Concrete States implement various behaviors, associated with a state of
    // the Context.
    class EstadoPausa : Estado
    {
        public override void Handle1()
        {
            Console.WriteLine("EstadoPausa handles request1.");
            Console.WriteLine("EstadoPausa quiere cambiar al estado de reproductorAudio.");
            this._reproductorAudio.TransitionTo(new EstadoReproduciendo());
        }

        public override void Handle2()
        {
            Console.WriteLine("EstadoPausa handles request2.");
        }
    }

    class EstadoReproduciendo : Estado
    {
        public override void Handle1()
        {
            Console.Write("EstadoReproduciendo handles request1.");
        }

        public override void Handle2()
        {
            Console.WriteLine("EstadoReproduciendo handles request2.");
            Console.WriteLine("EstadoReproduciendo quiere cambiar al estado de reproductorAudio.");
            this._reproductorAudio.TransitionTo(new EstadoPausa());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code.
            var reproductorAudio = new ReproductorAudio(new EstadoPausa());
            reproductorAudio.Request1();
            reproductorAudio.Request2();
        }
    }
}