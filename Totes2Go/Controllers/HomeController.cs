﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Totes2Go.Models;
using System.Net;
using SendGrid;
using System.Net.Mail;

namespace Totes2Go.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult Contact(EmailContact model)
        {
            if (ModelState.IsValid)
            {
                var body = "Name: {0} <br />  Email: {1} <br /> Phone: {2} <br />  Message: {3} <br /> Contact Type Phone: {4} <br /> Contact Type Email: {5}";
                var message = new SendGridMessage();
                message.AddTo("chrisg.universalad@gmail.com");  // replace with valid value 
                message.From = new MailAddress("chrisg.universalad@gmail.com");  // replace with valid value
                message.Subject = "Totes 2 Go Contact";
                message.Html = string.Format(body, model.Name, model.Email, model.Phone, model.Message, model.PhoneCheck, model.EmailCheck);
                //Azure credentials
                var username = "azure_8b8a64638c6bdacad86023f15c2e402b@azure.com";
                var pswd = "Cg090482?";

                // variable to store azure credentials
                var credentials = new NetworkCredential(username, pswd);
                // Create an Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email, which returns an awaitable task.
                transportWeb.DeliverAsync(message);

                ViewBag.Message = "Message Sent";
                ModelState.Clear(); //clears form when page reload
                return View("Index");

            }
            return View(model);
        }
    }
}