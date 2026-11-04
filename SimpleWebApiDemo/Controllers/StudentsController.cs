using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SimpleWebApiDemo.Models;

namespace SimpleWebApiDemo.Controllers
{
    // =====================================================================================
    // Controller: StudentsController.cs
    // =====================================================================================
    // WHAT IS AN API CONTROLLER?
    // In ASP.NET Web API, an ApiController is a C# class that handles incoming HTTP requests
    // (GET, POST, PUT, DELETE) and sends back raw data (like JSON), NOT HTML web pages.
    //
    // Notice that it inherits from "ApiController", NOT "Controller" (which is used in MVC).
    //
    // ROUTING CONVENTION:
    // By convention, ASP.NET strips the word "Controller" from the class name.
    // So "StudentsController" becomes accessible at the URL:
    //   -> http://localhost:port/api/students
    // =====================================================================================
    public class StudentsController : ApiController
    {
        // ---------------------------------------------------------------------------------
        // In-Memory Database (Demo List)
        // ---------------------------------------------------------------------------------
        // In a real application, you would connect to a database (like SQL Server via EF).
        // For learning basics without complex database setup, we use a simple static C# List.
        // It persists in memory as long as the web server is running.
        // ---------------------------------------------------------------------------------
        private static List<Student> studentsList = new List<Student>()
        {
            new Student { Id = 1, Name = "Komal Gupta", Course = "ASP.NET & C#", Age = 22 },
            new Student { Id = 2, Name = "Rahul Sharma", Course = "Python & AI", Age = 24 },
            new Student { Id = 3, Name = "Priya Patel", Course = "Cloud Computing", Age = 21 }
        };

        // =================================================================================
        // 1. GET ALL STUDENTS
        // =================================================================================
        // HTTP Verb: GET
        // URL:       http://localhost:port/api/students
        // Purpose:   Retrieve the full list of all students.
        //
        // Explanation:
        // When someone sends a GET request to /api/students, ASP.NET matches the GET verb
        // to this method. Ok(studentsList) sends back HTTP Status 200 (OK) with the list
        // automatically converted to JSON!
        // =================================================================================
        [HttpGet]
        public IHttpActionResult GetAllStudents()
        {
            return Ok(studentsList);
        }

        // =================================================================================
        // 2. GET A SINGLE STUDENT BY ID
        // =================================================================================
        // HTTP Verb: GET
        // URL:       http://localhost:port/api/students/{id}  (e.g., api/students/1)
        // Purpose:   Find and return a specific student matching the given ID.
        //
        // Explanation:
        // ASP.NET takes the number from the URL (e.g. 1) and automatically passes it
        // into the "int id" parameter.
        // If found: returns HTTP 200 OK with the student JSON.
        // If not found: returns HTTP 404 NotFound.
        // =================================================================================
        [HttpGet]
        public IHttpActionResult GetStudentById(int id)
        {
            // LINQ query: find student where Student.Id == id
            var student = studentsList.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                // Returns HTTP 404 Not Found error to the client
                return NotFound();
            }

            // Returns HTTP 200 OK along with the student data
            return Ok(student);
        }

        // =================================================================================
        // 3. CREATE A NEW STUDENT
        // =================================================================================
        // HTTP Verb: POST
        // URL:       http://localhost:port/api/students
        // Purpose:   Accepts student JSON data from the request body and adds them to our list.
        //
        // Explanation:
        // [FromBody] tells ASP.NET: "Read the JSON from the body of the HTTP request and
        // convert it into a Student object".
        // =================================================================================
        [HttpPost]
        public IHttpActionResult CreateStudent([FromBody] Student newStudent)
        {
            // Basic validation: ensure data was provided
            if (newStudent == null)
            {
                return BadRequest("Student data cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(newStudent.Name))
            {
                return BadRequest("Student name is required.");
            }

            // Auto-generate the next ID (e.g., max existing ID + 1)
            int nextId = studentsList.Any() ? studentsList.Max(s => s.Id) + 1 : 1;
            newStudent.Id = nextId;

            // Add the new student to our list
            studentsList.Add(newStudent);

            // In REST APIs, when a new item is created, it is standard practice to return:
            // HTTP 201 (Created) along with the URL where the new item can be viewed.
            return Created(new Uri(Request.RequestUri + "/" + newStudent.Id), newStudent);
        }

        // =================================================================================
        // 4. UPDATE AN EXISTING STUDENT
        // =================================================================================
        // HTTP Verb: PUT
        // URL:       http://localhost:port/api/students/{id}  (e.g., api/students/1)
        // Purpose:   Modify the details of an existing student.
        //
        // Explanation:
        // "int id" comes from the URL, while "updatedStudent" comes from the JSON body.
        // =================================================================================
        [HttpPut]
        public IHttpActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            if (updatedStudent == null)
            {
                return BadRequest("Updated data cannot be empty.");
            }

            // Find the existing student in our list
            var existingStudent = studentsList.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
            {
                return NotFound(); // HTTP 404 if student doesn't exist
            }

            // Update the fields
            existingStudent.Name = updatedStudent.Name;
            existingStudent.Course = updatedStudent.Course;
            existingStudent.Age = updatedStudent.Age;

            // Return HTTP 200 OK with the newly updated student
            return Ok(existingStudent);
        }

        // =================================================================================
        // 5. DELETE A STUDENT
        // =================================================================================
        // HTTP Verb: DELETE
        // URL:       http://localhost:port/api/students/{id}  (e.g., api/students/1)
        // Purpose:   Remove a student from the list.
        //
        // Explanation:
        // Searches for the student by ID. If found, removes it from the list.
        // =================================================================================
        [HttpDelete]
        public IHttpActionResult DeleteStudent(int id)
        {
            var student = studentsList.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound(); // HTTP 404
            }

            studentsList.Remove(student);

            // Return HTTP 200 OK with a friendly message or deleted student
            return Ok(new { message = $"Student with ID {id} was successfully deleted." });
        }
    }
}
