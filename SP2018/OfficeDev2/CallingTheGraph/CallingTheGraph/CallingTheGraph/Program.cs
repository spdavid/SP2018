using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace CallingTheGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = GraphHelper.GetGraphClient();

            var users = client.Users.Request()
                //.Filter("givenName eq 'Test'")
                .GetAsync().Result;


            foreach (var user in users)
            {
                Console.WriteLine(user.DisplayName); 
            }

            var anton = users.Where(u => u.DisplayName == "Anton Brodin").FirstOrDefault();


            Console.WriteLine("press enter");
            Console.ReadLine();

            var message = new Message()
            {
                Subject = "From Graph app",
                Body = new ItemBody()
                {
                    Content = "this is the body"
                },
                ToRecipients = new List<Recipient>()
                {
                    new Recipient()
                    {
                        EmailAddress = new EmailAddress () {Address ="David@zalosolutions.com", Name = "David O" }
                    }
                }
            };
            client.Users[anton.Id].SendMail(message).Request().PostAsync().Wait();



            //var firstName = "stevey";
            //var lastName = "wonder";

            //User u = new User()
            //{

            //    GivenName = firstName,
            //    Surname = lastName,

            //    DisplayName = firstName + " " + lastName,
            //    UserPrincipalName = firstName + "." + lastName + "@folkis2018.onmicrosoft.com",
            //    PasswordProfile = new PasswordProfile()
            //    {
            //        Password = "Vacation12!",
            //        ForceChangePasswordNextSignIn = false
            //    },
            //    MailNickname = firstName + "." + lastName,

            //    AccountEnabled = true

            //};


            //var newUser = client.Users.Request().AddAsync(u).Result;


        }
    }
}
