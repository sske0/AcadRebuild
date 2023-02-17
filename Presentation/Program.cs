using Core.Constants;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;
using Presentation.Services;
using System;
using System.Globalization;

namespace Presentation
{
    public static class Program
    {
        private readonly static GroupService _groupService;
        static Program()
        {
            _groupService = new GroupService();
        }
        static void Main()
        {
            ConsoleHelper.WriteWithColor("--- Welcome! ---", ConsoleColor.Cyan);

            while (true)
            {
                ConsoleHelper.WriteWithColor("1 - Group Creation", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("2 - Update Group", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("3 - Delete Group", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("4 - Get All Groups", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("5 - Get Group By Id", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("6 - Get Group By Name", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("0 - Exit", ConsoleColor.DarkCyan);
                ConsoleHelper.WriteWithColor("--- Select option ---", ConsoleColor.Cyan);
                int number;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputed number's format is not valid", ConsoleColor.Red);
                }
                else
                {
                    if (!(number >= 0 && number <= 6))
                    {
                        ConsoleHelper.WriteWithColor("Choose a number from 0 to 6!", ConsoleColor.Red);
                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)GroupOptions.GroupCreation:
                                _groupService.Create();
                                break;
                            case (int)GroupOptions.UpdateGroup:
                                break;
                            case (int)GroupOptions.DeleteGroup:
                                _groupService.Delete();
                                break;
                            case (int)GroupOptions.GetAllGroups:
                                _groupService.GetAll();
                                break;
                            case (int)GroupOptions.GetGroupById:
                                break;
                            case (int)GroupOptions.GetGroupByName:
                                break;
                            case (int)GroupOptions.Exit:
                                if (_groupService.Exit())
                                {
                                    return;
                                }
                                
                                break;

                            default:
                                break;
                        }

                    }
                }
            }

        }
    }
}