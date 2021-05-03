using System;
using NUnit.Framework;
using System.Linq;
using MultiValueDictionarySample.Helpers;

namespace MultiValueDictionarySample.Tests
{
    public class DictionaryHelperTests
    {
        private CommandValidator _commandValidator;

        #region Setup

        [SetUp]
        public void Setup()
        {
            _commandValidator = new CommandValidator();
            DictionaryOperations.Clear();
        }

        #endregion

        #region Input Validation Tests

        [TestCase("1", true)]
        [TestCase("10", true)]
        [TestCase(".", false)]
        [TestCase("<", false)]
        public void IsValid_GivenCommand_ReturnsExpectedResult(string command, bool expectedResult)
        {
            //Arrange & Act
            var result = _commandValidator.IsValid(command);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase]
        public void ValidateArguments_SuccessTest()
        {
            //Arrange & Act
            Runner run = new Runner(_commandValidator);

            // Assert
            Assert.Throws<ArgumentException>(() => run.ValidateArguments(null));
            Assert.Throws<ArgumentException>(() => run.ValidateArguments(""));
            Assert.Throws<ArgumentException>(() => run.ValidateArguments(" "));
            Assert.DoesNotThrow(() => run.ValidateArguments("Test"));
        }

        #endregion

        #region Get Keys Tests 

        [TestCase("baz", "bang")]
        public void GetKeys_SuccessTest(string key, string value)
        {
            //Arrange 
            DictionaryOperations.Add(key, value);

            // Assert
            Assert.AreEqual(key, DictionaryOperations.GetKeys().FirstOrDefault());
            Assert.IsTrue(DictionaryOperations.GetKeys().Count == 1);
        }

        [TestCase]
        public void GetKeys_FailTest()
        {
            // Assert
            Assert.IsFalse(DictionaryOperations.GetKeys().Count == 1);
        }

        #endregion

        #region Get Members Tests 

        [TestCase("foo", "bar")]
        public void GetMember_SuccessTest(string key, string value)
        {
            //Arrange 
            DictionaryOperations.Add(key, value);
            DictionaryOperations.Add(key, $"{value}Test");

            // Assert 
            Assert.IsTrue(DictionaryOperations.GetMembers(key).ToList().Count == 2);
        }

        [TestCase("foo", "bar")]
        public void GetMember_FailTest(string key, string value)
        {
            //Arrange 
            DictionaryOperations.Add(key, value);

            // Assert 
            Assert.IsNull(DictionaryOperations.GetMembers("InvalidKey"));
        }

        #endregion

        #region Add Tests

        [TestCase("foo", "bar")]
        [TestCase("foo", "baz")]
        [TestCase("baz", "bang")]
        public void Add_SuccessTest(string key, string value)
        {
            //Arrange & Act
            DictionaryOperations.Add(key, value);

            // Assert
            Assert.IsNotNull(DictionaryOperations.ApplicationDictionary);
            Assert.IsTrue(DictionaryOperations.ApplicationDictionary.Count >= 1);
        }

        [TestCase("foo", "bar")]
        public void Add_FailTest(string key, string value)
        {
            //Arrange & Act
            DictionaryOperations.Add(key, value);
            DictionaryOperations.Add(key, value);

            // Assert
            Assert.IsNotNull(DictionaryOperations.ApplicationDictionary);
            Assert.IsFalse(DictionaryOperations.ApplicationDictionary.Count >= 2);
        }

        #endregion

        #region Remove Tests

        [TestCase("foo", "bar")]
        public void Remove_SuccessTest(string key, string value)
        {
            //Arrange
            DictionaryOperations.Add(key, value);

            //Action
            DictionaryOperations.Remove(key, value);

            // Assert  
            Assert.IsFalse(DictionaryOperations.ValueExists(key, value));
        }

        [TestCase("foo", "bar")]
        public void Remove_FailTest(string key, string value)
        {
            //Arrange 
            DictionaryOperations.Add(key, value);

            //Action
            DictionaryOperations.Remove(key, $"{value}Test");

            // Assert
            Assert.IsNotNull(DictionaryOperations.ApplicationDictionary);
            Assert.IsTrue(DictionaryOperations.KeyExists(key));
        }


        [TestCase("foo", "bar")]
        public void RemoveAll_Test(string key, string value)
        {
            //Arrange 
            DictionaryOperations.Add(key, value);
            DictionaryOperations.Add(key, $"{value}Test");

            //Action
            DictionaryOperations.RemoveAll(key);

            // Assert 
            Assert.IsNull(DictionaryOperations.GetMembers(key));
            Assert.IsFalse(DictionaryOperations.KeyExists(key));
        }

        #endregion

        #region Clear Tests

        [TestCase("foo", "bar")]
        public void Clear_SuccessTest(string key, string value)
        {
            //Arrange
            DictionaryOperations.Add(key, value);
            DictionaryOperations.Add(key, $"{value}Test");

            //Action
            DictionaryOperations.Clear();

            // Assert  
            Assert.IsTrue(DictionaryOperations.ApplicationDictionary.Count == 0);
        }

        #endregion

        #region Key & Value Tests

        [TestCase("foo", "bar")]
        public void KeyValueExists_SuccessTest(string key, string value)
        {
            //Arrange
            DictionaryOperations.Add(key, value);

            // Action & Assert  
            Assert.IsTrue(DictionaryOperations.KeyExists(key));
            Assert.IsTrue(DictionaryOperations.ValueExists(key, value));
        }

        #endregion
    }
}