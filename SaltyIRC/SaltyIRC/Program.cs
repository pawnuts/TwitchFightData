using System;
using System.Threading;

namespace TwitchIRC
{
    class Program
    {
        //Bot settings
        private static string _botName = "enddustforelectronics";
        private static string _broadcasterName = "saltybet";
        private static string _twitchOAuth = "oauth:knkt4jw6rpfeki94isq7bt5jkbnfp6";

        static void Main(string[] args)
        {
            //Initialize and connect to twitch chat
            IrcClient irc = new IrcClient("irc.twitch.tv", 6667,
                _botName, _twitchOAuth, _broadcasterName);

            // Ping to the server to make sure this bot stays connected to the chat
            // Server will respond back to this bot with a PONG (without quotes):
            // Example: ":tmi.twitch.tv PONG tmi.twitch.tv :irc.twitch.tv"
            PingSender ping = new PingSender(irc);

            ping.Start();

            //Listen to the chat until program exits
            while (true)
            {
                //read any message from the chat room
                string message = irc.ReadMessage();
                int startMessage = message.LastIndexOf(':');
                int endMessage = message.Length - 1;

                Console.WriteLine(message); //print raw irc messages
                Console.WriteLine(message.Substring(startMessage, endMessage - startMessage + 1));

                if (message.Contains("waifu4u!waifu4u@waifu4u.tmi.twitch.tv"))
                {
                    // Messages from the users will look something like this (without quotes):
                    // Format: ":[user]![user]@[user].tmi.twitch.tv PRIVMSG #[channel] :[message]"

                    {/*
                    // Modify message to only retrieve user and message
                    int intIndexParseSign = message.IndexOf('!');
                    string username = message.Substring(1, intIndexParseSign - 1);
                    // parse username from specific section (without quotes)
                    // Format: ":[user]!"

                    // Get user's message
                    intIndexParseSign = message.IndexOf('!');
                    message = message.Substring(intIndexParseSign + 2);

                    Console.WriteLine(message); // Print parsed irc message (debugging only)
                    Console.WriteLine(intIndexParseSign);
                    */
                    }
                    string username = message.Substring(1, message.IndexOf('!'));
                    

                    Console.WriteLine(message);
                    

                    // Broadcaster commands
                    if (username.Equals(_broadcasterName))
                    {
                        if (message.Equals("!exitbot"))
                        {
                            irc.SendPublicChatMessage("Bye!");
                            Environment.Exit(0); // Stop the program
                        }
                    }

                    //General commands
                    if (message.Equals("!hello"))
                    {
                        irc.SendPublicChatMessage("Hello World!");
                    }

                }
            }
        }
    }//class Program

    /*public class PingSender
    {
        private IrcClient _irc;
        private Thread pingSender;

        // Empty constructor makes instance of Thread
        public PingSender(IrcClient irc)
        {
            _irc = irc;
            pingSender = new Thread(new ThreadStart(this.Run));
        }

        // Starts the thread
        public void Start()
        {
            pingSender.IsBackground = true;
            pingSender.Start();
        }

        // Send PING to irc server every 5 minutes
        public void Run()
        {
            while (true)
            {
                _irc.SendIrcMessage("PING irc.twitch.tv");
                Thread.Sleep(300000); // 5 minutes
            }
        }
    }*/

}//namespace TwitchIRC
