namespace NetDelegatsVariationApp
{
    delegate Message MessageBuilder(string text);
    delegate void EmailReceiver(EmailMessage message);

    delegate T MessageBuilderGeneric<out T>(string text);
    delegate void MessageReceiverGeneric<in T>(T message);

    delegate U MessageConverter<out U, in T>(T Message);

    internal class Program
    {
        static void Main(string[] args)
        {
            // Ковариантность
            MessageBuilder messageBuilder = CreateEmailMessage;
            Message message = messageBuilder("Example Covariant");
            message.Print();

            // Ковариантность для обобщенных делегатов
            MessageBuilderGeneric<Message> messageBuilderGeneric = CreateEmailMessageGeneric;
            Message message2 = messageBuilderGeneric("Example Generic Covariant");
            message2.Print();



            // Контрвариантность
            EmailReceiver emailReceiver = ReceiverMessage;
            emailReceiver(new EmailMessage("Example Contrvariant"));

            // Контрвариантность для обобщенных делегатов
            MessageReceiverGeneric<Message> receiverMessageGeneric 
                = (Message message) => message.Print();
            //    delegate (Message message)
            //{
            //    message.Print();
            //};

            MessageReceiverGeneric<EmailMessage> messageReceiverGeneric = receiverMessageGeneric;

            receiverMessageGeneric(new Message("Example Generic Contrvariant Message"));
            receiverMessageGeneric(new EmailMessage("Example Generic Contrvariant Email Message"));


            // Совмещение коваринтности и контрвариантности для обощений
            
            // лябда вариант метода с строчки
            MessageConverter<EmailMessage, Message> toEmailConverter
                = (Message message) => new EmailMessage(message.Text);

            MessageConverter<Message, SmsMessage> converter1 = toEmailConverter;
            MessageConverter<Message, SmsMessage> converter2 = EmailMessageConverter;

            Message message3 = converter1(new SmsMessage("Co and Contr together"));
            message3.Print();

        }
        static EmailMessage CreateEmailMessage(string text) => new EmailMessage(text);
        static void ReceiverMessage(Message message) => message.Print();


        static MessageBuilderGeneric<EmailMessage> CreateEmailMessageGeneric
            = (string text) => new EmailMessage(text);

        static EmailMessage EmailMessageConverter(Message message)
        {
            return new EmailMessage(message.Text);
        }


    }
}