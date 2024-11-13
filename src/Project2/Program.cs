using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("8168139255:AAEivPc7c5MAeDz3h1TWQgR2TroWhIYA7xg", cancellationToken: cts.Token);
var me = await bot.GetMeAsync();
var userNames = new Dictionary<string, string>();
bot.OnMessage += OnMessage;
bot.OnUpdate += OnUpdate;
await bot.SendTextMessageAsync("5210175632", "Здравствуйте, для начала напишите /start ");

Console.WriteLine($"@{me.Username} запущен... Нажмите Enter чтобы выключить");
Console.ReadLine();
cts.Cancel();

async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text == "Проверка")
    {
        await bot.SendTextMessageAsync(msg.Chat, "Проверка бота: работа корректна");
    }
    if (msg.Text == "Привет")
    {
        await bot.SendTextMessageAsync(msg.Chat, "Здравствуйте! Добро пожаловать в Школу Инфо-цыпленок.");
    }
    if (msg.Text == "/start")
    {
        await bot.SendTextMessageAsync("5210175632", "Выберите курс:",
           replyMarkup: new InlineKeyboardMarkup().AddButtons("Продажа палок", "Консультации по грядкам", "Курс по маркетингу", "help"));
    }
}

async Task OnUpdate(Update update)
{
    if (update is { CallbackQuery: { } query })
    {
        await bot.AnswerCallbackQueryAsync(query.Id, $"Вы выбрали: {query.Data}");

        if (query.Data == "Продажа палок")
        {
            await bot.SendTextMessageAsync("5210175632", "Вы активировали курс по продаже палок. Узнайте, как заработать на простых вещах!");
        }
        if (query.Data == "Консультации по грядкам")
        {
            await bot.SendTextMessageAsync("5210175632", "Вы активировали курс по планированию дачных грядок. Научитесь эффективно использовать пространство!");
        }
        if (query.Data == "Курс по маркетингу")
        {
            await bot.SendTextMessageAsync("5210175632", "Вы активировали курс по маркетингу. Узнайте, как продвигать свои продукты и услуги!");
        }
        if (query.Data == "help")
        {
            await bot.SendTextMessageAsync("5210175632", "Курсы помогут вам развить свой инфобизнес. Выберите подходящий для вас курс.");
        }
    }
}
