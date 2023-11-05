using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using DownloadVideo.Service.Services;
using DownloadVideo.Service.DTOs.Users;
using DownloadVideo;

public class TelegramBot
{
    private static ITelegramBotClient botClient;
    private static Startup userService;
    private static void Main(string[] args)
    {
        botClient = new TelegramBotClient("5640039846:AAEs_sFzdjfogP15Z1qikHpO10hDE6hAZw8");
        using CancellationTokenSource cts = new();

        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
        };

           botClient.StartReceiving(
           updateHandler: HandleUpdateAsync,
           pollingErrorHandler: HandlePollingErrorAsync,
           receiverOptions: receiverOptions,
           cancellationToken: cts.Token
           );


        Console.ReadLine();
        // Send cancellation request to stop bot
        cts.Cancel();
    }

    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Only process Message updates: https://core.telegram.org/bots/api#message
        if (update.Message is not { } message)
            return;
        // Only process text messages
        if (message.Text is not { } messageText)
            return;
        var chatId = message.Chat.Id;
        if (messageText == "/start")
        {
            Message sentMessageLink = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Botga xush kelibsiz! Botdan foydalanish uchun qaysi ishtimoiy tarmoqdan video yuklamoqchi bo'lsangiz birinchi uning nomini yozib keyin linkini tashlaysiz misol uchun: /tiktok linnk",
            cancellationToken: cancellationToken);

            // using Telegram.Bot.Types.ReplyMarkups;

            ReplyKeyboardMarkup replyKeyboardMarkupUser = new(new[]
            {
                    KeyboardButton.WithRequestContact("Share Contact"),
            });

            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Botdan foydalanish uchun iltimos nomeringizni tashlang.",
                replyMarkup: replyKeyboardMarkupUser,
                cancellationToken: cancellationToken);
        }
        else if (message.Contact != null)
        {
            UserForCreationDto user = new()
            {
                Name = message.Chat.FirstName,
                UserName = message.Chat.Username,
                TelegramId = message.Chat.Id,
                Phone = message.Contact.PhoneNumber
            };
 
            await userService.PostAsync(user);
        }
        
        else
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Facebook","TikTok" },
            })
            {

                ResizeKeyboard = true
            };

            string[] m = messageText.Split();
            if (m.Length < 2)
            {
                Message sentMessageLink = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Siz linkni tashlamadingiz",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);
            }

            if (messageText.StartsWith("/facebook"))
            {
                
                FacebookVideo facebookVideo = new();
                string body = await facebookVideo.DownloadFacebookVideoAsync(m[1]);

                var b1 = body.Split("{");
                if (b1.Length < 3)
                {
                    Message sentMessageLink = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Bu videoni yuklab olib bo'lmaydi.Iltimos boshqa link tashlab ko'ring!",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                    return;
                }
                var b2 = b1[2].Split('"');
                if (b2.Length < 8)
                {
                    Message sentMessageLink = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Bu videoni yuklab olib bo'lmaydi.Iltimos boshqa link tashlab ko'ring!",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                    return;
                }
                    
                Message sentMessageLin = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: b2[7],
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);


            }                                                   
            else if (messageText.StartsWith("/tiktok"))
            {
                if (!m[1].Contains("tiktok"))
                    return;
                TikTokVideo tikTokVideo = new();
                string body = await tikTokVideo.DownloadTikTokVideoAsync(m[2]);

                var b1 = body.Split('[');
                if (b1.Length < 2)
                {
                    Message sentMessageLink = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Bu videoni yuklab olib bo'lmaydi.Iltimos boshqa link tashlab ko'ring!",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                    return;
                }
                var b3 = b1[1].Split(']');

                Message sentMessage3 = await botClient.SendVideoAsync(
                            chatId: chatId,
                            video: InputFileUrl.FromUri(b3[0].Split('"')[1]),
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken);
            }
           
        }
    }

    public static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}