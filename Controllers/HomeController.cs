using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using TEAM3OIE2S.Models;
using System.Diagnostics;
using System.IO;
using System.Globalization;

using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;

namespace TEAM3OIE2S.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private TestimonialEntities _TestimonialDB = new TestimonialEntities();
        private AuditEntities _AuditDB = new AuditEntities();
        public ActionResult Index()
        {
            ViewData["Message"] = "TEAM3OIE2S";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult Visualize()
        {
            return View();
        }

        public ActionResult Analysis()
        {
            var model = new AnalysisModel();
            model.OriginalIDs = GetValidOriginalIDs();
            model.StudySeries = GetStudySeries(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Analysis(AnalysisModel model)
        {
            model.OriginalIDs = GetValidOriginalIDs();
            if (model.StudySeries== null || model.StudySeries.Count() < 1)
            {

                model.StudySeries = GetStudySeries(model);
            }
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }

        private IEnumerable<SelectListItem> GetStudySeries(AnalysisModel m)
        {

            if (m.originalID > 0)
            {
                string query_string = "SELECT Series.seriesDescription, Study.studyDate FROM Patient INNER JOIN Study ON Patient.patientID = Study.patientID INNER JOIN Series ON Study.studyID = Series.studyID WHERE Patient.originalID = @p1";
                SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
                SqlCommand Command = new SqlCommand(query_string);
                Command.Connection = db;
                Command.Parameters.AddWithValue("@p1", m.originalID.ToString());
                db.Open();
                SqlDataReader reader = Command.ExecuteReader();
                AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Patient", "originalID", "Select");
                AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Study", null, "Select");
                AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Series", null, "Select");
                var ss = new List<SelectListItem>();
                while (reader.Read())
                {
                    ss.Add(new SelectListItem
                    {
                        Value = reader["studyDate"] + " - " + reader["seriesDescription"],
                        Text = reader["studyDate"] + " - " + reader["seriesDescription"]
                    });
                }

                db.Close();
                if (ss.Count < 1)
                {
                    ss.Add(new SelectListItem
                    {
                        Value = "No data",
                        Text = "No data"
                    }
                    );
                }
                return ss;
            }
            return new List<SelectListItem>();
        }
        public ActionResult Contact()
        {
            return View();
        }

        [Authorize]
        public ActionResult Audit()
        {

            AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Audit", null, "Select");
            
            return View(_AuditDB.Audits.ToList());
        }

        public ActionResult Testimonial()
        {
            string userIDforAudit = Request.IsAuthenticated ? Session["userID"].ToString() : null;
            string usernameforAudit = Request.IsAuthenticated ? Session["username"].ToString() : null;
            AuditController.CreateAuditEntry(userIDforAudit, usernameforAudit, "Testimonial", null, "Select");
            return View(_TestimonialDB.TestimonialViews.ToList());
        }


        [HttpPost]
        public ActionResult Testimonial(FormCollection postData)
        {
            if (Request.Form["Search"].ToString().Length > 0)
            {
                string searchVal = Request.Form["Search"];
                if (searchVal.EndsWith(",Search"))
                {
                    searchVal = searchVal.Substring(0, searchVal.LastIndexOf(","));
                }
                searchVal = searchVal.Replace("--", " ").Replace("'", " ");
                var results = from t in _TestimonialDB.TestimonialViews
                              where t.content.ToLower().Contains(searchVal)
                              select t;
                string userIDforAudit = Request.IsAuthenticated ? Session["userID"].ToString() : null;
                string usernameforAudit = Request.IsAuthenticated ? Session["username"].ToString() : null;
                AuditController.CreateAuditEntry(userIDforAudit, usernameforAudit, "Testimonial", "content", "Select");
                return View(results.ToList());
            }

            else if (Request.Form["Add"].ToString().Length > 0)
            {
                string addVal = Request.Form["Add"];
                if (addVal.EndsWith(",Add"))
                {
                    addVal = addVal.Substring(0, addVal.LastIndexOf(","));
                }

                string insert_string = "INSERT INTO Testimonial (content,date,surgeonID) VALUES (@p1,CURRENT_TIMESTAMP,@p2);"; //insert statement
                SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
                SqlCommand command = new SqlCommand(insert_string, db);
                command.Parameters.AddWithValue("@p1", addVal);      
                command.Parameters.AddWithValue("@p2", Convert.ToString(Session["surgeonID"]));
                db.Open();
                command.ExecuteNonQuery();
                db.Close();
                AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Testimonial", null, "Insert");
            }


            return View(_TestimonialDB.TestimonialViews.ToList());
        }

        public ActionResult ZipFiles()
        {
            return View();
        }

        [Authorize]
        public ActionResult NewPatient()
        {
            var model = new Patient();
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult NewPatient(Patient pt)
        {
            if (ModelState.IsValid)
            {

                string insert_string = "INSERT INTO Patient (originalID,sex,age,entryDate,surgeonID) VALUES (@p1,@p2,@p3,CURRENT_TIMESTAMP,@p4);"; //insert statement
                SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
                SqlCommand command = new SqlCommand(insert_string, db);
                command.Parameters.AddWithValue("@p1", pt.originalID);
                command.Parameters.AddWithValue("@p2", pt.sex);
                command.Parameters.AddWithValue("@p3", pt.age);
                command.Parameters.AddWithValue("@p4", Session["surgeonID"].ToString());
                db.Open();
                command.ExecuteNonQuery();
                db.Close();
                AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Patient", null, "Insert");
            }
            return View();
        }

        private void Alert(string s)
        {
            Response.Write("<script>alert('"+s+"');</script>");
        }


        private Study GetStudyInfo(List<EvilDICOM.Core.Interfaces.IDICOMElement> elements, int start)
        {
            Study new_study = new Study();
            new_study.studyDate = new DateTime(1, 1, 1, 0, 0, 0);
            for (int i = start; i < elements.Count; i++)
            {
                var currElement = elements[i];
                var currTag = currElement.Tag;
                if (currTag.Equals(TagHelper.DIRECTORY_RECORD_TYPE))
                {
                    break;
                }
                else if (currTag.Equals(TagHelper.STUDY_DATE))
                {
                    DateTime studydate = DateTime.Parse(currElement.DData.ToString());

                    new_study.studyDate = new_study.studyDate.AddYears(studydate.Year-1);
                    new_study.studyDate = new_study.studyDate.AddMonths(studydate.Month - 1);
                    new_study.studyDate = new_study.studyDate.AddDays(studydate.Day - 1);
                }
                else if (currTag.Equals(TagHelper.STUDY_TIME))
                {
                    DateTime studytime = DateTime.Parse(currElement.DData.ToString());

                    new_study.studyDate = new_study.studyDate.AddHours(studytime.Hour);
                    new_study.studyDate = new_study.studyDate.AddMinutes(studytime.Minute);
                    new_study.studyDate = new_study.studyDate.AddSeconds(studytime.Second);
                }
                else if (currTag.Equals(TagHelper.STUDY_DESCRIPTION))
                {
                    new_study.studyDescription = currElement.DData.ToString();
                }
                else if (currTag.Equals(TagHelper.STUDY_ID))
                {
                    new_study.originalStudyID = Convert.ToInt32(currElement.DData.ToString());
                }
            }

            return new_study;
        }

        private Series GetSeriesInfo(List<EvilDICOM.Core.Interfaces.IDICOMElement> elements, int start)
        {
            Series new_series = new Series();
            new_series.seriesDate = new DateTime(1, 1, 1, 0, 0, 0);
            new_series.totalNumberofSlices = 0;
            for (int i = start; i < elements.Count; i++)
            {
                var currElement = elements[i];
                var currTag = currElement.Tag;
                if (currTag.Equals(TagHelper.DIRECTORY_RECORD_TYPE))
                {
                    if (currElement.DData.ToString().Equals("IMAGE"))
                    {
                        new_series.totalNumberofSlices += 1;   
                    }
                    else
                    {
                        break;
                    }
                }
                else if (currTag.Equals(TagHelper.SERIES_DATE))
                {
                    DateTime seriesdate = DateTime.Parse(currElement.DData.ToString());

                    new_series.seriesDate = new_series.seriesDate.AddYears(seriesdate.Year-1);
                    new_series.seriesDate = new_series.seriesDate.AddMonths(seriesdate.Month - 1);
                    new_series.seriesDate = new_series.seriesDate.AddDays(seriesdate.Day - 1);
                }
                else if (currTag.Equals(TagHelper.SERIES_TIME))
                {
                    DateTime seriestime = DateTime.Parse(currElement.DData.ToString());

                    new_series.seriesDate = new_series.seriesDate.AddHours(seriestime.Hour);
                    new_series.seriesDate = new_series.seriesDate.AddMinutes(seriestime.Minute);
                    new_series.seriesDate = new_series.seriesDate.AddSeconds(seriestime.Second);
                }
                else if (currTag.Equals(TagHelper.SERIES_DESCRIPTION))
                {
                    new_series.seriesDescription = currElement.DData.ToString();
                }
                else if (currTag.Equals(TagHelper.SERIES_NUMBER))
                {
                    new_series.originalSeriesStudyID = Convert.ToInt32(currElement.DData.ToString());
                }
            }

            return new_series;
        }

        private Image GetImageInfo(List<EvilDICOM.Core.Interfaces.IDICOMElement> elements, int start)
        {
            Image new_image = new Image();
            for (int i = start; i < elements.Count; i++)
            {
                var currElement = elements[i];
                var currTag = currElement.Tag;
                if (currTag.Equals(TagHelper.DIRECTORY_RECORD_TYPE))
                {
                    break;
                }
                else if (currTag.Equals(TagHelper.INSTANCE_NUMBER))
                {
                    new_image.imageOrder = Convert.ToInt32(currElement.DData.ToString());
                }
                else if (currTag.Equals(TagHelper.SLICE_THICKNESS))
                {
                    new_image.sliceThickness = Convert.ToInt32(currElement.DData.ToString());
                }
            }

            return new_image;
        }


        private int GetPatientIDFromOriginalID(int originalID)
        {
            string query_string = "SELECT patientID FROM Patient WHERE originalID = @p1 AND surgeonID = @p2;";
            SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
            SqlCommand Command = new SqlCommand(query_string, db);
            Command.Parameters.AddWithValue("@p1", originalID);
            Command.Parameters.AddWithValue("@p2", Session["surgeonID"].ToString());
            db.Open();
            SqlDataReader reader = Command.ExecuteReader();
            reader.Read();
            int patientID = Convert.ToInt32(reader["patientID"].ToString());
            db.Close();
            AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Patient", null, "Select");
            return patientID;
        }

        private Study InsertStudy(Study s)
        {
            if (s.originalStudyID == null)
            {
                s.originalStudyID = -1;
            }
            if (s.studyDescription == null)
            {
                s.studyDescription = "";
            }
            if (s.studyDate == null || s.studyDate.Year < 1900 || s.studyDate.Year > 9999)
            {
                s.studyDate = new DateTime(1900, 1, 1, 0, 0, 0);
            }
            

            string query_string = "INSERT INTO Study(originalStudyID, studyDescription, studyDate, delay, patientID) VALUES (@p1, @p2, @p3, @p4, @p5) SET @ID = SCOPE_IDENTITY();";
            SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
            SqlCommand Command = new SqlCommand(query_string, db);
            Command.Parameters.AddWithValue("@p1", s.originalStudyID);
            Command.Parameters.AddWithValue("@p2", s.studyDescription);
            Command.Parameters.AddWithValue("@p3", s.studyDate);
            Command.Parameters.AddWithValue("@p4", 0);
            Command.Parameters.AddWithValue("@p5", s.patientID);
            Command.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            db.Open();
            Command.ExecuteNonQuery();
            AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Study", null, "Insert");
            s.studyID = Convert.ToInt32(Command.Parameters["@ID"].Value.ToString());
            db.Close();
            return s;
        }

        private Series InsertSeries(Series s)
        {
            if (s.originalSeriesStudyID == null)
            {
                s.originalSeriesStudyID = -1;
            }
            if (s.seriesDescription == null)
            {
                s.seriesDescription = "";
            }
            if (s.seriesDate == null || s.seriesDate.Year < 1900 || s.seriesDate.Year > 9999)
            {
                s.seriesDate = new DateTime(1900, 1, 1, 0, 0, 0);
            }
            if (s.totalNumberofSlices == null)
            {
                s.totalNumberofSlices = 0;
            }


            string query_string = "INSERT INTO Series(originalSeriesStudyID, seriesDescription, seriesDate, totalNumberofSlices, studyID) VALUES (@p1, @p2, @p3, @p4, @p5) SET @ID = SCOPE_IDENTITY();";
            SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
            SqlCommand Command = new SqlCommand(query_string, db);
            Command.Parameters.AddWithValue("@p1", s.originalSeriesStudyID);
            Command.Parameters.AddWithValue("@p2", s.seriesDescription);
            Command.Parameters.AddWithValue("@p3", s.seriesDate);
            Command.Parameters.AddWithValue("@p4", s.totalNumberofSlices);
            Command.Parameters.AddWithValue("@p5", s.studyID);
            Command.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            db.Open();
            Command.ExecuteNonQuery();
            AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Series", null, "Insert");
            s.seriesID = Convert.ToInt32(Command.Parameters["@ID"].Value.ToString());
            db.Close();
            return s;
        }

        private Image InsertImage(Image i)
        {
            string query_string = "INSERT INTO Image(imageOrder, imageFilename, sliceThickness, seriesID) VALUES (@p1, @p2, @p3, @p4) SET @ID = SCOPE_IDENTITY();";
            SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
            SqlCommand Command = new SqlCommand(query_string, db);
            Command.Parameters.AddWithValue("@p1", i.imageOrder);
            Command.Parameters.AddWithValue("@p2", i.imageFilename);
            if (i.sliceThickness != null)
            {
                Command.Parameters.AddWithValue("@p3", i.sliceThickness);
            }
            else
            {
                Command.Parameters.AddWithValue("@p3", DBNull.Value);
            }
            Command.Parameters.AddWithValue("@p4", i.seriesID);
            Command.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            db.Open();
            Command.ExecuteNonQuery();
            AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Image", null, "Insert");
            i.imageID = Convert.ToInt32(Command.Parameters["@ID"].Value.ToString());
            db.Close();
            return i;
        }

        private void ReadDICOMDIR(UploadModel um, string path, string filename)
        {
            var dBytes = System.IO.File.ReadAllBytes(filename);
            var dcm = DICOMObject.Read(dBytes);

            var pt_originalID = dcm.FindFirst(TagHelper.PATIENT_ID).DData.ToString();
            if (!pt_originalID.Equals(um.originalID.ToString()))
            {
             //   Alert("Patient ID in DICOM file (" + pt_originalID + ") does not match the selected patient ID (" + um.originalID.ToString() + ")");
            }

            var allStudies = dcm.FindAll(TagHelper.STUDY_ID);
            var allElements = dcm.AllElements;
            Study currentStudy = null;
            Series currentSeries = null;
            for (int i = 0; i < allElements.Count; i++)
            {
                var currElement = allElements[i];
                if (currElement.Tag.Equals(TagHelper.DIRECTORY_RECORD_TYPE))
                {
                    if (currElement.DData.ToString().Equals("STUDY"))
                    {
                        Study nextStudy = GetStudyInfo(allElements, i+1);
                        nextStudy.patientID = GetPatientIDFromOriginalID(um.originalID);
                        currentStudy = InsertStudy(nextStudy);
                    }
                    else if (currElement.DData.ToString().Equals("SERIES"))
                    {
                        Series nextSeries = GetSeriesInfo(allElements, i+1);
                        nextSeries.studyID = currentStudy.studyID;
                        currentSeries = InsertSeries(nextSeries);
                    }
                    else if (currElement.DData.ToString().Equals("IMAGE"))
                    {
                        Image nextImage = GetImageInfo(allElements, i+1);
                        nextImage.seriesID = currentSeries.seriesID;
                        nextImage.imageFilename = String.Join("/", new string[] 
                            {"{IMAGE_DIR}", currentStudy.patientID.ToString(), currentStudy.studyID.ToString(), currentSeries.seriesID.ToString(), nextImage.imageOrder.ToString()});
                        InsertImage(nextImage);
                    }
                }

            }
            //Alert(dcm.ToString());
        }

        public ActionResult Audits()
        {
            return View();
        }
        private void Unzip(UploadModel um, Stream zipStream)
        {
            String tmpFileName = Path.GetTempPath() + "\\" + Path.GetRandomFileName();
            while (System.IO.File.Exists(tmpFileName))
            {
                tmpFileName = Path.GetTempPath() + "\\" + Path.GetRandomFileName();
            }
            ZipInputStream zipInputStream = new ZipInputStream(zipStream);
            ZipEntry entry = zipInputStream.GetNextEntry();
            while (entry != null)
            {
                
                String entryName = entry.Name;
                if (!entryName.EndsWith("DICOMDIR"))
                {
                    entry = zipInputStream.GetNextEntry();
                    continue;
                }
                //Alert(entryName);
                
                if (entry.IsFile)
                {
                    if (System.IO.File.Exists(tmpFileName))
                    {
                        System.IO.File.Delete(tmpFileName);
                    }
                    long bufSize = (entry.Size + 4096) - (entry.Size % 4096);
                    byte[] buffer = new byte[bufSize];
                    using (FileStream streamOut = System.IO.File.Create(tmpFileName))
                    {
                        StreamUtils.Copy(zipInputStream, streamOut, buffer);
                    }
                    if (System.IO.Path.GetFileName(entryName).Equals("DICOMDIR"))
                    {
                        try
                        {
                            ReadDICOMDIR(um, System.IO.Path.GetPathRoot(entryName), tmpFileName);
                        }
                        catch (Exception e)
                        {

                        }
                    }

                }
               
                entry = zipInputStream.GetNextEntry();
            }
            System.IO.File.Delete(tmpFileName);

        }


        public ActionResult Upload()
        {
            var model = new UploadModel();
            model.OriginalIDs = GetValidOriginalIDs();
            model.EndographBrands = GetEndographBrands();
            return View(model);
        }

        [HttpPost]
        public ActionResult Upload(UploadModel model, HttpPostedFileBase file)
        {
            model.OriginalIDs = GetValidOriginalIDs();
            model.EndographBrands = GetEndographBrands();

            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    String extension = Path.GetExtension(file.FileName);
                    if (extension.Equals(".zip"))
                    {
                        Unzip(model, file.InputStream);
                    }
                    else if (extension.Equals(".xlsx"))
                    {

                    }
                    else
                    {
                        Response.Write("<script>alert('File type not supported. Use zip or xlsx.');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('No file selected.');</script>");
                }
            }
            return View(model);
        }

        private IEnumerable<SelectListItem> GetValidOriginalIDs()
        {
            string query_string = "SELECT originalID FROM Patient WHERE surgeonID = @p1";
            SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
            SqlCommand Command = new SqlCommand(query_string);
            Command.Connection = db;
            Command.Parameters.AddWithValue("@p1", Session["surgeonID"].ToString());
            db.Open();
            SqlDataReader reader = Command.ExecuteReader();
            AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Patient", "originalID", "Select");
            var userTypes = new List<SelectListItem>();
            while (reader.Read())
            {
                userTypes.Add(new SelectListItem
                {
                    Value = reader["originalID"].ToString(),
                    Text = reader["originalID"].ToString()
                });
            }

            db.Close();
            return userTypes;
        }

        private IEnumerable<SelectListItem> GetEndographBrands()
        {
            string query_string = "SELECT * FROM Brand";
            SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
            SqlCommand Command = new SqlCommand(query_string);
            Command.Connection = db;
            db.Open();
            SqlDataReader reader = Command.ExecuteReader();
            AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Brand", null, "Select");
            var userTypes = new List<SelectListItem>();
            while (reader.Read())
            {
                userTypes.Add(new SelectListItem
                {
                    Value = reader["brandID"].ToString(),
                    Text = reader["brandName"].ToString()
                });
            }

            db.Close();
            return userTypes;
        }
    }

}
