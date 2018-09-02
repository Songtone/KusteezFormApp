﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KusteezFormApp.Models;
using KusteezFormApp.DataReader;
using MySql.Data.MySqlClient;

namespace KusteezFormApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()//the main page when it loads it will have all the page refreshed and the dropdown list filled properly.
        {
            FormInsert infoReader = new FormInsert();
            List<SizeReference> sizeRef = infoReader.GetSizeReferences();
            List<ClothesColorReference> clothesColorRef = infoReader.GetClothesColorReference();
            List<PrintColorReference> printColorRef = infoReader.GetPrintColorReference();
            List<CustomLaceColorReference> laceColorRef = infoReader.GetCustomLaceColorReference();

            FormDetails formDetails = new FormDetails();

            formDetails.sizes = sizeRef;
            formDetails.clothesColor = clothesColorRef;
            formDetails.printColor = printColorRef;
            formDetails.laceColor = laceColorRef;

            return View("Index",formDetails);
        }

        [HttpPost]
        public ActionResult Submit(FormDetails fd)//submitting the information to the database
        {

            FormInsert infoReader = new FormInsert();
            List<SizeReference> sizeRef = infoReader.GetSizeReferences();
            List<ClothesColorReference> clothesColorRef = infoReader.GetClothesColorReference();
            List<PrintColorReference> printColorRef = infoReader.GetPrintColorReference();
            List<CustomLaceColorReference> laceColorRef = infoReader.GetCustomLaceColorReference();

            FormDetails formDetails = new FormDetails();

            formDetails.sizes = sizeRef;
            formDetails.clothesColor = clothesColorRef;
            formDetails.printColor = printColorRef;
            formDetails.laceColor = laceColorRef;
            string test = "";

            test = fd.clothingType;
            test = formDetails.clothingType;

            FormInsert findEstimate = new FormInsert();
            //int Result = findEstimate.GetEstimatedCost(fd);

            int Result = findEstimate.Insert(fd);
            formDetails.estimatedCost = fd.estimatedCost;

            //validation starts here
            if (fd.clothingType == "")//makes sure they choose an option
            {
                string x = "YAY";
                ModelState.AddModelError("clothingType", "Choose a clothing type");
            }

            if (fd.clothingType != "PO")//if the choice is not Print Only they must choose size and color
            {
                if (fd.sizeCode == "00")
                {
                    ModelState.AddModelError("sizeCode", "Choose a size");
                }
                if(fd.clothesColorCode == "00")
                {
                    ModelState.AddModelError("clothesColorCode", "Choose a clothes color");
                }
                
            }

            if (fd.printColorCode == "00")//if they choose print only, they must choose a print color
            {
                ModelState.AddModelError("printColorCode", "Choose a print color");
            }
            else
            {
                if(fd.fontType == "")
                {
                    ModelState.AddModelError("fontType", "Choose a font");
                }
            }

            if(fd.ticketNumber == null)//makes sure the ticket section is filled out
            {
                ModelState.AddModelError("ticketNumber", "Enter a ticket number");
            }

            if (fd.confirmationButton)
            {
                if (ModelState.IsValid)
                {
                    //FormInsert finsert = new FormInsert();
                    //int Result1 = finsert.Insert(fd);
                    return RedirectToAction("Index", fd);
                }
                else
                {
                    return View("Index", formDetails);
                }
            }
            return View("Index", formDetails);

        }
        
    }
}
