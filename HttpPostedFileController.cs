 // CUstom VBM Crud operation.



        public ActionResult MyList()
        {
            // List<property> images = new List<property>();   // must have to choose "List" Layout of "Add View."
            //List<property> images = db.properties.ToList();
            //return View(images);
            // or

            List<property> images = new List<property>();  // must have to choose "List" Layout of "Add View."
            images = db.properties.ToList();
            return View(images);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(property pro)
        {
            try
            {
                if (pro.Gallery_ImageUpload != null || pro.Floor_ImageUpload != null || pro.Document_ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.Gallery_ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.Gallery_ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    pro.prop_gallery_path = "~/ProjectFIle/Gallery/" + fileName;
                    pro.Gallery_ImageUpload.SaveAs(Path.Combine(Server.MapPath("/ProjectFIle/Gallery/"), fileName));

                    string fileName_two = Path.GetFileNameWithoutExtension(pro.Floor_ImageUpload.FileName);
                    string extension_two = Path.GetExtension(pro.Floor_ImageUpload.FileName);
                    fileName_two = fileName_two + DateTime.Now.ToString("yymmssfff") + extension;
                    pro.prop_floor_path = "~/ProjectFIle/Gallery/" + fileName_two;
                    pro.Floor_ImageUpload.SaveAs(Path.Combine(Server.MapPath("/ProjectFIle/FloorDigram/"), fileName_two));

                    string fileName_three = Path.GetFileNameWithoutExtension(pro.Document_ImageUpload.FileName);
                    string extension_three = Path.GetExtension(pro.Document_ImageUpload.FileName);
                    fileName_three = fileName_three + DateTime.Now.ToString("yymmssfff") + extension;
                    pro.prop_document = "~/ProjectFIle/Document/" + fileName_three;
                    pro.Document_ImageUpload.SaveAs(Path.Combine(Server.MapPath("/ProjectFIle/Document/"), fileName_three));
                }
                using (RealEstateDBEntities1 db = new RealEstateDBEntities1())
                {
                    if (pro.prop_id == 0)
                    {
                        db.properties.Add(pro);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(pro).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
                //return Json(new { success = true, message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
                return View();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Details(int? id)
        {
            List<property> PropData = new List<property>();  // must have to choose "List" Layout of "Add View."
            property myPropData = db.properties.Find(id);
            return View(myPropData);
        }

        // https://localhost:44387/DashBoard/MyListings
        //https://localhost:44387/DashBoard/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                property myprop = db.properties.Find(id);
                return View(myprop);
            }
            //Session["Gallery_ImageUpload"] = myprop.Gallery_ImageUpload;
            //Session["Floor_ImageUpload"] = myprop.Floor_ImageUpload;
            //Session["Document_ImageUpload"] = myprop.Document_ImageUpload;       

        }

        [HttpPost]
        public ActionResult Edit(property pro)
        {
            try                                  //@Html.HiddenFor(model => model.prop_id)
            {
                if (pro.Gallery_ImageUpload != null || pro.Floor_ImageUpload != null || pro.Document_ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.Gallery_ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.Gallery_ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    pro.prop_gallery_path = "~/ProjectFIle/Gallery/" + fileName;
                    pro.Gallery_ImageUpload.SaveAs(Path.Combine(Server.MapPath("/ProjectFIle/Gallery/"), fileName));

                    if (pro.Floor_ImageUpload != null)
                    {
                        string fileName_two = Path.GetFileNameWithoutExtension(pro.Floor_ImageUpload.FileName);
                        string extension_two = Path.GetExtension(pro.Floor_ImageUpload.FileName);
                        fileName_two = fileName_two + DateTime.Now.ToString("yymmssfff") + extension;
                        pro.prop_floor_path = "~/ProjectFIle/Gallery/" + fileName_two;
                        pro.Floor_ImageUpload.SaveAs(Path.Combine(Server.MapPath("/ProjectFIle/FloorDigram/"), fileName_two));
                    }

                    if (pro.Document_ImageUpload != null)
                    {

                        string fileName_three = Path.GetFileNameWithoutExtension(pro.Document_ImageUpload.FileName);
                        string extension_three = Path.GetExtension(pro.Document_ImageUpload.FileName);
                        fileName_three = fileName_three + DateTime.Now.ToString("yymmssfff") + extension;
                        pro.prop_document = "~/ProjectFIle/Document/" + fileName_three;
                        pro.Document_ImageUpload.SaveAs(Path.Combine(Server.MapPath("/ProjectFIle/Document/"), fileName_three));
                    }
                }
                using (RealEstateDBEntities1 db = new RealEstateDBEntities1())
                {
                    if (pro.prop_id == 0)
                    {
                        db.properties.Add(pro);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(pro).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("dashboard");
                    }

                }
                return View();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            property pro = db.properties.Find(id);
            db.properties.Remove(pro);
            db.SaveChanges();
            return View("Dashboard");
        }
    }
}
