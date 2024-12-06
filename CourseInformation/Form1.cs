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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CourseInformation
{
    public partial class Form1 : Form
    {
        ArrayList courses = new ArrayList();//Course arraylist to hold the courses the user selects
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Course setup
            cmbCourses.DropDownStyle = ComboBoxStyle.DropDownList; //Makes the combobox uneditable
            timer1.Start();//Start Timer

            //set status label to empty
            lblStatus.Text = string.Empty;


            //To Do List Setup
                //Create columns    Title| Description
                todoList.Columns.Add("Title");
                todoList.Columns.Add("Description");

                //point our dataview to data source
                toDoListView.DataSource = todoList;
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            //OpenFileDialog allows the user to pick a file.
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //This allows to filter the type of files that the user can pick. 
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.Title = "Select a XML file";

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the path of the selected file and display to the combobox
                string filePath = openFileDialog.FileName;
                //if the course is already present
                if (cmbCourses.Items.Contains(Path.GetFileNameWithoutExtension(filePath)))//Prevents the same file to be added twice!
                {
                    lblStatus.Text = "Course is already added!";
                    return;
                }

                //Add course to list
                Course course = new Course(filePath);//Make A new Object
                courses.Add(course);//Add course to arraylist
                filePath = Path.GetFileNameWithoutExtension(filePath);//Name of the file
                cmbCourses.Items.Add(filePath);//add name of file to cmb
                lblStatus.Text = "Course Added";//Notify User
            }
        }

        private void cmbCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Show information of selected Course
            showInfo((Course)courses[cmbCourses.SelectedIndex]);
        }
        private void showInfo(Course course)
        {
            //Display Information to Course Information
            course.setAssignments(lbxAssignments);//Asign the list box the listbox from course obj
            lblProfessor.Text = "Professor: " + course.getProfessorName();
            lblEmail.Text = "Email: " + course.getEmail();
            lblPhone.Text = "Phone: " + course.getPhone();
            lblOfficeHours.Text = "Office Hours: " + course.getOfficeHours();
            lblClassTime.Text = "Class Time: " + course.getClassTime();
            lblCourseName.Text = "Course: " + course.getClassName();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("MMMM dd, yyy hh:mm:ss tt");//Clock format
        }
        
        private void lbxAssignments_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Change listbox to the course listbox
            Assignment work = (Assignment)lbxAssignments.SelectedItem;
            lblDueDate.Text = "Due Date: " + work.getDate().ToString("g");
            lblDescription.Text = "Description: " + work.getDescription();
        }


        //\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\


        //To Do List Methods
        DataTable todoList = new DataTable();//This data structure stores the actual list data. Data grid points to this data table! (To Do List)
        bool isEditing = false; //Todo List

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            isEditing = true;
            //Fill text fields with data from table
            if (toDoListView.CurrentCell != null)
            {
                txtTitle.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[0].ToString(); //ItemArray[0] == Title
                txtDescription.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[1].ToString();//ItemArray[1] == description
            }
            else
                lblStatus.Text = "There is nothing to edit!";
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (toDoListView.CurrentCell != null)
                {
                    todoList.Rows[toDoListView.CurrentCell.RowIndex].Delete();
                    this.btnClear_Click(sender, e);//Clear fields
                    lblStatus.Text = "Succefully Deleted!";
                }
                else
                    lblStatus.Text = "There is nothing to delete!";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (isEditing)//if its a currently existing node
            {
                if ( toDoListView.CurrentCell != null)
                {
                    todoList.Rows[toDoListView.CurrentCell.RowIndex]["Title"] = txtTitle.Text;
                    todoList.Rows[toDoListView.CurrentCell.RowIndex]["Description"] = txtDescription.Text;
                    lblStatus.Text = "Successfully Saved!";
                }
                else
                    lblStatus.Text = "There is nothing to Save!";
            }
            else//If its a brand new node
            {
                Console.WriteLine("Title: " + this.txtTitle.Text == string.Empty);
                Console.WriteLine("Description: " + this.txtDescription.Text == string.Empty);
                Console.WriteLine(toDoListView.CurrentCell != null);
                
                if ((this.txtDescription .Text != string.Empty && this.txtTitle.Text != string.Empty))
                {
                    todoList.Rows.Add(txtTitle.Text, txtDescription.Text);
                    lblStatus.Text = "Successfully Saved!";
                }
                else
                    lblStatus.Text = "There is nothing to Save!";

            }
            //clear all text fields
            this.btnClear_Click(sender, e);
            isEditing = false;

        }
    }
}
