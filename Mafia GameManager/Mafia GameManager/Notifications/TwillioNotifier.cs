using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Mafia_GameManager.Notifications
{
    public enum MessageType
    {
        SMS,
        Whatsapp
    }
    internal class TwillioNotifier
    {
        string messageFrom = "+14155238886";
        string accountSid = "ACd6674c64b406c5f25a57475c67e195ee";
        string authToken = "936e370c2c2f59e9a1fa6ac468f6a21d";

        static TwillioNotifier instance;
        protected TwillioNotifier()
        {
            TwilioClient.Init(accountSid, authToken);
        }
        public static TwillioNotifier Instance()
        {
            if (instance == null)
            {
                instance = new TwillioNotifier();
            }
            return instance;
        }

        public void SendMessage(string toNumber, MessageType messageType, string messageText)
        {
            string fromNumberWithType;
            string toNumberWithType;

            if(messageType == MessageType.Whatsapp)
            {
                fromNumberWithType = "whatsapp:" + messageFrom;
                toNumberWithType = "whatsapp:" + toNumber;
            }
            else
            {
                fromNumberWithType =  messageFrom;
                toNumberWithType =  toNumber;
            }

            try
            {
                var messageOptions = new CreateMessageOptions(new PhoneNumber(toNumberWithType));
                messageOptions.From = new PhoneNumber(fromNumberWithType);
                messageOptions.Body = messageText;

                var response = MessageResource.Create(messageOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }



}
