using System;
using Assignment3.Interface;

namespace Assignment3.Models
{
    public class MyService : IMyService
    {
        public string GetMessage()
        {
            return "Hello, World!";
        }
    }
}

