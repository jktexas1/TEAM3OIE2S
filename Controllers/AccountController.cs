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
using TEAM3OIE2S.Controllers;

namespace TEAM3OIE2S.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        private Surgeon surgeon = new Surgeon();

        private void Alert(string s)
        {
            Response.Write("<script>alert('" + s + "');</script>");
        }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string query_string = "SELECT * FROM Users WHERE Username = @parameter0 AND Password = @parameter1";
                SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
                SqlCommand Command = new SqlCommand(query_string);

                Command.Connection = db;
                Command.Parameters.AddWithValue("@parameter0", model.UserName);
                Command.Parameters.AddWithValue("@parameter1", model.Password);

                db.Open();
                SqlDataReader reader = Command.ExecuteReader();
                AuditController.CreateAuditEntry(null, model.UserName, "Users", null, "Select");

                if (reader.HasRows) //If login credentials return results.
                {
                    reader.Read();
                    Session["username"] = model.UserName;
                    Session["userID"] = reader["userID"].ToString();
                    Session["typeID"] = reader["typeID"].ToString();
                    CheckAndSetSurgeonID();

                    FormsService.SignIn(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        db.Close();
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        db.Close();
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    db.Close();
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            var model = new RegisterModel();
            model.UserTypes = GetAllUserTypes();
            model.Institutions = GetAllInstitutions();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            model.UserTypes = GetAllUserTypes();
            model.Institutions = GetAllInstitutions();

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                string query_string = "SELECT * FROM Users WHERE Username = @parameter0 OR Email = @parameter1";
                SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
                SqlCommand Command = new SqlCommand(query_string);

                Command.Connection = db;
                Command.Parameters.AddWithValue("@parameter0", model.UserName);
                Command.Parameters.AddWithValue("@parameter1", model.Email);

                db.Open();
                SqlDataReader reader = Command.ExecuteReader();
                AuditController.CreateAuditEntry(null, model.UserName, "Users", null, "Select");

                if (reader.HasRows) //User already exists.
                {
                    Alert("Username and/or Email Address already in use.");
                    db.Close();
                    reader.Close();
                }
                else //Work on registering new user.
                {

                    if(model.InstitutionID == null) //If institution does not exist add into database and get reader results after.
                    {
                        query_string = "Insert INTO Institution (institutionName, institutionLocation) VALUES (@p0, @p1) SET @ID = SCOPE_IDENTITY();";
                        Command.CommandText = query_string;
                        Command.Parameters.Clear();
                        Command.Parameters.AddWithValue("@p0", model.InstitutionName);
                        Command.Parameters.AddWithValue("@p1", model.InstitutionLocation);
                        Command.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                        reader.Close();
                        Command.ExecuteNonQuery();
                        AuditController.CreateAuditEntry(null, model.UserName, "Institution", null, "Insert");
                        model.InstitutionID = Command.Parameters["@ID"].Value.ToString();

                    }

                    string institutionID = model.InstitutionID;


                    //Get user type ID
                    string userTypeID = model.UserType;
                    
                    //Add user to database.
                    string add_user_insert_string = "INSERT INTO Users(firstname,lastname,username,password,email,typeID) VALUES (@p0,@p1,@p2,@p3,@p4,@p5) SET @ID = SCOPE_IDENTITY();";
                    Command.CommandText = add_user_insert_string;
                    Command.Parameters.Clear();
                    Command.Parameters.AddWithValue("@p0", model.FirstName);
                    Command.Parameters.AddWithValue("@p1", model.LastName);
                    Command.Parameters.AddWithValue("@p2", model.UserName);
                    Command.Parameters.AddWithValue("@p3", model.Password);
                    Command.Parameters.AddWithValue("@p4", model.Email);
                    Command.Parameters.AddWithValue("@p5", userTypeID);
                    Command.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    reader.Close();
                    Command.ExecuteNonQuery();
                    string userID = Command.Parameters["@ID"].Value.ToString();
                    AuditController.CreateAuditEntry(userID, model.UserName, "Users", null, "Insert");

                    //if a Surgeon, add to Surgeon Table as well
                    if (Convert.ToInt32(userTypeID) == 3)
                    {
                        query_string = "Insert INTO Surgeon (firstname,lastname,username,password,email,institutionID) VALUES (@p0,@p1,@p2,@p3,@p4,@p5);";
                        Command.CommandText = query_string;
                        Command.Parameters.Clear();
                        Command.Parameters.AddWithValue("@p0", model.FirstName);
                        Command.Parameters.AddWithValue("@p1", model.LastName);
                        Command.Parameters.AddWithValue("@p2", model.UserName);
                        Command.Parameters.AddWithValue("@p3", model.Password);
                        Command.Parameters.AddWithValue("@p4", model.Email);
                        Command.Parameters.AddWithValue("@p5", institutionID);
                        reader.Close();
                        Command.ExecuteNonQuery();
                        AuditController.CreateAuditEntry(userID, model.UserName, "Surgeon", null, "Insert");
                    }

                    db.Close();

                    //Sign in.

                    Session["username"] = model.UserName;
                    Session["userID"] = userID;
                    Session["typeID"] = userTypeID;
                    CheckAndSetSurgeonID();

                    FormsService.SignIn(model.UserName, false /* createPersistentCookie */ );
                    return RedirectToAction("Index", "Home");   
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        private void CheckAndSetSurgeonID()
        {
            if (Session["username"] != null)
            {
                string query_string = "SELECT * FROM Surgeon WHERE Username = @parameter0";
                SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
                SqlCommand Command = new SqlCommand(query_string);

                Command.Connection = db;
                Command.Parameters.AddWithValue("@parameter0", Session["username"].ToString());

                db.Open();
                SqlDataReader reader = Command.ExecuteReader();
                AuditController.CreateAuditEntry(Session["userID"].ToString(), Session["username"].ToString(), "Surgeon", "username", "Select");
                if (reader.HasRows)
                {
                    reader.Read();
                    Session["surgeonID"] = reader["surgeonID"];
                }
                db.Close();
            }
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private IEnumerable<SelectListItem> GetAllUserTypes()
        {
            string query_string = "SELECT * FROM UserType";
            SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
            SqlCommand Command = new SqlCommand(query_string);
            Command.Connection = db;
            db.Open();
            SqlDataReader reader = Command.ExecuteReader();
            AuditController.CreateAuditEntry(null, null, "UserType", null, "Select");
            var userTypes = new List<SelectListItem>();
            while (reader.Read())
            {
                userTypes.Add(new SelectListItem
                {
                    Value = reader["typeID"].ToString(),
                    Text = reader["typeName"].ToString()
                });
            }

            db.Close();
            return userTypes;
        }

        private IEnumerable<SelectListItem> GetAllInstitutions()
        {
            string query_string = "SELECT * FROM Institution";
            SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
            SqlCommand Command = new SqlCommand(query_string);
            Command.Connection = db;
            db.Open();
            SqlDataReader reader = Command.ExecuteReader();
            AuditController.CreateAuditEntry(null, null, "Institution", null, "Select");
            var institutions = new List<SelectListItem>();
            while (reader.Read())
            {
                institutions.Add(new SelectListItem
                {
                    Value = reader["institutionID"].ToString(),
                    Text = reader["institutionName"].ToString() + " (" + reader["institutionLocation"].ToString() + ")"
                });
            }

            db.Close();
            return institutions;
        }

    }
}
