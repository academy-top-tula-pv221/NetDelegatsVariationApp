using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDelegatsVariationApp
{
    internal class Message
    {
        public string? Text { set; get; }
        public Message(string? text) => Text = text;
        public virtual void Print() => Console.WriteLine($"Message text: {Text}");
    }

    internal class EmailMessage : Message
    {
        public EmailMessage(string? text) : base(text) { }
        public override void Print() => Console.WriteLine($"Email message: {Text}");

    }

    internal class SmsMessage : Message
    {
        public SmsMessage(string? text) : base(text) { }
        public override void Print() => Console.WriteLine($"Sms message: {Text}");

    }
}