using System;

namespace Proxy
{
    // The Subject interface declares common operations for both RealSubject and
    // the Proxy. As long as the client works with RealSubject using this
    // interface, you'll be able to pass it a proxy instead of a real subject.
    public interface IThirdPartyYoutubeLib
    {
        void Request();
    }

    // The RealSubject contains some core business logic. Usually, RealSubjects
    // are capable of doing some useful work which may also be very slow or
    // sensitive - e.g. correcting input data. A Proxy can solve these issues
    // without any changes to the RealSubject's code.
    class ThirdPartyYoutubeClass : IThirdPartyYoutubeLib
    {
        public void Request()
        {
            Console.WriteLine("ThirdPartyYoutubeClass: Handling Request.");
        }
    }

    // The Proxy has an interface identical to the RealSubject.
    class CachedYoutubeClass : IThirdPartyYoutubeLib
    {
        private ThirdPartyYoutubeClass _thirdPartyYoutubeClass;

        public CachedYoutubeClass(ThirdPartyYoutubeClass thirdPartyYoutubeClass)
        {
            this._thirdPartyYoutubeClass = thirdPartyYoutubeClass;
        }

        // The most common applications of the Proxy pattern are lazy loading,
        // caching, controlling the access, logging, etc. A Proxy can perform
        // one of these things and then, depending on the result, pass the
        // execution to the same method in a linked RealSubject object.
        public void Request()
        {
            if (this.CheckAccess())
            {
                this._thirdPartyYoutubeClass.Request();

                this.LogAccess();
            }
        }

        public bool CheckAccess()
        {
            // Some real checks should go here.
            Console.WriteLine("CachedYoutubeClass: Checking access prior to firing a real request.");

            return true;
        }

        public void LogAccess()
        {
            Console.WriteLine("CachedYoutubeClass: Logging the time of request.");
        }
    }

    public class YoutubeManager
    {
        // The client code is supposed to work with all objects (both subjects
        // and proxies) via the Subject interface in order to support both real
        // subjects and proxies. In real life, however, clients mostly work with
        // their real subjects directly. In this case, to implement the pattern
        // more easily, you can extend your proxy from the real subject's class.
        public void YoutubeManagerCode(IThirdPartyYoutubeLib subject)
        {
            // ...

            subject.Request();

            // ...
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            YoutubeManager client = new YoutubeManager();

            Console.WriteLine("YoutubeManager: Executing the YoutubeManager code with a real subject:");
            ThirdPartyYoutubeClass thirdPartyYoutubeClass = new ThirdPartyYoutubeClass();
            client.YoutubeManagerCode(thirdPartyYoutubeClass);

            Console.WriteLine();

            Console.WriteLine("YoutubeManager: Executing the same YoutubeManager code with a CachedYoutubeClass:");
            CachedYoutubeClass cachedYoutubeClass = new CachedYoutubeClass(thirdPartyYoutubeClass);
            client.YoutubeManagerCode(cachedYoutubeClass);
        }
    }
}