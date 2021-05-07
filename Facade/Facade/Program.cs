using System;

namespace Facade
{
    // The Facade class provides a simple interface to the complex logic of one
    // or several subsystems. The Facade delegates the client requests to the
    // appropriate objects within the subsystem. The Facade is also responsible
    // for managing their lifecycle. All of this shields the client from the
    // undesired complexity of the subsystem.
    public class VideoConverter
    {
        protected VideoFile _videoFile;

        protected AudioMixer _AudioMixer;

        public VideoConverter(VideoFile videoFile, AudioMixer AudioMixer)
        {
            this._videoFile = videoFile;
            this._AudioMixer = AudioMixer;
        }

        // The Facade's methods are convenient shortcuts to the sophisticated
        // functionality of the subsystems. However, clients get only to a
        // fraction of a subsystem's capabilities.
        public string Operation()
        {
            string result = "VideoConverter initializes subsystems:\n";
            result += this._videoFile.operation1();
            result += this._AudioMixer.operation1();
            result += "VideoConverter orders subsystems to perform the action:\n";
            result += this._videoFile.operationN();
            result += this._AudioMixer.operationZ();
            return result;
        }
    }

    // The Subsystem can accept requests either from the facade or client
    // directly. In any case, to the Subsystem, the Facade is yet another
    // client, and it's not a part of the Subsystem.
    public class VideoFile
    {
        public string operation1()
        {
            return "VideoFile: Ready!\n";
        }

        public string operationN()
        {
            return "VideoFile: Go!\n";
        }
    }

    // Some facades can work with multiple subsystems at the same time.
    public class AudioMixer
    {
        public string operation1()
        {
            return "AudioMixer: Get ready!\n";
        }

        public string operationZ()
        {
            return "AudioMixer: Fire!\n";
        }
    }


    class Client
    {
        // The client code works with complex subsystems through a simple
        // interface provided by the Facade. When a facade manages the lifecycle
        // of the subsystem, the client might not even know about the existence
        // of the subsystem. This approach lets you keep the complexity under
        // control.
        public static void ClientCode(VideoConverter videoConverter)
        {
            Console.Write(videoConverter.Operation());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code may have some of the subsystem's objects already
            // created. In this case, it might be worthwhile to initialize the
            // Facade with these objects instead of letting the Facade create
            // new instances.
            VideoFile videoFile = new VideoFile();
            AudioMixer audioMixer = new AudioMixer();
            VideoConverter videoConverter = new VideoConverter(videoFile, audioMixer);
            Client.ClientCode(videoConverter);
        }
    }
}