using System;

namespace App
{
    public class ErrorViewModel
    {
        public string Error { get; }

        public ErrorViewModel(string error)
        {
            Error = error;
        }

        public ErrorViewModel(Exception exception) : this(exception.ToString())
        {

        }
    }
}
