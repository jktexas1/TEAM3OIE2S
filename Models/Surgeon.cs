using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEAM3OIE2S.Models
{
    public class Surgeon
    {
        private int surgeonID;
        private string firstname;
        private string username;
        private string lastname;
        private string password;
        private string email;
        private int institutionID;
        public void setSurgeonID(int s)
        {
            surgeonID = s;
        }
        public int getSurgeonID()
        {
            return surgeonID;
        }
        public void setFirstName(string f)
        {
            firstname = f;
        }
        public string getFirstName()
        {
            return firstname;
        }
        public void setLastName(string l)
        {
            lastname = l;
        }
        public string getLastName()
        {
            return lastname;
        }
        public void setUserName(string u)
        {
            username = u;
        }
        public string getUserName()
        {
            return username;
        }
        public void setPassword(string p)
        {
            password = p;
        }
        public string getPassword()
        {
            return password;
        }
        public void setEmail(string e)
        {
            email = e;
        }
        public string getEmail()
        {
            return email;
        }
        public void setInstitutionID(int i)
        {
            institutionID = i;
        }
        public int getInstitutionID()
        {
            return institutionID;
        }
    }
}