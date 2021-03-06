﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace Messenger.Bot.Tests
{
    using SendAPI;
    using Buttons;
    using Templates;

    using static Consts;

    [TestClass]
    public class SendClientTests
    {
        private SendApiClient client;

        [TestInitialize]
        public void Intitialize()
        {
            client = new SendApiClient(PageToken);
        }

        [TestMethod]
        public void ApiUriTest()
        {
            string result = client.ApiUri;
            Trace.WriteLine($"ApiUri: {result}");
        }

        [TestMethod]
        public void SendMessageTest()
        {
            string result = client.SendMessageAsync(UserId.ToString(), "Test message").Result.Id;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void SendMessageWithQuickReplyTest()
        {
            List<QuickReply> qrl = new List<QuickReply>();
            qrl.Add(new QuickReply()
            {
                Title = "title 1",
                Payload = "postback#1",
                QuickReplyType = QuickReplyTypes.Text
            });

            qrl.Add(new QuickReply()
            {
                Title = "title 2",
                Payload = "postback#2",
                QuickReplyType = QuickReplyTypes.Text
            });

            string result = client.SendMessageAsync(UserId.ToString(), "Message with quick replies", qrl).Result.Id;
            Trace.WriteLine($"Message Id: {result}");
        }
        
        [TestMethod]
        public void RequestUserLocationTest()
        {
            string result = client.RequestUserLocationAsync(UserId.ToString(), "Your location:").Result.Id;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void RequestUserPhoneTest()
        {
            string result = client.RequestUserPhoneNumberAsync(UserId.ToString(), "Your Phone Number:").Result.Id;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void RequestUserEmailTest()
        {
            string result = client.RequestUserEmailAsync(UserId.ToString(), "Your Email:").Result.Id;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void GenericTemplateTest()
        {
            var buttons = new List<IButton>();
            buttons.Add(new PostbackButton()
            {
                Title = "Button 1",
                Payload = "webhook_value_1"
            });

            buttons.Add(new PostbackButton()
            {
                Title = "Button 2",
                Payload = "webhook_value_2"
            });

            var defaultAction = new ElementAction()
            {
                Url = "https://m.me/",
            };

            var elements = new List<GenericTemplate.Element>();
            elements.Add(new GenericTemplate.Element()
            {
                Title = "Title 1",
                Subtitle = "Subtitle 1",
                ImageUrl = "",
                DefaultAction = defaultAction,
                Buttons = buttons
            });

            elements.Add(new GenericTemplate.Element()
            {
                Title = "Title 2",
                Subtitle = "Subtitle 2",
                ImageUrl = "",
                Buttons = buttons
            });

            var attachment = new Attachment<GenericTemplate>()
            {
                Payload = new GenericTemplate()
                {
                    Elements = elements
                }
            };

            //client.SendAttachment<LogInButton>("asd", new LogInButton());

            string result = client.SendAttachment<GenericTemplate>(UserId.ToString(), attachment).Result.Id;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void ListTemplateTest()
        {
            var buttons = new List<IButton>();
            buttons.Add(new PostbackButton()
            {
                Title = "Button 1",
                Payload = "webhook_value_1"
            });

            var defaultAction = new ElementAction()
            {
                Url = "https://m.me/",

            };

            var elements = new List<ListTemplate.Element>();
            elements.Add(new ListTemplate.Element()
            {
                Title = "Title 1",
                Subtitle = "Subtitle 1",
                ImageUrl = "",
                DefaultAction = defaultAction,
                Buttons = buttons
            });

            elements.Add(new ListTemplate.Element()
            {
                Title = "Title 2",
                Subtitle = "Subtitle 2",
                ImageUrl = "",
                Buttons = buttons
            });

            var attachment = new Attachment<ListTemplate>()
            {
                Payload = new ListTemplate()
                {
                    Elements = elements,
                    Buttons = buttons
                }
            };

            string result = client.SendAttachment<ListTemplate>(UserId.ToString(), attachment).Result.Id;
            Trace.WriteLine($"Message Id: {result}");
        }

        [TestMethod]
        public void ButtonTemplateTest()
        {
            var buttons = new List<IButton>();
            buttons.Add(new UrlButton()
            {
                Title = "Url Button",
                Url = "http://m.me"
            });

            buttons.Add(new PostbackButton()
            {
                Title = "Post Back Button",
                Payload = "webhook_value"
            });

            buttons.Add(new CallButton()
            {
                Title = "Call button",
                Payload = "+123456789"
            });

            var attachment = new Attachment<ButtonTemplate>()
            {
                Payload = new ButtonTemplate()
                {
                    Text = "Button Template Test",
                    Buttons = buttons
                }
            };

            string result = client.SendAttachment<ButtonTemplate>(UserId.ToString(), attachment).Result.Id;
            Trace.WriteLine($"Message Id: {result}");
        }


    }
}
