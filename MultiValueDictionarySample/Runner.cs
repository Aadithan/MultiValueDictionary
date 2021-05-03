using MultiValueDictionarySample.Helpers;
using MultiValueDictionarySample.Interfaces;
using System;

namespace MultiValueDictionarySample
{
    public class Runner : IRunner
    {
        private readonly ICommandValidator _commandValidator;

        public Runner(ICommandValidator commandValidator)
        {
            _commandValidator = commandValidator;
        }

        public void Run()
        {
            ShowHelpText();
            string command;
            do
            {
                command = Console.ReadLine();
                if (!string.IsNullOrEmpty(command) && command.Equals("z"))
                {
                    Console.Clear();
                    Environment.Exit(0);
                }

                if (!_commandValidator.IsValid(command))
                {
                    Console.Clear();
                    Console.Write("Not valid input. Please enter an valid option 0-9");
                    ShowHelpText();
                }
                else
                {
                    int.TryParse(command, out var parsedOption);
                    CommandType cmdType = (CommandType)parsedOption;
                    ExecuteCommand(cmdType);
                }
            }
            while (command != null && !command.Equals("exit", StringComparison.InvariantCultureIgnoreCase));
            Console.ReadKey();
        }

        private void ExecuteCommand(CommandType type)
        {
            string key, value, input;

            switch (type)
            {
                case CommandType.KEYS:
                    Console.Write($"{CommandType.KEYS}{Environment.NewLine}");
                    DictionaryOperations.GetKeys();
                    break;
                case CommandType.MEMBERS:
                    Console.Write($"{CommandType.MEMBERS}{Environment.NewLine}"); 
                    key = Console.ReadLine();
                    ValidateArguments(key);
                    DictionaryOperations.GetMembers(key);
                    break;
                case CommandType.ADD:
                    Console.Write($"{CommandType.ADD}{Environment.NewLine}");
                    input = Console.ReadLine();
                    (key, value) = GetKeyValue(input);
                    if (string.IsNullOrEmpty(value))
                    {
                        value = Console.ReadLine();
                    }
                    ValidateArguments(key);
                    ValidateArguments(value);
                    DictionaryOperations.Add(key, value);
                    break;
                case CommandType.REMOVE:
                    Console.Write($"{CommandType.REMOVE}{Environment.NewLine}");
                    input = Console.ReadLine();
                    (key, value) = GetKeyValue(input);
                    if (string.IsNullOrEmpty(value))
                    {
                        Console.Write("Please enter a value or just press enter to skip");
                        value = Console.ReadLine();
                    } 
                    ValidateArguments(key);
                    DictionaryOperations.Remove(key, value);
                    break;
                case CommandType.REMOVEALL:
                    Console.Write($"{CommandType.REMOVEALL}{Environment.NewLine}");
                    input = Console.ReadLine();
                    (key, _) = GetKeyValue(input);
                    ValidateArguments(key);
                    DictionaryOperations.RemoveAll(key);
                    break;
                case CommandType.CLEAR:
                    Console.Write($"{CommandType.CLEAR}{Environment.NewLine}");
                    DictionaryOperations.Clear();
                    break;
                case CommandType.KEYEXISTS:
                    Console.Write($"{CommandType.KEYEXISTS}{Environment.NewLine}");
                    input = Console.ReadLine();
                    (key, _) = GetKeyValue(input);
                    ValidateArguments(key);
                    DictionaryOperations.KeyExists(key);
                    break;
                case CommandType.VALUEEXISTS:
                    Console.Write($"{CommandType.VALUEEXISTS}{Environment.NewLine}");
                    input = Console.ReadLine();
                    (key, value) = GetKeyValue(input);
                    if (string.IsNullOrEmpty(value))
                    {
                        value = Console.ReadLine();
                    }
                    ValidateArguments(key);
                    ValidateArguments(value);
                    DictionaryOperations.ValueExists(key, value);
                    break;
                case CommandType.ALLMEMBERS:
                    Console.Write($"{CommandType.ALLMEMBERS}{Environment.NewLine}");
                    DictionaryOperations.AllMembers();
                    break;
                case CommandType.ITEMS:
                    Console.Write($"{CommandType.ITEMS}{Environment.NewLine}");
                    DictionaryOperations.Items();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
         
        #region Helper Methods

        private static void ShowHelpText()
        {
            Console.WriteLine("Enter the action to be performed on the dictionary");
            Console.WriteLine("Press 0 (KEYS) to return all the keys in the dictionary");
            Console.WriteLine("Press 1 (MEMBERS) for return the collection of strings for the given key");
            Console.WriteLine("Press 2 (ADD) to add a member to a collection for a given key");
            Console.WriteLine("Press 3 (REMOVE) to removes a value from a key");
            Console.WriteLine("Press 4 (REMOVE ALL) removes all value for a key and removes the key from the dictionary");
            Console.WriteLine("Press 5 (CLEAR) removes all keys and all values from the dictionary.");
            Console.WriteLine("Press 6 (KEY EXISTS) returns whether a key exists or not.");
            Console.WriteLine("Press 7 (VALUE EXISTS) to return whether a value exists within a key");
            Console.WriteLine("Press 8 (ALL MEMBERS) to returns all the values in the dictionary");
            Console.WriteLine("Press 9 (ITEMS) to returns all keys in the dictionary and all of their values");
            Console.WriteLine("Press z (EXIT) the application");
        }

        private static (string, string) GetKeyValue(string input)
        {
            string key;
            string value = null;

            string[] inputArray = input?.Split(' ');
            if (inputArray != null && inputArray.Length == 2)
            {
                key = inputArray[0].Trim();
                value = inputArray[1].Trim();
            }
            else
            {
                key = input;
            }

            return (key, value);
        }

        public void ValidateArguments(string input)
        {
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input)) return;
            Console.WriteLine("Please enter valid inputs.");
            throw new ArgumentException("Please enter valid inputs.");
        }

        #endregion
    }
}
