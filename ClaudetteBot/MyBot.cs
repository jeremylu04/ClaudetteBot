using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaudetteBot
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;
        Random rand;

        string[] freshestMemes;
        string[] messages;
        string[] trystins;

        public MyBot()
        {
            rand = new Random();

            freshestMemes = new string[]
            {
                "memes/meme2.jpg",
                "memes/meme3.jpg",
                "memes/meme4.jpg",
                "memes/meme5.jpg",
            };

            messages = new string[]
            {
                "Why, hello there, Summoner",
                "Hello hello",
                "Heroes never die",
                "I can assist you with my botany knowledge!",
                "Michael Myers is a piece of shit",
                "Leader is the most useless perk",
            };

            trystins = new string[]
            {
                "Did you do your dishes yet?",
                "Be nice to mom",
                "Stop playing thoth"
            };

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '~';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            commands.CreateCommand("hello")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Hi!");
                });

            commands.CreateCommand("fuck you")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Fuck you too");
                });

            commands.CreateCommand("You hurt people's feelings")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("I'm sorry");
                });

            RegisterMemeCommand();
            RegisterTalkCommand();
            RegisterTrystinCommand();
            RegisterPurgeCommand();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("Mjc4NjAwNzQ0NjQ5ODgzNjU4.C3u0Cw.KSrlIxiI1vQ64-1devLtlHWqiJg", TokenType.Bot);
            });
        }

        private void RegisterMemeCommand()
        {
            commands.CreateCommand("meme")
                .Do(async (e) =>
                {
                    int randomMemeIndex = rand.Next(freshestMemes.Length);
                    string memeToPost = freshestMemes[randomMemeIndex];
                    await e.Channel.SendFile(memeToPost);
                });
        }

        private void RegisterPurgeCommand()
        {
            commands.CreateCommand("purge")
                .Do(async (e) =>
                {
                    Message[] messagesToDelete;
                    messagesToDelete = await e.Channel.DownloadMessages(100);

                    await e.Channel.DeleteMessages(messagesToDelete);
                });
        }

        private void RegisterTalkCommand()
        {
            commands.CreateCommand("talk")
                .Do(async (e) =>
                {
                    int randomMessagesIndex = rand.Next(messages.Length);
                    string message = messages[randomMessagesIndex];
                    await e.Channel.SendMessage(message);
                });
        }

        private void RegisterTrystinCommand()
        {
            commands.CreateCommand("trystin")
                .Do(async (e) =>
                {
                    int randomMessagesIndex = rand.Next(trystins.Length);
                    string message = trystins[randomMessagesIndex];
                    await e.Channel.SendMessage(message);
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
