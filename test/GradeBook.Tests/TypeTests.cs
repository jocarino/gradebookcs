using System;
using Xunit;

namespace GradeBook.Tests
{

    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTests
    {
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            //log = new WriteLogDelegate(ReturnMessage);
            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello");
            Assert.Equal(3, count);
        }

        string IncrementCount(string message)
        {
            count++;
            return message;
        }

        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        [Fact]
        public void Test1()
        {
            //Given
            var x = GetInt();
            SetInt(ref x);
            //When
            
            //Then
            Assert.Equal(42, x);
        } 

        private void SetInt(ref int x)
        {
            x = 42;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            // arrange
            var book1 = GetBook("Book1");
            GetBookSetName( ref book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }


        [Fact]
        public void CSharpIsPassByValue()
        {
            // arrange
            var book1 = GetBook("Book1");
            GetBookSetName(book1, "New Name");

            Assert.Equal("Book1", book1.Name);
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CanSetNAmeFromReference()
        {
            // arrange
            var book1 = GetBook("Book1");
            SetName(book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void GetBookReturnDifferentObjects()
        {
            // arrange
            var book1 = GetBook("Book1");
            var book2 = GetBook("Book2");

            Assert.Equal("Book1", book1.Name);
            Assert.Equal("Book2", book2.Name);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            // arrange
            var book1 = GetBook("Book1");
            var book2 = book1;

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}
