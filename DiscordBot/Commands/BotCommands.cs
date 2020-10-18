using System.Net.Http;
using System.Threading.Tasks;
using DiscordBot.Models;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.VoiceNext;
using Newtonsoft.Json;

namespace DiscordBot.Commands
{
    
    public class BotCommands : BaseCommandModule
    {
        [Command("hello"), Description("Says hello.")]
        public async Task Hello(CommandContext commandContext)
        {
            await commandContext.RespondAsync($"Hello, {commandContext.User.Mention}!");
        }

        [Command("whoareu?"), Description("Learn about the bot.")]
        public async Task WhoAreYou(CommandContext commandContext)
        {
            await commandContext.RespondAsync($"My name is CbD (Chased by Dogs), I am a bot created by two sleep deprived hovers.");
        }

        [Command("news:"), Description("Return news article.")]
        public async Task News(CommandContext commandContext, [Description("Section.")] string section)
        {
            var response = await GetMeAnArticle("", section);
            await commandContext.RespondAsync(response.ToString());
        }

        [Command("technews"), Description("Return news article.")]
        public async Task TalkTechWithMe(CommandContext commandContext)
        {
            var response = await GetMeAnArticle("", "technology");
            await commandContext.RespondAsync(response.ToString());
        }

        [Command("join"), Description("Bot joins the voice channel")]
        public async Task YouAreNotAlone (CommandContext commandContext)
        {
            var voiceNextClient = commandContext.Client.GetVoiceNext();
            var voiceChannel = commandContext.Member?.VoiceState?.Channel;
            var serverConnection = voiceNextClient.GetConnection(commandContext.Guild);

            if (serverConnection == null)
            {
                if (voiceChannel != null)
                {
                    await commandContext.RespondAsync($"Moving to channel {voiceChannel.Mention}, at the request of {commandContext.User.Mention}");
                    var voiceConnection = voiceNextClient.GetConnection(commandContext.Guild);
                    voiceConnection = await voiceNextClient.ConnectAsync(voiceChannel);

                }
                else if (voiceChannel == null)
                    await commandContext.RespondAsync("No can do!");
            }else
                await commandContext.RespondAsync("Busy!");

        }

        [Command("leave"), Description("Bot leaves the voice channel")]
        public async Task LeaveMeAlone(CommandContext commandContext)
        {
            var voiceNextClient = commandContext.Client.GetVoiceNext();
            var serverConnection = voiceNextClient.GetConnection(commandContext.Guild);
            var voiceChannel = commandContext.Member?.VoiceState?.Channel;
            var serverChannelConenction = serverConnection.Channel;

            if (serverConnection != null)
            {
                if (voiceChannel != serverChannelConenction) {
                    await commandContext.RespondAsync($"You are not in the right server!");
                }
                else
                {
                    serverConnection.Disconnect();
                    await commandContext.RespondAsync($"Left channel, at the request of {commandContext.User.Mention}");
                }
            }
            else
                await commandContext.RespondAsync("No can do!");

        }

        public async Task<string> GetMeAnArticle(string apiKey, string section)
        {
            var client = new HttpClient();
            var request = await client.GetAsync("https://content.guardianapis.com/search?api-key=" + apiKey + "&section=" + section + "&order-by=newest&page-size=1");
            var content = request.Content.ReadAsStringAsync().Result;
            var articleModel = JsonConvert.DeserializeObject<ArticleModel>(content);
            var webUrl = articleModel.response.results[0].webUrl;

            return webUrl;
        }
    }
}
