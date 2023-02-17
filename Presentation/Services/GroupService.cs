using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    class GroupService
    {
        private readonly GroupRepository _groupRepository;
        public GroupService()
        {
            _groupRepository = new GroupRepository();
        }
        public void GetAll()
        {
            var groups = _groupRepository.GetAll();
            foreach (var group in groups)
            {
                ConsoleHelper.WriteWithColor($"Id: {group.Id} Name: {group.Name} Max size: {group.MaxSize}, Start date: {group.StartDate} End date: {group.EndDate}", ConsoleColor.Magenta);
            }
        }
        public void Create()
        {
            ConsoleHelper.WriteWithColor("-- Enter name: ", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();

        MaxSizeInput: ConsoleHelper.WriteWithColor("-- Enter max size of the group: ", ConsoleColor.DarkCyan);
            int maxSize;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed size format is not valid", ConsoleColor.Red);
                goto MaxSizeInput;
            }

            if (maxSize > 20)
            {
                ConsoleHelper.WriteWithColor("Max size of group is 20", ConsoleColor.Red);
                goto MaxSizeInput;
            }

        StartDateInput: ConsoleHelper.WriteWithColor("-- Enter start date: ", ConsoleColor.DarkCyan);
            DateTime startDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Start date's format is not correct!", ConsoleColor.Red);
                goto StartDateInput;
            }

            DateTime boundaryDate = new DateTime(2015, 1, 1);

            if (startDate < boundaryDate)
            {
                ConsoleHelper.WriteWithColor("Start date is not chosen right", ConsoleColor.Red);
                goto StartDateInput;
            }

        EndDateInput: ConsoleHelper.WriteWithColor("-- Enter end date: ", ConsoleColor.DarkCyan);
            DateTime endDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Start date's format is not correct!", ConsoleColor.Red);
                goto EndDateInput;
            }

            if (startDate > endDate)
            {
                ConsoleHelper.WriteWithColor("End date cant be earlier than start date!", ConsoleColor.Red);
                goto EndDateInput;
            }

            var group = new Group
            {
                Name = name,
                MaxSize = maxSize,
                StartDate = startDate,
                EndDate = endDate,
            };

            _groupRepository.Add(group);
            ConsoleHelper.WriteWithColor($"Group was successfully created with Name: {group.Name}\n Max size: {group.MaxSize}\n Start date: {group.StartDate.ToLongDateString()}\n End date: {group.EndDate.ToLongDateString()}", ConsoleColor.Magenta);
        }
        public void Delete()
        {
            GetAll();
        IdInput: ConsoleHelper.WriteWithColor("-- Enter Id: ", ConsoleColor.DarkCyan);

            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Id's format is not correct!", ConsoleColor.Red);
                goto IdInput;
            }

            var dbGroup = _groupRepository.Get(id);
            if (dbGroup is null)
                ConsoleHelper.WriteWithColor("There is no such a group with written id", ConsoleColor.Red);
            else
            {
                _groupRepository.Delete(dbGroup);
                ConsoleHelper.WriteWithColor("The group was successfully deleted", ConsoleColor.Green);
            }
        }
        public bool Exit()
        {
        Decision: ConsoleHelper.WriteWithColor("Are you sure? --- y or n ---", ConsoleColor.DarkRed);
            char decision;
            bool isSucceeded = char.TryParse(Console.ReadLine(), out decision);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid format!", ConsoleColor.Red);
                goto Decision;
            }
            if (!(decision == 'y' || decision == 'n'))
            {
                ConsoleHelper.WriteWithColor("Choose between y and no!", ConsoleColor.Red);
                goto Decision;
            }
            if (decision == 'y')
            {
                return true;
            }
            else
                return false;
        }
    }
}
