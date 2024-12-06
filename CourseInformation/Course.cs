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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace CourseInformation
{
    public class Course
    {
        //Path to Class XML File
        private string file_path;

        //Class name = name + code
        private string class_Name;

        //professor or lecturer name
        private string professor_Name;

        //Contact information
        private string professor_Email;
        private string professor_Phone_Number;

        //Office and classroom hours
        private string office_Hours;
        private string class_Time;

        //List of Assignments
        private ListBox Assigments;

        //Node Management Objects
        private XmlDocument document = new XmlDocument();

        public Course(string file_path)
        {
            if (File.Exists(file_path))
            { this.file_path = file_path; document.Load(file_path); }
            else
                this.file_path = null;
        }
        public string getClassName()
        {
            if (class_Name == null)
            {
                XmlNode node = document.SelectSingleNode("//ClassName");
                class_Name += " " + node.InnerText;

                node = document.SelectSingleNode("//ClassCode");
                class_Name += " " + node.InnerText;
            }

            return class_Name;
        }

        public string getProfessorName()
        {
            if (professor_Name == null)
            {
                XmlNode node = document.SelectSingleNode("//Name");
                professor_Name = " " + node.InnerText;
            }
            return professor_Name;
        }
        public string getEmail()
        {
            if(professor_Email == null)
            {
                XmlNode node = document.SelectSingleNode("//Email");
                professor_Email += " " + node.InnerText;
            }
            return professor_Email;
        }
        public string getPhone()
        {
            if (professor_Phone_Number == null)
            {
                XmlNode node = document.SelectSingleNode("//PhoneNumber");
                professor_Phone_Number += " " + node.InnerText;
            }
            return professor_Phone_Number;
        }
        public string getOfficeHours()
        {
            //if office_Hours does not exist, assign it a value, otherwise return office_hours
            if (office_Hours == null)
            {
                XmlNodeList nodeList = document.GetElementsByTagName("OfficeHours");
                foreach (XmlNode node in nodeList)
                {
                    office_Hours += " " + node.InnerText + " ";
                }
            }
            return office_Hours;
        }
        public string getClassTime()
        {
            if (class_Time == null)
            {
                XmlNodeList nodeList = document.GetElementsByTagName("ClassTime");
                foreach (XmlNode node in nodeList)
                {
                    class_Time += " " + node.InnerText;
                }
            }
            return class_Time;
        }
        public void setAssignments(ListBox list)//pass by reference
        {
            list.Items.Clear();//Clear Assignments
            XmlNodeList titleList = document.GetElementsByTagName("Title");
            XmlNodeList dateList = document.GetElementsByTagName("DueDate");
            XmlNodeList descriptionList = document.GetElementsByTagName("Description");
            for(int i = 0; i< titleList.Count; i++)
            {
                DateTime date = DateTime.Parse(dateList[i].InnerText);
                Assignment work = new Assignment(titleList[i].InnerText, date.ToString("G"), descriptionList[i].InnerText);
                list.Items.Add(work);
            }
        }
    }
}