using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using webdevfinal.ViewModels;
using Umbraco.Core.Models;



namespace webdevfinal.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: Default
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());

        }
        //Comment
        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            if (!ModelState.IsValid) { return CurrentUmbracoPage(); }
            // Read data from model and send
            //
            IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "ContactForm");
            // assign values
            comment.SetValue("name", model.Name);
            comment.SetValue("email", model.Email);
            comment.SetValue("subject", model.Subject);
            comment.SetValue("message", model.Message);
            // save to Umbraco
            Services.ContentService.Save(comment);
            // Services.ContentService.SaveAndPublish(comment);

            TempData["success"] = true;

            //RedirectToCurrentUmbracoPage: Do not keep the POST - data that was sent to the controller
            //Passes TempData from the controller
            //Use RedirectToCurrentUmbracoPage()if the post was successful
            //Prevents users from re-posting data
            return RedirectToCurrentUmbracoPage();
        }



    }
}