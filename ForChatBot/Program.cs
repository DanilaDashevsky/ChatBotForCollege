using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using Microsoft.VisualBasic;


internal class Program
{

    public static string muvie;
    public static string token = "6394081048:AAGm67dYZcnZj9nXakp1gHFV-yVAtLaFAhE";
    public static TelegramBotClient client = new TelegramBotClient(token);
    public static string currentGroup = "";

   public static async Task MyErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancel)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error {apiRequestException.Message}",
            _ => exception.ToString()
        };
    }

    public static async Task MyHandlerAsync(ITelegramBotClient botClient, Update update, CancellationToken cancel)
    {

            InlineKeyboardMarkup mainMenu = new InlineKeyboardMarkup(
    new[] {
                new [] { InlineKeyboardButton.WithCallbackData(text: "1 курс",callbackData:"OneCourse"), InlineKeyboardButton.WithCallbackData(text: "2 курс", callbackData: "TwoCourse") },
                new [] { InlineKeyboardButton.WithCallbackData(text: "3 курс",callbackData:"ThreeCourse"), InlineKeyboardButton.WithCallbackData(text: "4 курс", callbackData: "FourCourse") }
    }
    );
            string[] allGroups = new string[] { "П-10", "Д-12", "Ф-13", "Ю-14", "Ш-15", "П-20", "Д-22", "Ф-23", "Ю-24", "Ш-25", "Д-32", "Ф-33", "Ю-34", "Ш-35", "П-40", "Д-42", "Ф-43", "Ю-44", "Ш-45" };
            string[] allDays = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Вся неделя" };

            ReplyKeyboardMarkup replyKeyboardMarkup1 = new(new[]
            {
                new KeyboardButton[] { "П-10", "Д-12", "Ф-13","Ю-14","Ш-15" },
            })
            {
                ResizeKeyboard = true
            };
            ReplyKeyboardMarkup replyKeyboardMarkup2 = new(new[]
    {
                new KeyboardButton[] { "П-20", "Д-22", "Ф-23","Ю-24","Ш-25" },
            })
            {
                ResizeKeyboard = true
            };
            ReplyKeyboardMarkup replyKeyboardMarkup3 = new(new[]
    {
                new KeyboardButton[] { "Д-32", "Ф-33","Ю-34","Ш-35" },
            })
            {
                ResizeKeyboard = true
            };
            ReplyKeyboardMarkup replyKeyboardMarkup4 = new(new[]
            {
                new KeyboardButton[] { "П-40", "Д-42", "Ф-43","Ю-44","Ш-45" },
            })
            {
                ResizeKeyboard = true
            };


            ReplyKeyboardMarkup Week = new(new[]
    {
                new KeyboardButton[] { "Понедельник", "Вторник", "Среда" },
                new KeyboardButton[] { "Четверг","Пятница","Суббота" },
                new KeyboardButton[] { "Вся неделя","Назад༼ つ ◕_◕ ༽つ" },
            })
            {
                ResizeKeyboard = true
            };
            try
            {
            string dir1 = Directory.GetCurrentDirectory() + @"\wwwroot\Schedule\"; // получаем текущую директорию
            if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)
            {

                string messsageText = update.Message.Text;
                string firstName = update.Message.From.FirstName;
                var chatId = update.Message.Chat.Id;
                if (messsageText == "/start")
                {
                    Message setMessage = await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Привет, друг! Выбери свой курс, чтобы получить расписание😀",
                        replyMarkup: mainMenu,
                        cancellationToken: cancel
                        );
                }
                else
                if (allGroups.Contains(messsageText))
                {
                    currentGroup = messsageText;
                    Message setMessage1 = await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Выберите день недели😉",
                    replyMarkup: Week,
                    cancellationToken: cancel
                    );
                }
                else
                if (messsageText == "Назад༼ つ ◕_◕ ༽つ")
                {
                    Message setMessage = await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Выбери свой курс, чтобы получить расписание😀",
                        replyMarkup: mainMenu,
                        cancellationToken: cancel
                );

                }
                else
                if (messsageText == "Вся неделя" && currentGroup != "")
                {
                    Console.WriteLine(dir1 + "Неделя" + currentGroup + ".png");
                    using (var fileStream = new FileStream(dir1 + "Неделя" + currentGroup + ".png", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        try
                        {
                            Message setMessage = await client.SendPhotoAsync(
                            chatId: chatId,
                            photo: InputFile.FromStream(fileStream),
                            cancellationToken: cancel
                        );
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                else
                if ((allDays.Contains(messsageText) && currentGroup != ""))
                {

                    DirectoryInfo dir = new DirectoryInfo(dir1);
                    foreach (FileInfo item in dir.GetFiles())
                    {

                        #region Этот скрипт я использовал для написания переименовывающего кода, чтобы привести к единому виду
                        //Console.WriteLine(item.FullName);
                        //if (item.Name.IndexOf(' ') != -1)
                        //{
                        //    string newName = item.Name;
                        //    newName = newName.Replace(" ", "");
                        //    try
                        //    {
                        //        Console.WriteLine(item.Name);
                        //        
                        //        System.IO.File.Move(item.Name, newName);
                        //        Console.WriteLine($"Работаетssssssssssssssssssssssssssssssssssssssssssssssssssssssss");
                        //    }
                        //    catch (Exception ex)
                        //    {

                        //        Console.WriteLine($"{ex.Message}");
                        //    }
                        //}
                        #endregion
                        if (item.Name.Contains(currentGroup) && item.Name.Contains(messsageText))
                        {
                            using (var fileStream = new FileStream(dir1 + messsageText + currentGroup + ".png", FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                Message setMessage = await client.SendPhotoAsync(
                                    chatId: chatId,
                                    photo: InputFile.FromStream(fileStream),
                                    cancellationToken: cancel
                                );
                            }
                            break;
                        }
                    }
                }
                else
                {
                    Message setMessage1 = await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Извини дорогой, я не запрограммирован отвечать на подобные сообщения😉.\n Если хочешь, я могу подсказать тебе твой расписание😜",
                        replyMarkup: replyKeyboardMarkup1,
                        cancellationToken: cancel
                        );
                }
            }


            if (update.Type == UpdateType.CallbackQuery)
            {
                if (update.CallbackQuery != null)
                    if (update.CallbackQuery.Data == "OneCourse")
                    {
                        //Message setMessage = await client.SendPhotoAsync(

                        //    chatId: update.CallbackQuery.Message.Chat.Id,
                        //    photo: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg"),
                        //    cancellationToken: cancel
                        //    );
                        Message setMessage1 = await client.SendTextMessageAsync(
                            chatId: update.CallbackQuery.Message.Chat.Id,
                            text: $"Выберите свою группу😁",
                            replyMarkup: replyKeyboardMarkup1,
                            cancellationToken: cancel
                            );
                    }
                    else
                    if (update.CallbackQuery.Data == "TwoCourse")
                    {

                        Message setMessage1 = await client.SendTextMessageAsync(
                            chatId: update.CallbackQuery.Message.Chat.Id,
                            text: $"Выберите свою группу😁",
                            replyMarkup: replyKeyboardMarkup2,
                            cancellationToken: cancel
                            );
                    }
                    else
                    if (update.CallbackQuery.Data == "ThreeCourse")
                    {
                        Message setMessage1 = await client.SendTextMessageAsync(
                            chatId: update.CallbackQuery.Message.Chat.Id,
                            text: $"Выберите свою группу😁",
                            replyMarkup: replyKeyboardMarkup3,
                            cancellationToken: cancel
                            );
                    }
                    else
                   if (update.CallbackQuery.Data == "FourCourse")
                    {
                        Message setMessage1 = await client.SendTextMessageAsync(
                            chatId: update.CallbackQuery.Message.Chat.Id,
                            text: $"Выберите свою группу😁",
                            replyMarkup: replyKeyboardMarkup4,
                            cancellationToken: cancel
                            );
                    }
                    else
                    {
                        Message setMessage1 = await client.SendTextMessageAsync(
                            chatId: update.CallbackQuery.Message.Chat.Id,
                            text: $"Нет какого курса. Нажмите на кнопку, если хотите выбрать курс",
                            replyMarkup: mainMenu,
                            cancellationToken: cancel
                        );
                    }
            }
        }
        catch (Exception ex)
        {

                    Message setMessage1 = await client.SendTextMessageAsync(
            chatId: update.CallbackQuery.Message.Chat.Id,
            text: $"Извините, произошла следующая ошибка: {ex.Message}",
            replyMarkup: mainMenu,
            cancellationToken: cancel
            );
        }

    }

        private static async Task Main(string[] args)
        {
        try
        {
            var ctss = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            client.StartReceiving(
                MyHandlerAsync,
                MyErrorAsync,
                receiverOptions,
                cancellationToken: ctss.Token
                );
            var me = await client.GetMeAsync();
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Извините, произошла следующая ошибка: {ex.Message}");
        }


            var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

        
 
        }

    }

