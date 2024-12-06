/*
 * 
 * Name: Daniel Antonio
 * Date: 12/5/2024
 * Stephen F. Austin State University Honors Project
 * Summary: This project is a "Prototype" Calendar Event-Planner application with the purpose of mimicking an application that can read a Course Syllabus
 * and automatically present the user with useful information about the choosen course. Usally Syllabi are written in a DOCX File, which consists of a ZIP package of XML Files.
 * To Mimick this, A Custom XML File is created and used. The courses are provided within the project
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseInformation
{
    public class Assignment
    {
        private DateTime dueDate;
        private string title;
        private string description;

        public Assignment() { }
        public Assignment(string title, string dueDate, string description)
        {
            this.title = title;
            this.dueDate = DateTime.Parse(dueDate);
            this.description = description;
        }
        public void setTitle(string title)
        {
            this.title = title;
        }
        public void setDate(DateTime date)
        {
            this.dueDate = date;
        }
        public void setDescription(string description)
        {
            this.description = description;
        }
        public string getTitle()
        {
            return this.title;
        }
        public DateTime getDate()
        {
            return this.dueDate;
        }
        public string getDescription()
        {
            return this.description;
        }
        public override string ToString()
        {
            return this.title;
        }
    }
}
