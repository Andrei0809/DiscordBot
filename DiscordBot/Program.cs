using DiscordBot.Commands;
using DiscordBot.Models;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.VoiceNext;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Program
    {
        static DiscordClient discordClient;
        static CommandsNextExtension commandsNextModule;
        static VoiceNextExtension voice;

        static void Main(string[] args)
        {

            RunBotAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task RunBotAsync(string[] args)
        {
            var json = "";
            var path = Path.GetFullPath("config.json");
            using (var fileStream = File.OpenRead(path))
            using (var streamReader = new StreamReader(fileStream, new UTF8Encoding(false)))
                json = await streamReader.ReadToEndAsync();

            var appSettingsModel = JsonConvert.DeserializeObject<AppSettingsModel>(json);
                    


            discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = "",
                TokenType = TokenType.Bot,
            });

            commandsNextModule = discordClient.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new List<string> {"``", "~~"},
                EnableDms = false,
                CaseSensitive = false,
                
            });;

            voice = discordClient.UseVoiceNext();
            commandsNextModule.RegisterCommands<BotCommands>();

            await discordClient.ConnectAsync();
            await Task.Delay(-1);
        }




    }


}
