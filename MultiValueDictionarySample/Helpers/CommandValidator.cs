using System;
using MultiValueDictionarySample.Interfaces;

namespace MultiValueDictionarySample.Helpers
{
    public class CommandValidator : ICommandValidator
    { 
        public bool IsValid(string command)
        {
            bool isNumber = int.TryParse(command, out var option);
            return isNumber && option <= Enum.GetNames(typeof(CommandType)).Length; 
        } 
    }
}
